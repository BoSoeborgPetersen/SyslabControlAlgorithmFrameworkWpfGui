/**
 * Wraps request with resource name, result and result type.
 */
namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class StringSerializedResultDTO
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public string SerializedResult { get; set; }
        public string ResultTypeName { get; set; }

        public StringSerializedResultDTO() { }

        public StringSerializedResultDTO(string guid, string resourceName, string serializedResult, string resultTypeName)
        {
            Guid = guid;
            ResourceName = resourceName;
            SerializedResult = serializedResult;
            ResultTypeName = resultTypeName;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == this)
        //        return true;
        //    if (obj == null || obj.GetType() != GetType())
        //        return false;
        //    StringSerializedResultDTO val = (StringSerializedResultDTO)obj;
        //    return IsEqual(val.resourceName, resourceName) && IsEqual(val.serializedResult, serializedResult) && IsEqual(val.resultTypeName, resultTypeName);
        //}

        //public static bool IsEqual(object o1, object o2)
        //{
        //    return o1 == o2 || (o1 != null && o1.Equals(o2));
        //}

        public override string ToString() => 
            "StringSerializedResultDTO [ResourceName=" + ResourceName + ", SerializedResult=" + SerializedResult + ", ResultTypeName=" + ResultTypeName + ", Guid=" + Guid + "]";
    }
}