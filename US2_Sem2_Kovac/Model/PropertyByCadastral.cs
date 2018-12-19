using DynHash;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Model
{
    public class PropertyByCadastral : IRecord<PropertyByCadastral>
    {
        public Property Property { get; set; }

        public PropertyByCadastral() => this.Property = new Property();
        public PropertyByCadastral(Property property) => this.Property = property;

        public PropertyByCadastral Clone() => new PropertyByCadastral(this.Property.Clone());

        public bool Equals(PropertyByCadastral obj) => this.Property.CadastralArea.TrimEnd() == obj.Property.CadastralArea.TrimEnd() && this.Property.RN == obj.Property.RN;

        public void FromByteArray(byte[] arr) => this.Property.FromByteArray(arr);

        public void Update(PropertyByCadastral p) => this.Property.Update(p.Property);

        public BitArray GetHash(int Length)
        {
            string binString = Convert.ToString(this.Property.RN, 2);
            Length = Length == 0 ? Int32.MaxValue : Length;
            bool[] bitArray = new bool[Length];
            int i;
            for (i = 0; i < Length && i < binString.Length; i++)
                bitArray[i] = Int32.Parse(binString[binString.Length - 1 - i].ToString()) == 1;

            if (i < Length)
            {
                string fString = string.Join("", Encoding.ASCII.GetBytes(this.Property.CadastralArea).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
                int j = 0;
                while (i < Length)
                {
                    bitArray[i++] = j < fString.Length ? fString[j++] == '1' : false;
                }
            }

            return new BitArray(bitArray);
        }

        public int GetSize() => this.Property.GetSize();

        public byte[] ToByteArray() => this.Property.ToByteArray();

        public int GetID() => this.Property.ID;

        public int KeySize() => 4 + (2 * 15);

        public byte[] GetKey()
        {
            byte[] ret = new byte[this.KeySize()];
            this.Property.SetIntToByteArray(this.Property.RN, 0, ref ret);
            this.Property.SetStringToByteArray(this.Property.CadastralArea, 4, 15, ref ret);
            return ret;
        }

        public void SetKey(byte[] arr)
        {
            this.Property.RN = BitConverter.ToInt32(arr, 0);
            char[] CadastralC = new char[15];
            int index = 4, indexC = 0;
            while (index < 34 && index < arr.Length)
            {
                CadastralC[indexC++] = BitConverter.ToChar(arr, index++);
                index++;
            }
            this.Property.CadastralArea = new string(CadastralC).TrimEnd();
        }

        public bool EmptyKey() => this.Property.CadastralArea == null;
    }
}
