using SyslabControlAlgorithmFrameworkWpfGui.Model;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class StringSerializedParametersDTO : ModelBase
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public string[] SerializedParameters { get; set; }
        public string[] ParameterTypeNames { get; set; }
    }
}