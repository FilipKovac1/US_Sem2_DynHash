using System.Collections;

namespace DynHash
{
    public interface IRecord<T> where T : IRecord<T>
    {
        byte[] ToByteArray();
        void FromByteArray(byte[] arr);
        int GetSize();
        BitArray GetHash(int Length);
        bool Equals(T Compare);
        int GetID();
        T Clone();
        void Update(T Record);
    }
}
