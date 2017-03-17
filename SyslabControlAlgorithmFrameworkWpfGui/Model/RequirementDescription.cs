using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class RequirementDescription : ModelBase
    {
        public double StartupDefer { get; set; }
        public double FreqDeviationMax { get; set; }
        public double VoltDeviationMax { get; set; }
        public double ActivePower { get; set; }
        public double ReactivePower { get; set; }
    }
}
