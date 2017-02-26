using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class EVNT : ModelBase
    {
        public int eventID { get; set; }
        public int parameter1 { get; set; }
        public int parameter2 { get; set; }
        public string reference { get; set; }
        public TSTP time { get; set; }
    }
}
