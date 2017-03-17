using System;
using System.IO;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public abstract class StringSerializerBase : SerializerBase
    {
        public override byte[] SerializeToByte<T>(T data)
        {
            throw new IOException(GetType().Name + " should not be used for binary serialization");
        }

        public override object Deserialize(byte[] serialized, Type type)
        {
            throw new IOException(GetType().Name + " should not be used for binary deserialization");
        }
    }
}