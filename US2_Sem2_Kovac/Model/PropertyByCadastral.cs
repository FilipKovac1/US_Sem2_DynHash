using DynHash;
using System;
using System.Collections;

namespace Model
{
    class PropertyByCadastral : IRecord<PropertyByCadastral>
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
            byte[] byteArr = System.Text.Encoding.ASCII.GetBytes(this.Property.RN + this.Property.CadastralArea);
            Length = Length == 0 ? byteArr.Length : Length; 
            byte[] bitArray = new byte[Length];
            int i = 0;
            foreach (byte b in byteArr)
            {
                if (i >= Length)
                    break;
                bitArray[i++] = b;
            }
            while (i < Length)
                bitArray[i++] = 0;
            
            return new BitArray(bitArray);
        }

        public int GetSize() => this.Property.GetSize();

        public byte[] ToByteArray() => this.Property.ToByteArray();

        public int GetID() => this.Property.ID;
    }
}
