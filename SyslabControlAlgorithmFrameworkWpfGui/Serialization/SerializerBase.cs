using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public abstract class SerializerBase : ISerializer
    {
        public abstract string SerializerName { get; }
        public abstract bool StringNotBinary { get; }
        public abstract bool SupportsCompression { get; }
        public abstract string SerializeToString<T>(T data);
        public abstract byte[] SerializeToByte<T>(T data);
        public abstract object Deserialize(string serialized, Type type);
        public abstract object Deserialize(byte[] serialized, Type type);
    }
}