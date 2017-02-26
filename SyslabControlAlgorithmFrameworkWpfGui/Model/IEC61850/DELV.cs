using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class DELV : ModelBase
    {
        public CompositeMeasurement phaseAB { get; set; }
        public CompositeMeasurement phaseBC { get; set; }
        public CompositeMeasurement phaseCA { get; set; }
        public CompositeMeasurement phaseAverage { get; set; }
    }
}
