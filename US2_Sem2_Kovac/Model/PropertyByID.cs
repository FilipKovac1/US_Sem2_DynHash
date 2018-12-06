using System.Collections;
using DynHash;

namespace Model
{
    class PropertyByID : IRecord<PropertyByID>
    {
        public Property Property { get; set; }

        public PropertyByID() => this.Property = new Property();
        public PropertyByID(Property property) => this.Property = property;

        public PropertyByID Clone() => new PropertyByID(this.Property.Clone());

        public bool Equals(PropertyByID obj) => this.Property.ID == obj.Property.ID;

        public void FromByteArray(byte[] arr) => this.Property.FromByteArray(arr);

        public BitArray GetHash(int Length) => this.Property.GetHash(Length);

        public void Update(PropertyByID p) => this.Property.Update(p.Property);

        public int GetSize() => this.Property.GetSize();

        public byte[] ToByteArray() => this.Property.ToByteArray();

        public override string ToString()
        {
            return this.Property.ToString();
        }

        public int GetID() => this.Property.ID;
    }
}
