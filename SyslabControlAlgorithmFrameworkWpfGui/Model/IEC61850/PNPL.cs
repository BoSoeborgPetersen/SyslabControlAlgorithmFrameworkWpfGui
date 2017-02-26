using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class PNPL : ModelBase
    {
        public string vendor { get; set; }
        public string serialNumber { get; set; }
        public string model { get; set; }
        public string location { get; set; }
    }
}
