using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class CapabilityDescription : ModelBase
    {
        public bool PowerSupplyDecoupled { get; set; }
        public bool BlackstartCapable { get; set; }
        public bool BlackstartReady { get; set; }
        public double BlackstartDelay { get; set; }
        public double FreqDeviation { get; set; }
        public double VoltDeviation { get; set; }
        public double ActivePowerMin { get; set; }
        public double ActivePowerMax { get; set; }
        public double ReactivePowerMin { get; set; }
        public double ReactivePowerMax { get; set; }
        public double Capacity { get; set; }
    }
}
