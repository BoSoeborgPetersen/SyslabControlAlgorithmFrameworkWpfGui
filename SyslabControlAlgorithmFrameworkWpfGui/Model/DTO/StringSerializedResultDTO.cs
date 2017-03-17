using SyslabControlAlgorithmFrameworkWpfGui.Model;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class StringSerializedResultDTO : ModelBase
    {
        public string Guid { get; set; }
        public string ResourceName { get; set; }
        public string SerializedResult { get; set; }
        public string ResultTypeName { get; set; }
    }
}