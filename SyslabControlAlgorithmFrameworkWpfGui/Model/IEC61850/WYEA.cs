using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class WYEA : ModelBase
    {
        public CompositeMeasurement phaseA { get; set; }
        public CompositeMeasurement phaseB { get; set; }
        public CompositeMeasurement phaseC { get; set; }
        public CompositeMeasurement neutral { get; set; }
    }
}
