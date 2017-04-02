using GalaSoft.MvvmLight;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model
{
    public class ClientVM : ViewModelBase
    {
        public ExternalViewClient Client { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsIsolated { get; set; }

        public string DisplayName => String.IsNullOrEmpty(Name) ? "Client (Url: " + Host + ":" + Port + ", IsIsolated: " + IsIsolated + ")" : Name + " (Url: " + Host + ":" + Port + ", IsIsolated: " + IsIsolated + ")";

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            return obj is ClientVM && Name == (obj as ClientVM).Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
