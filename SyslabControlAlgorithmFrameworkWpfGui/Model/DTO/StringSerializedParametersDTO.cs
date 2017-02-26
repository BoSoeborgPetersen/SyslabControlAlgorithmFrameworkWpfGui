using SyslabControlAlgorithmFrameworkWpfGui.Model;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class StringSerializedParametersDTO : ModelBase
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public string[] SerializedParameters { get; set; }
        public string[] ParameterTypeNames { get; set; }

        //public override string ToString() => 
        //    "StringSerializedParametersDTO [ResourceName=" + ResourceName + ", SerializedParameters=" + SerializedParameters + ", ParameterTypeNames=" + ParameterTypeNames + ", Guid=" + Guid + "]";
    }
}