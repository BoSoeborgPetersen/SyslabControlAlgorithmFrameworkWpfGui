using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public interface Serializer
    {
        string SerializerName { get; }
        bool StringNotBinary { get; }
        bool SupportsCompression { get; }

        byte[] SerializeToByte<T>(T data);
        string SerializeToString<T>(T data);
        object Deserialize(byte[] serialized, Type type);
        object Deserialize(string serialized, Type type);
    }
}