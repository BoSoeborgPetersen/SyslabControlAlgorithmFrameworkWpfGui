using SyslabControlAlgorithmFrameworkWpfGui.Model;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class BinarySerializedResultDTO : ModelBase
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public byte[] SerializedResult { get; set; }
        public string ResultTypeName { get; set; }

        //public override string ToString() => 
        //    "BinarySerializedResultDTO [ResourceName=" + ResourceName + ", SerializedResult=" + SerializedResult + ", ResultTypeName=" + ResultTypeName + ", Guid=" + Guid + "]";
    }
}
