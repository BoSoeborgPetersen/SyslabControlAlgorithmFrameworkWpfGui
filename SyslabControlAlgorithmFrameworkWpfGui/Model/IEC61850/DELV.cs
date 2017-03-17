using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class DELV : ModelBase
    {
        public CompositeMeasurement PhaseAB { get; set; }
        public CompositeMeasurement PhaseBC { get; set; }
        public CompositeMeasurement PhaseCA { get; set; }
        public CompositeMeasurement PhaseAverage { get; set; }
    }
}
