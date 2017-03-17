using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class PNPL : ModelBase
    {
        public string Vendor { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
    }
}
