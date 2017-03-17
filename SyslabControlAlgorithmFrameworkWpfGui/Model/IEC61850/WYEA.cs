﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model.IEC61850
{
    public class WYEA : ModelBase
    {
        public CompositeMeasurement PhaseA { get; set; }
        public CompositeMeasurement PhaseB { get; set; }
        public CompositeMeasurement PhaseC { get; set; }
        public CompositeMeasurement Neutral { get; set; }
    }
}
