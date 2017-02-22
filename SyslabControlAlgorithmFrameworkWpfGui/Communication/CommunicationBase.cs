using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public abstract class CommunicationBase
    {
        protected bool talkToJava = true;

        protected int GetSerializationNumber(SerializationType serializationType)
        {
            return 10;
            //int i = 0;
            //foreach (SerializationType type in Enum.GetValues(typeof(SerializationType)))
            //{
            //    if (type.Equals(serializationType))
            //        return i;
            //    i++;
            //}
            //throw new Exception("Invalid serialization type");
        }

        protected byte[] AppendSerializationNumber(byte[] serialized, int serializationNumber)
        {
            Array.Resize(ref serialized, serialized.Length + 1);
            serialized[serialized.Length] = IntToByte(serializationNumber);
            return serialized;
        }

        protected int ExtractSerializationNumber(byte[] serialized) => ByteToInt(serialized[serialized.Length - 1]);

        protected byte[] RemoveSerializationNumber(byte[] serialized)
        {
            Array.Resize(ref serialized, serialized.Length - 1);
            return serialized;
        }

        protected static int ByteToInt(byte b) => b & 0xFF;

        protected static byte IntToByte(int a) => (byte)(a & 0xFF);

        protected string AppendSerializationNumber(string serialized, int serializationNumber)
        {
            string serializationNumberstring = serializationNumber > 9 ? "" + serializationNumber : "0" + serializationNumber;
            return serialized + serializationNumberstring;
        }

        protected int ExtractSerializationNumber(string serialized)
        {
            string serializationNumberstring = serialized.Substring(serialized.Length - 2, 2);
            return int.Parse(serializationNumberstring);
        }

        protected string RemoveSerializationNumber(string serialized) => serialized.Substring(0, serialized.Length - 2);

        protected string ScrambleEscapedXmlTags(string xml) => xml.Replace("&", "@");

        protected string UnscrambleEscapedXmlTags(string xml) => xml.Replace("@", "&");

        protected string MaybeConvertToJavaType(string name)
        {
            if (!talkToJava) return name;

            if (name == "String") return "java.lang.String";
            if (name == "Double") return "java.lang.Double";

            else
                return null;
        }

        protected string MaybeConvertFromJavaType(string name)
        {
            if (!talkToJava) return name;
            
            if (name == "java.util.ArrayList") return "System.Collections.ArrayList";
            if (name == "java.util.Vector") return "System.Collections.ArrayList";
            if (name == "java.lang.Double") return "System.Double";
            if (name == "java.lang.Integer") return "System.Int32";
            
            else
                return null;
        }
    }
}
