using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DynHash
{
    public class Block<T> where T : IRecord<T>
    {

        public List<T> Records { get; set; }
        public int Depth { get; set; }

        private T PomRecord { get; set; }

        public Block(int size, T Record)
        {
            Records = new List<T>(size);
            Depth = 1;
            this.PomRecord = Record.Clone();
        }

        public bool Add(T Record)
        {
            if (this.Records.Count == this.Records.Capacity)
                return false;

            this.Records.Add(Record);

            return true;
        }

        public byte[] ToByteArray()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(this.Records.Count);
                    bw.Write(this.Depth);
                    foreach (T rec in this.Records)
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
                    this.Records = new List<T>(this.Records.Capacity);

                    while (number > this.Records.Count)
                    {
                        PomRecord = PomRecord.Clone();
                        PomRecord.FromByteArray(br.ReadBytes(PomRecord.GetSize()));
                        this.Add(PomRecord);
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
        public Block<T> Split(T record, int MaxDepth)
        {
            BitArray ba = record.GetHash(MaxDepth);
            Block<T> newBlock = new Block<T>(this.Records.Capacity, record);
            int depth = this.Depth;

            while (true)
            {
                depth++;
                if (depth > MaxDepth)
                    return default(Block<T>);
                foreach (T Record in this.Records)
                    if (ba[depth - 1] != Record.GetHash(MaxDepth)[depth - 1])
                        goto Next;
            }

            Next:
            this.Depth = depth;

            List<T> toMove = new List<T>(this.Records.Capacity);
            foreach (T Record in this.Records) // split records with another bit to right or left
                if (Record.GetHash(MaxDepth)[depth - 1])
                    toMove.Add(Record);
            foreach (T Record in toMove)
            { // make move (need to do move for all records with next bit to righ)
                this.Records.Remove(Record);
                newBlock.Add(Record);
            }

            if (ba[depth - 1]) // add to right
                newBlock.Add(record);
            else
                this.Add(record);

            newBlock.Depth = this.Depth;

            return newBlock;
        }

        public T FindRecord(T Record) => this.Records.Where(o => o.Equals(Record)).FirstOrDefault();

        public int GetSize() => this.Records.Capacity * this.PomRecord.GetSize() + 4 + 4;
    }
}
