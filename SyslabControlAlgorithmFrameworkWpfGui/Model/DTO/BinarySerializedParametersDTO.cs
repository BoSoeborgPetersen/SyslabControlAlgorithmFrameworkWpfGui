using SyslabControlAlgorithmFrameworkWpfGui.Model;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class BinarySerializedParametersDTO : ModelBase
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public byte[][] SerializedParameters { get; set; }
        public string[] ParameterTypeNames { get; set; }

        //public override string ToString() => 
        //    "BinarySerializedParametersDTO [ResourceName=" + ResourceName + ", SerializedParameters=" + SerializedParameters + ", ParameterTypeNames=" + ParameterTypeNames + ", Guid=" + Guid + "]";
    }
}
