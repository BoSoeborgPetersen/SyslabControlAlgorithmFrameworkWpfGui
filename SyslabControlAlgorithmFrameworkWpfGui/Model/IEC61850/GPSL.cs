using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class GPSL : ModelBase
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public TSTP time { get; set; }
    }
}
