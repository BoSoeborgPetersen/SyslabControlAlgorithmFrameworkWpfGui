using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public static class SerializationTypeHelper
    {
        public static SerializationType GetFromstring(string name)
        {
            foreach (SerializationType type in Enum.GetValues(typeof(SerializationType)))
            {
                if (name?.ToLower().Contains(Enum.GetName(typeof(SerializationType), type).ToLower()) == true)
                {
                    return type;
                }
            }
            return SerializationType.JsonNewtonsoft;
        }
    }
}
