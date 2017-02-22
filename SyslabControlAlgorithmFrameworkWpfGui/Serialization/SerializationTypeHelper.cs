using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public class SerializationTypeHelper
    {
        public static SerializationType GetFromstring(string name)
        {
            foreach (SerializationType type in Enum.GetValues(typeof(SerializationType)))
            {
                if (name != null && name.ToLower().Contains(Enum.GetName(typeof(SerializationType), type).ToLower()))
                {
                    return type;
                }
            }
            return SerializationType.JsonNewtonsoft;
        }
    }
}
