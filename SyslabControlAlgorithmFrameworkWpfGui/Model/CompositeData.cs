using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public abstract class CompositeData : ModelBase
    {
        public long timestampMicros { get; set; }
        public short timeprecision { get; set; }
        public byte quality { get; set; }
        public byte validity { get; set; }
        public byte source { get; set; }
    }
}
