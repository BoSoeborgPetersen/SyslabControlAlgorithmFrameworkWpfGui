/**
 * Wraps request with resource name, parameters, and parameter types.
 */
namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class StringSerializedParametersDTO
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public string[] SerializedParameters { get; set; }
        public string[] ParameterTypeNames { get; set; }

        public StringSerializedParametersDTO() { }

        public StringSerializedParametersDTO(string resourceName, string[] serializedParameters, string[] parameterTypeNames, string guid = null)
        {
            Guid = guid;
            ResourceName = resourceName;
            SerializedParameters = serializedParameters;
            ParameterTypeNames = parameterTypeNames;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == this)
        //        return true;
        //    if (obj == null || obj.GetType() != GetType())
        //        return false;
        //    StringSerializedParametersDTO val = (StringSerializedParametersDTO)obj;
        //    return IsEqual(val.resourceName, resourceName) && Equals(val.serializedParameters, serializedParameters) && Equals(val.parameterTypeNames, parameterTypeNames);
        //}

        //public static bool IsEqual(object o1, object o2)
        //{
        //    return o1 == o2 || (o1 != null && o1.Equals(o2));
        //}

        public override string ToString() => 
            "StringSerializedParametersDTO [ResourceName=" + ResourceName + ", SerializedParameters=" + SerializedParameters + ", ParameterTypeNames=" + ParameterTypeNames + ", Guid=" + Guid + "]";
    }
}