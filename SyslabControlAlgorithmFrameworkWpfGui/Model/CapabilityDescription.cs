using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class CapabilityDescription : ModelBase
    {
        public bool powerSupplyDecoupled { get; set; }
        public bool blackstartCapable { get; set; }
        public bool blackstartReady { get; set; }
        public double blackstartDelay { get; set; }
        public double freqDeviation { get; set; }
        public double voltDeviation { get; set; }
        public double activePowerMin { get; set; }
        public double activePowerMax { get; set; }
        public double reactivePowerMin { get; set; }
        public double reactivePowerMax { get; set; }
        public double capacity { get; set; }
    }
}
