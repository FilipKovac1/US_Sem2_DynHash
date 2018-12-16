using System;
using System.Collections;
using System.Linq;
using System.Text;
using DynHash;

namespace Model
{
    public class Person : IRecord<Person>
    {
        private string _ID { get; set; }
        public string ID { get
            {
                return this._ID;
            }
            set
            {
                if (value != null && value.Length > 10)
                    value = value.Substring(0, 10);
                this._ID = value;
            }
        }
        private string _Firstname { get; set; }
        public string Firstname
        {
            get
            {
                return this._Firstname;
            }
            set
            {
                if (value != null && value.Length > 10)
                    value = value.Substring(0, 10);
                this._Firstname = value;
            }
        }
        private string _Lastname { get; set; }
        public string Lastname
        {
            get
            {
                return this._Lastname;
            }
            set
            {
                if (value != null && value.Length > 10)
                    value = value.Substring(0, 10);
                this._Lastname = value;
            }
        }

        public Person () { }
        public Person (string id, string firstname, string lastname)
        {
            this.ID = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
        }

        public Person Clone() => new Person(this.ID, this.Firstname, this.Lastname);

        public bool Equals(Person person) => this.ID == person.ID;
        public bool CompareFull(Person person) => this.ID == person.ID && this.Firstname == person.Firstname && this.Lastname == person.Lastname;

        public void FromByteArray(byte[] arr)
        {
            char[] IDc = new char[10];
            char[] FirstC = new char[10];
            char[] LastC = new char[10];
            int index = 0, indexA = 0, indexC = 0, indexD = 0;
            while (index < 10)
            {
                IDc[indexA++] = BitConverter.ToChar(arr, index);
                index += 2;
            }
            while (index < 20)
            {
                FirstC[indexC++] = BitConverter.ToChar(arr, index);
                index += 2;
            }
            while (index < 30)
            {
                LastC[indexD++] = BitConverter.ToChar(arr, index);
                index += 2;
            }
            this.ID = new string(IDc).Replace("\0", "").TrimEnd();
            this.Firstname = new string(FirstC).Replace("\0", "").TrimEnd();
            this.Lastname = new string(LastC).Replace("\0", "").TrimEnd();
        }

        public BitArray GetHash(int Length)
        {
            Length = Length == 0 ? Int32.MaxValue : Length;
            bool[] bitArray = new bool[Length];
            int i = 0;

            if (i < Length)
            {
                string fString = string.Join("", Encoding.ASCII.GetBytes(this.ID).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
                int j = 0;
                while (i < Length)
                    bitArray[i++] = j < fString.Length ? fString[j++] == '1' : false;
            }

            return new BitArray(bitArray);
        }

        public int GetID()
        {
            throw new NotImplementedException();
        }

        public int GetSize() => (2 * 10) + (2 * 10) + (2 * 10);

        public byte[] ToByteArray()
        {
            byte[] ret = new byte[this.GetSize()];
            this.SetStringToByteArray(this.ID, 0, 10, ref ret);
            this.SetStringToByteArray(this.Firstname, 10, 10, ref ret);
            this.SetStringToByteArray(this.Lastname, 20, 10, ref ret);
            return ret;
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
                where[startIndex++] = 0;
                lengthCheck++;
            }
        }

        public void Update(Person Record)
        {
            this.Firstname = Record.Firstname;
            this.Lastname = Record.Lastname;
        }

        public int KeySize() => 2 * 10;
        public byte[] GetKey()
        {
            byte[] ret = new byte[this.KeySize()];
            this.SetStringToByteArray(this.ID, 0, 10, ref ret);
            return ret;
        }
        public void SetKey(byte[] arr)
        {
            char[] IDc = new char[this.KeySize()];
            int index = 0, indexA = 0;
            while (index < 10)
            {
                IDc[indexA++] = BitConverter.ToChar(arr, index);
                index += 2;
            }
            this.ID = new string(IDc).Replace("\0", "").TrimEnd();
        }

        public bool EmptyKey() => this.ID == null;
    }
}
