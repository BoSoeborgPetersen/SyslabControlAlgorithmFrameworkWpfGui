using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class TSTP : ModelBase
    {
        public int Nanoseconds { get; set; }
        public long Seconds { get; set; }
    }
}
