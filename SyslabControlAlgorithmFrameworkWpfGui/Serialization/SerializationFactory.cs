using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public class SerializationFactory
    {
        public static Serializer GetSerializer(SerializationType type)
        {
            if (type == SerializationType.JsonNewtonsoft)
            {
                return new JsonNewtonsoftSerializer();
            }

            throw new ArgumentException("Wrong serialization type");
        }
    }

}