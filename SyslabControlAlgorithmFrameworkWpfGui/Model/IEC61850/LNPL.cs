using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class LNPL : ModelBase
    {
        public string vendor { get; set; }
        public string softwareRevision { get; set; }
        public string nodeDescription { get; set; }
    }
}
