using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model
{
    public class AddressVM
    {
        public string Name { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public bool IsKnown { get; set; }
        public Brush Color => IsKnown ? Brushes.DarkGreen : Brushes.DarkRed;
        public string DisplayName => String.IsNullOrEmpty(Name) ? "Client (Url: " + Hostname + ":" + Port + ")" : Name + " (Url: " + Hostname + ":" + Port + ")";
        public override string ToString() => DisplayName;
    }
}
