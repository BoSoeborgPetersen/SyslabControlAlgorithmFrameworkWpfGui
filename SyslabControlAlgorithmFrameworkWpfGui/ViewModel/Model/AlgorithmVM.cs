using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model
{
    public class AlgorithmVM : ViewModelBase
    {
        private AlgorithmsViewModel parentVM;

        public string Name { get; set; }
        public int Interval { get; set; }
        public string State { get; set; }

        public string DisplayName => Name + " [Interval: " + Interval + ", State: " + State + "]";

        public ICommand StartAlgorithmCommand { get; }
        public ICommand StopAlgorithmCommand { get; }
        public ICommand RestartAlgorithmCommand { get; }
        public ICommand PauseAlgorithmCommand { get; }
        public ICommand ResumeAlgorithmCommand { get; }

        public Visibility StartAlgorithmVisibility => (State == "Initial State" || State == "Stopped" || State == "Error") ? Visibility.Visible : Visibility.Collapsed;
        public Visibility StopAlgorithmVisibility => State == "Running" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility RestartAlgorithmVisibility => State == "Running" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility PauseAlgorithmVisibility => State == "Running" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ResumeAlgorithmVisibility => State == "Paused" ? Visibility.Visible : Visibility.Collapsed;

        public AlgorithmVM(AlgorithmsViewModel parentVM)
        {
            this.parentVM = parentVM;
            StartAlgorithmCommand = new RelayCommand(StartAlgorithm);
            StopAlgorithmCommand = new RelayCommand(StopAlgorithm);
            RestartAlgorithmCommand = new RelayCommand(RestartAlgorithm);
            PauseAlgorithmCommand = new RelayCommand(PauseAlgorithm);
            ResumeAlgorithmCommand = new RelayCommand(ResumeAlgorithm);
        }

        private void StartAlgorithm()
        {
            parentVM.StartAlgorithm(Name);
        }

        private void StopAlgorithm()
        {
            parentVM.StopAlgorithm(Name);
        }

        private void RestartAlgorithm()
        {
            parentVM.RestartAlgorithm(Name);
        }

        private void PauseAlgorithm()
        {
            parentVM.PauseAlgorithm(Name);
        }

        private void ResumeAlgorithm()
        {
            parentVM.ResumeAlgorithm(Name);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            return obj is AlgorithmVM && Name == (obj as AlgorithmVM).Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
