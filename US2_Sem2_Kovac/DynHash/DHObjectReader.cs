using System.Collections.Generic;
using System.IO;

namespace DynHash
{
    public class DHObjectReader
    {
        private string FilePath { get; set; }
        public int LastAddress { get; set; }

        public delegate void Iterate<T>(T Record);
        public delegate void OnAdd(Record record);
        public OnAdd onAdd { get; set; }

        private FileStream fs { get; set; }
        private BinaryWriter bw { get; set; }
        private BinaryReader br { get; set; }

        public DHObjectReader (string filePath)
        {
            this.FilePath = filePath;
            this.LastAddress = 0;

            this.fs = new FileStream(this.FilePath, FileMode.OpenOrCreate);
            this.bw = new BinaryWriter(this.fs);
            this.br = new BinaryReader(this.fs);
        }

        public T Get<T> (int address) where T : IRecord<T>, new()
        {
            T ret = new T();
            byte[] arr = new byte[ret.GetSize()];
            br.BaseStream.Seek(address, SeekOrigin.Begin);
            br.Read(arr, 0, ret.GetSize());
            ret.FromByteArray(arr);
            return ret;
        }

        public int Add<T> (T record) where T : IRecord<T>
        {
            int add = this.LastAddress;
            bw.Seek(add, SeekOrigin.Begin);
            bw.Write(record.ToByteArray());
            this.onAdd?.Invoke(new Record(add, record.ToByteArray()));

            this.LastAddress += record.GetSize();

            return add;
        }

        public LinkedList<T> GetAll<T>(T Record, Iterate<T> iterate = null) where T : IRecord<T>
        {
            LinkedList<T> ret = new LinkedList<T>();
            int position = 0;
            byte[] arr = new byte[Record.GetSize()];
            while (position < this.LastAddress)
            {
                Record = Record.Clone();
                br.BaseStream.Seek(position, SeekOrigin.Begin);
                br.Read(arr, 0, Record.GetSize());
                Record.FromByteArray(arr);
                ret.AddLast(Record);
                iterate?.Invoke(Record);
                position += Record.GetSize();
            }
            return ret;
        }

        public bool Update<T>(T record, int add) where T : IRecord<T>, new()
        {
            T ret = this.Get<T>(add);
            ret.Update(record); // update data
            bw.Seek(add, SeekOrigin.Begin);
            bw.Write(ret.ToByteArray()); // update block in file
            return true;
        }

        public void Destruct()
        {
            this.bw.Close();
            this.br.Close();
            this.fs.Close();
            File.WriteAllText(this.FilePath, string.Empty);
            this.LastAddress = 0;
            this.fs = new FileStream(this.FilePath, FileMode.OpenOrCreate);
            this.bw = new BinaryWriter(this.fs);
            this.br = new BinaryReader(this.fs);
        }

        public override string ToString() => this.LastAddress.ToString();
    }
}
