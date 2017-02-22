/**
* Wraps request with resource name and arguments.
*/
namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class BinarySerializedParametersDTO
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public byte[][] SerializedParameters { get; set; }
        public string[] ParameterTypeNames { get; set; }

        public BinarySerializedParametersDTO() { }

        public BinarySerializedParametersDTO(string resourceName, byte[][] serializedParameters, string[] parameterTypeNames, string guid = null)
        {
            Guid = guid;
            ResourceName = resourceName;
            SerializedParameters = serializedParameters;
            ParameterTypeNames = parameterTypeNames;
        }

        //public override bool Equals(Object obj)
        //{
        //    if (obj == this)
        //        return true;
        //    if (obj == null || obj.GetType() != GetType())
        //        return false;
        //    BinarySerializedParametersDTO val = (BinarySerializedParametersDTO)obj;
        //    // TODO: Compare serialized parameters, by comparing the array of arrays, ie. two layers of Arrays.equals.
        //    return IsEqual(val.resourceName, resourceName) && Equals(val.serializedParameters, serializedParameters) && Equals(val.parameterTypeNames, parameterTypeNames);
        //}

        //public static bool IsEqual(object o1, object o2)
        //{
        //    return o1 == o2 || (o1 != null && o1.Equals(o2));
        //}

        public override string ToString() => 
            "BinarySerializedParametersDTO [ResourceName=" + ResourceName + ", SerializedParameters=" + SerializedParameters + ", ParameterTypeNames=" + ParameterTypeNames + ", Guid=" + Guid + "]";
    }
}
