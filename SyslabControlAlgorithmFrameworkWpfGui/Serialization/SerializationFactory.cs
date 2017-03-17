using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public static class SerializationFactory
    {
        public static ISerializer GetSerializer(SerializationType type)
        {
            if (type == SerializationType.JsonNewtonsoft)
            {
                return new JsonNewtonsoftSerializer();
            }

            throw new ArgumentException("Wrong serialization type");
        }
    }
}