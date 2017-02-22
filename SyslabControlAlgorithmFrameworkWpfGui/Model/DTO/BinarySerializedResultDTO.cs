/**
 * Wraps result with resource name, result and result type.
 */
namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class BinarySerializedResultDTO
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public byte[] SerializedResult { get; set; }
        public string ResultTypeName { get; set; }

        public BinarySerializedResultDTO() { }

        public BinarySerializedResultDTO(string guid, string resourceName, byte[] serializedResult, string resultTypeName)
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
        //    BinarySerializedResultDTO val = (BinarySerializedResultDTO)obj;
        //    return IsEqual(val.resourceName, resourceName) && Equals(val.serializedResult, serializedResult) && IsEqual(val.resultTypeName, resultTypeName);
        //}

        //public static bool IsEqual(object o1, object o2)
        //{
        //    return o1 == o2 || (o1 != null && o1.Equals(o2));
        //}

        public override string ToString() => 
            "BinarySerializedResultDTO [ResourceName=" + ResourceName + ", SerializedResult=" + SerializedResult + ", ResultTypeName=" + ResultTypeName + ", Guid=" + Guid + "]";
    }
}
