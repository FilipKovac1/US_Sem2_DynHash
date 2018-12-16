using System.Collections;

namespace DynHash
{
    public interface IRecord<T> where T : IRecord<T>
    {
        byte[] ToByteArray();
        void FromByteArray(byte[] arr);
        int GetSize();
        int KeySize();
        BitArray GetHash(int Length);
        bool Equals(T Compare);
        byte[] GetKey();
        void SetKey(byte[] arr);
        T Clone();
        void Update(T Record);
        bool EmptyKey();
    }
}
