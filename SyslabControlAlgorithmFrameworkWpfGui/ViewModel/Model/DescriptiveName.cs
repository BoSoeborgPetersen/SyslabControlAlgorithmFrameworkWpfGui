using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model
{
    public class DescriptiveName
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Brush Color { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
