using System;
using System.Collections;
using DynHash;

namespace Model
{
    public class Property : IRecord<Property>
    {
        /// <summary>
        /// Register number (unique through cadastral area)
        /// </summary>
        public int RN { get; set; }
        /// <summary>
        /// Identify number (unique for whole system)
        /// </summary>
        public int ID { get; set; }
        private string _CadastralArea { get; set; }
        public string CadastralArea {
            get { return this._CadastralArea; }
            set {
                if (value != null && value.Length > 15)
                    value = value.Substring(0, 15);
                this._CadastralArea = value;
            }
        } // max 15 length
        private string _Description { get; set; }
        public string Description {
            get { return this._Description; }
            set {
                if (value != null && value.Length > 20)
                    value = value.Substring(0, 20);
                this._Description = value;
            }
        } // max 20 length

        private const char emptySpace = ' ';

        public Property() { }
        public Property(int id) => this.ID = id;
        public Property(int rn, string cadastralArea)
        {
            this.RN = rn;
            this.CadastralArea = cadastralArea;
        }

        public byte[] ToByteArray()
        {
            byte[] ret = new byte[this.GetSize()];
            this.SetIntToByteArray(this.ID, 0, ref ret);
            this.SetIntToByteArray(this.RN, 4, ref ret);
            this.SetStringToByteArray(this._CadastralArea, 8, 15, ref ret);
            this.SetStringToByteArray(this._Description, 38, 20, ref ret);
            return ret;
        }

        private void SetIntToByteArray(int what, int startIndex, ref byte[] where)
        {
            foreach (byte b in BitConverter.GetBytes(what))
                where[startIndex++] = b;
        }
        private void SetStringToByteArray(string what, int startIndex, int length, ref byte[] where)
        {
            byte[] tmp = new byte[2];
            int lengthCheck = 0;
            foreach (char c in what.ToCharArray())
            {
                tmp = BitConverter.GetBytes(c);
                foreach (byte b in tmp)
                    where[startIndex++] = b;
                lengthCheck++;
            }
            while (lengthCheck != length)
            {
                tmp = BitConverter.GetBytes(Property.emptySpace);
                foreach (byte b in tmp)
                    where[startIndex++] = b;
                lengthCheck++;
            }
        } 

        public void Update(Property toUpdate)
        {
            this.Description = toUpdate.Description; // cannot update any other field (others are indexed and there is deletion necessary)
        }

        public void FromByteArray(byte[] arr)
        {
            this.ID = BitConverter.ToInt32(arr, 0);
            this.RN = BitConverter.ToInt32(arr, 4);
            char[] CadastralC = new char[15];
            char[] DescriptionC = new char[20];
            int index = 8, indexC = 0, indexD = 0;
            while (index < 38)
            {
                CadastralC[indexC++] = BitConverter.ToChar(arr, index++);
                index++;
            }
            while (index < this.GetSize())
            {
                DescriptionC[indexD++] = BitConverter.ToChar(arr, index++);
                index++;
            }
            this.CadastralArea = new string(CadastralC).TrimEnd();
            this.Description = new string(DescriptionC).TrimEnd();
        }

        public int GetSize() => 4 + 4 + (2 * 15) + (2 * 20);

        public BitArray GetHash(int Length)
        {
            string binString = Convert.ToString(this.ID, 2);
            Length = Length == 0 ? binString.Length : Length;
            bool[] bitArray = new bool[Length];
            int i = 0;
            for (i = 0; i < Length && i < binString.Length; i++)
                bitArray[i] = Int32.Parse(binString[binString.Length - 1 - i].ToString()) == 1;
            while (i < Length)
                bitArray[i++] = false;
            return new BitArray(bitArray);
        }

        public bool Equals(Property obj) => obj.ID == this.ID && this.CadastralArea == obj.CadastralArea && this.RN == obj.RN;
        public Property Clone()
        {
            return new Property
            {
                ID = this.ID,
                RN = this.RN,
                Description = this.Description,
                CadastralArea = this.CadastralArea
            };
        }

        public override string ToString()
        {
            return this.ID + " " + this.RN + " " + this.CadastralArea;
        }

        public int GetID() => this.ID;

        public bool CompareFull(Property p) => this.ID == p.ID && this.CadastralArea == p.CadastralArea && this.RN == p.RN && this.Description == p.Description;
    }
}
