using System;
using System.IO;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public abstract class BinarySerializerBase : SerializerBase
    {
        public override string SerializeToString<T>(T data)
        {
            throw new IOException(GetType().Name + " should not be used for string serialization");
        }

        public override object Deserialize(string serialized, Type type)
        {
            throw new IOException(GetType().Name + " should not be used for string deserialization");
        }
    }
}