using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Model
{
    public abstract class ModelBase
    {
        //public override string ToString()
        //{
        //    return "DELV \n  [phaseAB=" + Indent(phaseAB) + ", phaseBC=" + Indent(phaseBC) + ", phaseCA=" + Indent(phaseCA) + ", phaseAverage=" + Indent(phaseAverage) + "]";
        //}

        protected string Indent(object input)
        {
            if (input == null) return "Error";
            return input.ToString().Replace("\n", "\n  ");
        }

        //public override string ToString() =>
        //    "CompositeMeasurement \n" +
        //    "  [value=" + value + ", \n" +
        //    "   timestampMicros=" + timestampMicros + ", \n" +
        //    "   timeprecision=" + timeprecision + ", \n" +
        //    "   quality=" + quality + ", \n" +
        //    "   validity=" + validity + ", \n" +
        //    "   source=" + source + "]";

        //public override string ToString()
        //{
        //    return "DELV \n  [phaseAB=" + Indent(phaseAB) + ", phaseBC=" + Indent(phaseBC) + ", phaseCA=" + Indent(phaseCA) + ", phaseAverage=" + Indent(phaseAverage) + "]";
        //}

        public override string ToString()
        {
            string result = GetType().Name + " \n[";

            foreach (var property in GetType().GetProperties())
            {
                result += "\n  " + property.Name + ": " + (!GetType().IsValueType ? Indent(property.GetValue(this)) : property.GetValue(this)) + ", ";
            }

            return result + "\n]";
        }
    }
}
