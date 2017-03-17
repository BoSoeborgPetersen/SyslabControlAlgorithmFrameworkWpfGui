using SyslabControlAlgorithmFrameworkWpfGui.Model;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class BinarySerializedParametersDTO : ModelBase
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public byte[][] SerializedParameters { get; set; }
        public string[] ParameterTypeNames { get; set; }
    }
}
