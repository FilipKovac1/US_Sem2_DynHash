using System.Linq;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace DynHash
{
    class DynHash<T> where T : IRecord<T>
    {
        private Trie Trie { get; set; }
        public int MaxDepth { get; set; }

        public string FilePath { get; set; }
        private int LastPosition { get; set; }

        public int BlockSize { get; set; }
        public delegate void Iterate (T Record);

        private FileStream fs { get; set; }
        private BinaryWriter bw { get; set; }
        private BinaryReader br { get; set; }

        public DynHash(int MaxDepth, int BlockSize, string FilePath)
        {
            this.Trie = new Trie();
            this.FilePath = FilePath; // absolute path to the directory with files
            this.LastPosition = 0;
            this.MaxDepth = MaxDepth;
            this.BlockSize = BlockSize;
            this.OpenFile();
        }

        public bool Add(T Record)
        {
            Block<T> block = new Block<T>(BlockSize, Record);
            BitArray recordHash = Record.GetHash(this.MaxDepth);
            Node add = Trie.Find(recordHash, out int depth);
            if (add == null || // add is null only on first insert 
                (depth < this.MaxDepth && add.BlockSize <= this.BlockSize) || // split or add
                (depth == this.MaxDepth && add.BlockSize < this.BlockSize)) // just add
            {
                if (add == null || add.BlockSize < 0)
                {
                    block.Add(Record);
                    if (add == null)
                        Trie.Add(recordHash, this.LastPosition, 1);
                    else
                    {
                        add.BlockSize = 1;
                        add.Address = this.LastPosition;
                    }
                    bw.Seek(this.LastPosition, SeekOrigin.Begin);
                    bw.Write(block.ToByteArray());
                    this.LastPosition += block.GetSize();
                }
                else
                {
                    this.LoadBlock(ref block, add.Address);
                    if (block.FindRecord(Record) != null)
                        return false;
                    if (add.BlockSize == BlockSize)
                    {
                        Block<T> blockNew = block.Split(Record, this.MaxDepth);
                        if (blockNew == null)
                            return this.AddFull(Record, add);
                        Node nodeNew = Trie.Add(recordHash, this.LastPosition, blockNew.Depth, true);
                        nodeNew.Right.BlockSize = blockNew.Records.Count;
                        nodeNew.Left.BlockSize = block.Records.Count;
                        nodeNew.Left.Address = add.Address;
                        add.BlockSize = -1;
                        add.Address = -1;
                        bw.Seek(nodeNew.Right.Address, SeekOrigin.Begin);
                        bw.Write(blockNew.ToByteArray());
                        this.LastPosition += block.GetSize();
                        add = nodeNew.Left;
                    }
                    else
                    {
                        block.Add(Record);
                        add.BlockSize++;
                    }
                    bw.Seek(add.Address, SeekOrigin.Begin);
                    bw.Write(block.ToByteArray());
                }
            }
            else
                return this.AddFull(Record, add);

            return true;
        }

        private void LoadBlock(ref Block<T> block, int address)
        {
            byte[] arr = new byte[block.GetSize()];
            br.BaseStream.Seek(address, SeekOrigin.Begin);
            br.Read(arr, 0, block.GetSize());
            block.FromByteArray(arr);
        }

        /// <summary>
        /// Looking for record by address from trie
        /// </summary>
        /// <param name="Record"></param>
        /// <returns></returns>
        public T Find(T Record)
        {
            Node add = Trie.Find(Record.GetHash(this.MaxDepth), out int depth);
            if (add == null)
                return default(T);

            Block<T> block = new Block<T>(BlockSize, Record);
            this.LoadBlock(ref block, add.Address);

            T ret = block.FindRecord(Record);
            if (ret == null && add.Next != null)
            {
                foreach (Node act in add.Next)
                {
                    this.LoadBlock(ref block, act.Address);
                    ret = block.FindRecord(Record);
                    if (ret != null)
                        break;
                }
            }
            return ret;
        }

        public bool Update (T Record)
        {
            Node n = Trie.Find(Record.GetHash(this.MaxDepth), out int depth);
            int add = n.Address;

            if (n == null)
                return false;

            Block<T> block = new Block<T>(BlockSize, Record);
            this.LoadBlock(ref block, n.Address);

            T ret = block.FindRecord(Record);
            if (ret == null && n.Next != null)
            {
                foreach (Node act in n.Next)
                {
                    add = act.Address;
                    this.LoadBlock(ref block, act.Address);
                    ret = block.FindRecord(Record);
                    if (ret != null)
                        break;
                }
            }
            if (ret == null)
                return false;

            ret.Update(Record); // update data
            bw.Seek(add, SeekOrigin.Begin);
            bw.Write(block.ToByteArray()); // update block in file

            return true;
        }

        private bool AddFull(T Record, Node add)
        {
            bool create = false;
            Create:
            Block<T> block = new Block<T>(this.BlockSize, Record);
            if (add.Next.LastOrDefault() == null || create) // if node has not any another blocks
            { // or block is full
                add.Next.AddLast(new Node(1, this.LastPosition)); // alloc position for new block
                this.LastPosition += block.GetSize(); 
            }
            else
            {
                foreach (Node act in add.Next) // check if is already added 
                { // if not, block is set to Last also
                    this.LoadBlock(ref block, act.Address);
                    if (block.FindRecord(Record) != null)
                        return false;
                }
            }
            if (block.Records.Count == this.BlockSize)
            {
                create = true;
                goto Create;
            }

            block.Add(Record);

            add.Next.Last().BlockSize = block.Records.Count; // set blockSize
            bw.Seek(add.Next.Last().Address, SeekOrigin.Begin);
            bw.Write(block.ToByteArray()); // write to file
            return true;
        }

        public LinkedList<T> GetAll(T Record, Iterate iterate = null)
        {
            LinkedList<T> ret = new LinkedList<T>();
            Block<T> block = new Block<T>(this.BlockSize, Record);
            int position = 0;
            while (position < this.LastPosition)
            {
                this.LoadBlock(ref block, position);
                foreach (T r in block.Records)
                {
                    iterate?.Invoke(r);
                    ret.AddLast(r);
                }
                position += block.GetSize();
            }
            return ret;
        }

        public override string ToString()
        {
            string ret = String.Format("'{0}' {1} {2} {3}\n", this.FilePath, this.MaxDepth, this.BlockSize, this.LastPosition);

            ret += Trie.ToString();
            return ret;
        }

        public void Load (string FilePathToTextFile, T Record)
        {
            this.Trie = new Trie();
            this.CloseFile();
            string[] line;
            int address;
            Block<T> block;
            Node primary;
            try
            {
                using (StreamReader sr = new StreamReader(FilePathToTextFile))
                {
                    line = sr.ReadLine().Split(' ');
                    this.FilePath = line[0].Replace("'", "");
                    this.MaxDepth = Int32.Parse(line[1]);
                    this.BlockSize = Int32.Parse(line[2]);
                    this.LastPosition = Int32.Parse(line[3]);
                    this.OpenFile();
                    block = new Block<T>(this.BlockSize, Record);
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine().Split(' ');
                        address = Int32.Parse(line[0]);
                        this.LoadBlock(ref block, address);
                        primary = this.Trie.Add(block.Records.First().GetHash(block.Depth), address, block.Depth);
                        primary.Address = address;
                        primary.BlockSize = block.Records.Count;
                        if (primary.BlockSize == this.BlockSize)
                        {
                            for (int i = 1; i < line.Length; i++)
                            {
                                if (!Int32.TryParse(line[i], out address))
                                    continue;
                                this.LoadBlock(ref block, address);
                                primary.Next.AddLast(new Node(block.Records.Count, address));
                            }
                        }
                    }
                    sr.Close();
                }
            } catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }

        public void Save (string FilePathToTextFile)
        {
            using (StreamWriter sw = new StreamWriter(FilePathToTextFile))
            {
                sw.Write(this.ToString());
                sw.Close();
            }
        }

        public void Destruct()
        {
            this.Trie = new Trie();
            this.LastPosition = 0;
            this.CloseFile();
            this.ClearFile();
            this.OpenFile();
        }

        public void ClearFile() => File.WriteAllText(this.FilePath, string.Empty); // clear file

        private void OpenFile()
        {
            this.fs = new FileStream(this.FilePath, FileMode.OpenOrCreate);
            this.bw = new BinaryWriter(this.fs);
            this.br = new BinaryReader(this.fs);
        }

        public void CloseFile ()
        {
            this.bw.Close();
            this.br.Close();
            this.fs.Close();
        }
    }
}
