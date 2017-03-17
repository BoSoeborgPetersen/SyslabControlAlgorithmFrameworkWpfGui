using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class MeasurementItem : ModelBase
    {
        public MeasurementName Name { get; set; }
        public CompositeMeasurement Measurement { get; set; }
    }
}
