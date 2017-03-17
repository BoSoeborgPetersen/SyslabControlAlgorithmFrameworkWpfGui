using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class Flexibility : ModelBase
    {
        public double Cost { get; set; }
        public double Activepower { get; set; }
        public double PowerflexibilityUp { get; set; }
        public double PowerflexibilityDown { get; set; }
        public long Time_ms { get; set; }
    }
}
