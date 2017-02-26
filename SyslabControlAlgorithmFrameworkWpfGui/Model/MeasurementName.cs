using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class MeasurementName : ModelBase
    {
        public string server { get; set; }
        public string unit { get; set; }
        public string part { get; set; }
        public MeasurementType type { get; set; }
        public string name { get; set; }
    }
}
