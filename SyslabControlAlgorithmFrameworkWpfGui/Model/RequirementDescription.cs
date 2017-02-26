using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class RequirementDescription : ModelBase
    {
        public double startupDefer { get; set; }
        public double freqDeviationMax { get; set; }
        public double voltDeviationMax { get; set; }
        public double activePower { get; set; }
        public double reactivePower { get; set; }
    }
}
