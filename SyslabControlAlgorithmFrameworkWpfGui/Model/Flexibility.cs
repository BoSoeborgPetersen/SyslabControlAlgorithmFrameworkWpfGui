using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class Flexibility : ModelBase
    {
        public double cost { get; set; }
        public double activepower { get; set; }
        public double powerflexibilityUp { get; set; }
        public double powerflexibilityDown { get; set; }
        public long time_ms { get; set; }
    }
}
