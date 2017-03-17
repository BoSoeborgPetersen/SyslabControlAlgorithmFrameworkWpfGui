using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class EVNT : ModelBase
    {
        public int EventID { get; set; }
        public int Parameter1 { get; set; }
        public int Parameter2 { get; set; }
        public string Reference { get; set; }
        public TSTP Time { get; set; }
    }
}
