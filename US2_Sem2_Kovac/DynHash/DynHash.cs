using System.Linq;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace DynHash
{
    class DynHash<T> where T : IRecord<T>, new()
    {
        private Trie Trie { get; set; }
        public int MaxDepth { get; set; }

        public string FilePath { get; set; }
        private int LastPosition { get; set; }

        public int BlockSize { get; set; }

        private DHObjectReader ObjectReader { get; set; }

        private FileStream fs { get; set; }
        private BinaryWriter bw { get; set; }
        private BinaryReader br { get; set; }

        private bool AddFlag { get; set; }
        private int AddAddress { get; set; }

        public DynHash(int MaxDepth, int BlockSize, string FilePath, DHObjectReader objectReader)
        {
            this.Trie = new Trie();
            this.FilePath = FilePath; // absolute path to the file with blocks
            this.LastPosition = 0;
            this.MaxDepth = MaxDepth;
            this.BlockSize = BlockSize;
            this.OpenFile();
            this.ObjectReader = objectReader;
            this.AddFlag = false;
            this.ObjectReader.onAdd += (tmpRec) =>
            {
                if (this.AddFlag)
                    return;
                T rec = new T();
                rec.FromByteArray(tmpRec.Key);
                tmpRec.Key = rec.GetKey();
                this.Add(rec, tmpRec, false);
            };
        }

        private bool Add(T Record, Record tmpRec = null, bool check = true)
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
                    if (tmpRec == null)
                    {
                        this.RemoveAddEvent();
                        block.Add(new Record(ObjectReader.Add(Record), Record.GetKey()));
                        this.AddAddEvent();
                    }
                    else
                        block.Add(tmpRec);
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
                    if (check && this.FindAddress(Record, add) >= 0)
                        return false;

                    if (tmpRec == null)
                    {
                        this.RemoveAddEvent();
                        tmpRec = new Record(ObjectReader.Add(Record), Record.GetKey());
                        this.AddAddEvent();
                    }
                    this.LoadBlock(ref block, add.Address);
                    if (add.BlockSize == BlockSize)
                    {
                        Block<T> blockNew = block.Split(Record, this.MaxDepth, tmpRec.Address);
                        if (blockNew == null)
                            return this.AddFull(Record, add, false, tmpRec);
                        Node nodeNew = Trie.Add(recordHash, this.LastPosition, blockNew.Depth, true);
                        nodeNew.Right.BlockSize = blockNew.Records.Count;
                        nodeNew.Left.BlockSize = block.Records.Count;
                        nodeNew.Left.Address = add.Address;
                        if (add.Next.Count > 0)
                        {
                            nodeNew.Left.Next = new LinkedList<Node>(add.Next);
                            add.Next.Clear();
                        }
                        add.BlockSize = -1;
                        add.Address = -1;

                        bw.Seek(nodeNew.Right.Address, SeekOrigin.Begin);
                        bw.Write(blockNew.ToByteArray());
                        this.LastPosition += block.GetSize();
                        add = nodeNew.Left;
                    }
                    else
                    {
                        block.Add(tmpRec);
                        add.BlockSize++;
                    }
                    bw.Seek(add.Address, SeekOrigin.Begin);
                    bw.Write(block.ToByteArray());
                }
            }
            else
                return this.AddFull(Record, add, tmpRec == null, tmpRec);

            return true;
        }

        public bool Add(T Record) => this.Add(Record, null);

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
        public T Find(T Record, Node add = null)
        {
            if (add == null)
                add = Trie.Find(Record.GetHash(this.MaxDepth), out int depth);
            if (add == null || add.Address < 0)
                return default(T);

            Block<T> block = new Block<T>(BlockSize, Record);
            this.LoadBlock(ref block, add.Address);

            int? ret = block.FindRecord(Record);
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
            if (ret == null)
                return default(T);
            return this.ObjectReader.Get<T>(ret.Value);
        }

        private int FindAddress(T Record, Node add = null)
        {
            if (add == null)
                add = Trie.Find(Record.GetHash(this.MaxDepth), out int depth);
            if (add == null)
                return -1;

            Block<T> block = new Block<T>(BlockSize, Record);
            this.LoadBlock(ref block, add.Address);

            int? ret = block.FindRecord(Record);
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
            return ret.HasValue ? ret.Value : -1;
        }

        public bool Update(T Record)
        {
            int add = this.FindAddress(Record);
            if (add >= 0)
                return this.ObjectReader.Update(Record, add);
            return false;
        }

        private bool AddFull(T Record, Node add, bool check = false, Record tmpRec = null)
        {
            if (check && this.FindAddress(Record, add) >= 0)
                    return false;

            Block<T> block = new Block<T>(this.BlockSize, Record);
            if (add.Next.LastOrDefault() == null || add.Next.Last().BlockSize == this.BlockSize) // if node has not any another blocks
            { // or block is full
                add.Next.AddLast(new Node(0, this.LastPosition)); // alloc position for new block
                this.LastPosition += block.GetSize();
            }

            if (add.Next.Last().BlockSize > 0)
                this.LoadBlock(ref block, add.Next.Last().Address);

            if (tmpRec == null)
            {
                this.RemoveAddEvent();
                block.Add(new Record(ObjectReader.Add(Record), Record.GetKey()));
                this.AddAddEvent();
            }
            else
                block.Add(tmpRec);
            add.Next.Last().BlockSize = block.Records.Count; // set blockSize
            bw.Seek(add.Next.Last().Address, SeekOrigin.Begin);
            bw.Write(block.ToByteArray()); // write to file
            return true;
        }

        public LinkedList<T> GetAll(T Record, DHObjectReader.Iterate<T> iterate = null) => this.ObjectReader.GetAll(Record, iterate);

        public override string ToString()
        {
            string ret = String.Format("'{0}' {1} {2} {3} {4}\n", this.FilePath, this.MaxDepth, this.BlockSize, this.LastPosition, this.ObjectReader.LastAddress);

            ret += Trie.ToString();
            return ret;
        }

        public void Load(string FilePathToTextFile, T rec)
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
                    this.ObjectReader.LastAddress = Int32.Parse(line[4]);

                    this.OpenFile();
                    block = new Block<T>(this.BlockSize, rec);
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine().Split(' ');
                        address = Int32.Parse(line[0]);
                        this.LoadBlock(ref block, address);
                        rec.SetKey(block.Records.First().Key);
                        primary = this.Trie.Add(rec.GetHash(block.Depth), address, block.Depth);
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
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }

        public void Save(string FilePathToTextFile)
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
            this.ObjectReader.Destruct();
            this.OpenFile();
        }

        public void ClearFile() => File.WriteAllText(this.FilePath, string.Empty); // clear file

        private void OpenFile()
        {
            this.fs = new FileStream(this.FilePath, FileMode.OpenOrCreate);
            this.bw = new BinaryWriter(this.fs);
            this.br = new BinaryReader(this.fs);
        }

        public void CloseFile()
        {
            this.bw.Close();
            this.br.Close();
            this.fs.Close();
        }

        private void RemoveAddEvent() => this.AddFlag = true;
        private void AddAddEvent() => this.AddFlag = false;
    }
}
