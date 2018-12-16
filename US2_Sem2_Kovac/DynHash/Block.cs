using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DynHash
{
    public class Block<T> where T : IRecord<T>
    {

        public List<Record> Records { get; set; }
        public int Depth { get; set; }

        private T TmpObj { get; set; }
        private Record TmpRec { get; set; }

        public Block(int size, T Record)
        {
            Records = new List<Record>(size);
            Depth = 1;
            this.TmpObj = Record.Clone();
            this.TmpRec = new Record(0, this.TmpObj.EmptyKey() ? new byte[this.TmpObj.KeySize()] : this.TmpObj.GetKey());
        }

        public bool Add(T Record, int Address)
        {
            if (this.Records.Count == this.Records.Capacity)
                return false;

            this.Records.Add(new Record(Address, Record.GetKey()));

            return true;
        }
        public void Add(Record rec) => this.Records.Add(rec);

        public byte[] ToByteArray()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(this.Records.Count);
                    bw.Write(this.Depth);
                    foreach (Record rec in this.Records)
                        bw.Write(rec.ToByteArray());
                    return ms.ToArray();
                }
            }
        }

        public void FromByteArray(byte[] arr)
        {
            using (MemoryStream ms = new MemoryStream(arr))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    int number = br.ReadInt32();
                    this.Depth = br.ReadInt32();
                    this.Records = new List<Record>(this.Records.Capacity);
                    while (number > this.Records.Count)
                    {
                        this.TmpRec = new Record(0, new byte[this.TmpObj.KeySize()]);
                        this.TmpRec.FromByteArray(br.ReadBytes(this.TmpRec.GetSize()));
                        this.Records.Add(this.TmpRec);
                    }
                }
            }
        }

        /// <summary>
        /// Split one block to two (this and one new returned) check all records if should they go left or right
        /// and set the depth of both blocks
        /// </summary>
        /// <param name="record"></param>
        /// <param name="MaxDepth"></param>
        /// <returns></returns>
        public Block<T> Split(T record, int MaxDepth, int Address)
        {
            BitArray ba = record.GetHash(MaxDepth);
            Block<T> newBlock = new Block<T>(this.Records.Capacity, record);
            T tmp = record.Clone();
            int depth = this.Depth;

            while (true)
            {
                depth++;
                if (depth > MaxDepth)
                    return default(Block<T>);
                foreach (Record Record in this.Records)
                {
                    tmp.SetKey(Record.Key); // set key
                    if (ba[depth - 1] != tmp.GetHash(MaxDepth)[depth - 1]) // compare hash
                        goto Next;
                }
            }

            Next:
            this.Depth = depth;

            List<Record> toMove = new List<Record>(this.Records.Capacity);
            foreach (Record Record in this.Records)
            { // split records with another bit to right or left
                tmp.SetKey(Record.Key);
                if (tmp.GetHash(MaxDepth)[depth - 1])
                    toMove.Add(Record);
            }
            foreach (Record Record in toMove)
            { // make move (need to do move for all records with next bit to righ)
                this.Records.Remove(Record);
                newBlock.Records.Add(Record);
            }

            if (ba[depth - 1]) // add to right
                newBlock.Add(record, Address);
            else
                this.Add(record, Address);

            newBlock.Depth = this.Depth;

            return newBlock;
        }

        public int? FindRecord(T Record) => this.Records.Where(o => o.Equals(Record.GetKey())).FirstOrDefault()?.Address;

        public int GetSize() => this.Records.Capacity * this.TmpObj.GetSize() + 4 + 4;
    }

    public class Record
    {
        public int Address { get; set; }
        public byte[] Key { get; set; }

        public Record(int address, byte[] key)
        {
            this.Address = address;
            this.Key = key;
        }

        public int GetSize() => this.Key.Length + 4;
        public void FromByteArray(byte[] arr)
        {
            for (int i = 0; i < this.Key.Length; i++)
                this.Key[i] = arr[i];
            this.Address = BitConverter.ToInt32(arr, this.Key.Length);
        }
        public byte[] ToByteArray()
        {
            byte[] ret = new byte[this.GetSize()];
            int i = 0;
            for (i = 0; i < this.Key.Length; i++)
                ret[i] = this.Key[i];
            foreach (byte b in BitConverter.GetBytes(this.Address))
                ret[i++] = b;

            return ret;
        }

        public bool Equals(byte[] key) {
            if (this.Key.Length != key.Length)
                return false;
            for (int i = 0; i < this.Key.Length; i++)
                if (this.Key[i] != key[i])
                    return false;

            return true;
        }
    }
}
