using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class MeasurementName : ModelBase
    {
        public string Server { get; set; }
        public string Unit { get; set; }
        public string Part { get; set; }
        public MeasurementType Type { get; set; }
        public string Name { get; set; }
    }
}
