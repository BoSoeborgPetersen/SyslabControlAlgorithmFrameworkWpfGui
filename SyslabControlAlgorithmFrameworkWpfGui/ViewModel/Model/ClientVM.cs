using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model
{
    public class ClientVM : ViewModelBase
    {
        private AlgorithmsViewModel parentVM;

        public ExternalViewClient Client { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsIsolated { get; set; }

        public string DisplayName => String.IsNullOrEmpty(Name)? "Client (Url: " + Host + ":" + Port + ", IsIsolated: " + IsIsolated + ")" : Name + " (Url: " + Host + ":" + Port + ", IsIsolated: " + IsIsolated + ")";

        public ICommand IsolateCommand { get; }
        public ICommand UnisolateCommand { get; }

        public Visibility IsolateVisibility => !IsIsolated ? Visibility.Visible : Visibility.Collapsed;
        public Visibility UnisolateVisibility => IsIsolated ? Visibility.Visible : Visibility.Collapsed;

        public ClientVM(AlgorithmsViewModel parentVM)
        {
            this.parentVM = parentVM;
            IsolateCommand = new RelayCommand(SwitchIsIsolated);
            UnisolateCommand = new RelayCommand(SwitchIsIsolated);
        }

        private void SwitchIsIsolated()
        {
            parentVM.SwitchIsIsolated(Name);
        }

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
