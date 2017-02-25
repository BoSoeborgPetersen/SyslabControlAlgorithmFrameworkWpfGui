using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public class CompositeMeasurement
    {
        public double value { get; set; }
        public long timestampMicros { get; set; }
        public short timeprecision { get; set; }
        public byte quality { get; set; }
        public byte validity { get; set; }
        public byte source { get; set; }

        public override string ToString() =>
            "CompositeMeasurement \n" +
            "  [value=" + value + ", \n" +
            "   timestampMicros=" + timestampMicros + ", \n" +
            "   timeprecision=" + timeprecision + ", \n" +
            "   quality=" + quality + ", \n" +
            "   validity=" + validity + ", \n" +
            "   source=" + source + "]";
    }
}
