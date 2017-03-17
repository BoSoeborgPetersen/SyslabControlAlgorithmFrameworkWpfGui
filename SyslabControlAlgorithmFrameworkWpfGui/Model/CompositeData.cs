using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public abstract class CompositeData : ModelBase
    {
        public long TimestampMicros { get; set; }
        public short Timeprecision { get; set; }
        public byte Quality { get; set; }
        public byte Validity { get; set; }
        public byte Source { get; set; }
    }
}
