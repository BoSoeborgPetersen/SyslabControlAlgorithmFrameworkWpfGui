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
    public class ClientControlVM : ClientVM
    {
        private AlgorithmsViewModel parentVM;

        public ICommand IsolateCommand { get; }
        public ICommand UnisolateCommand { get; }

        public Visibility IsolateVisibility => !IsIsolated ? Visibility.Visible : Visibility.Collapsed;
        public Visibility UnisolateVisibility => IsIsolated ? Visibility.Visible : Visibility.Collapsed;

        public ClientControlVM(AlgorithmsViewModel parentVM)
        {
            this.parentVM = parentVM;
            IsolateCommand = new RelayCommand(SwitchIsIsolated);
            UnisolateCommand = new RelayCommand(SwitchIsIsolated);
        }

        private void SwitchIsIsolated()
        {
            parentVM.SwitchIsIsolated(Name);
        }
    }
}
