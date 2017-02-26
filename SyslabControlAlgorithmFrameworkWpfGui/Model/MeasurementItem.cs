using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class MeasurementItem : ModelBase
    {
        public MeasurementName name { get; set; }
        public CompositeMeasurement measurement { get; set; }
    }
}
