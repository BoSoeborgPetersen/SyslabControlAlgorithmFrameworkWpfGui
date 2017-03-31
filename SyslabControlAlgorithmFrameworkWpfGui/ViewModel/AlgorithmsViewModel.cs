using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class AlgorithmsViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();
        private ExternalViewClient selectedClient;
        private ClientVM selectedClientVM;

        private object controlParameter;
        private AlgorithmVM selectedAlgorithmVM;
        private string selectedControlParameterName;
        private string selectedControlOutputName;

        public ObservableCollection<ClientVM> Clients => new ObservableCollection<ClientVM>(clients.Select(x =>
            new ClientVM(this) {
                Client = x,
                Name = x.Name,
                Host = x.Hostname,
                Port = x.Port,
                IsIsolated = x.IsIsolated
            }));
        public ClientVM SelectedClient { get { return selectedClientVM; } set { SetSelectedClient(value); } }

        public ObservableCollection<AlgorithmVM> Algorithms => new ObservableCollection<AlgorithmVM>(selectedClient.GetControlAlgorithmNames()?.Select(x =>
            new AlgorithmVM(this) {
                Name = x,
                Interval = selectedClient?.GetControlAlgorithmRunIntervalMillis(x) ?? -1,
                State = selectedClient?.GetControlAlgorithmState(x)
            }));
        public AlgorithmVM SelectedAlgorithm { get => selectedAlgorithmVM; set => SetSelectedAlgorithmName(value); }
        public ICommand SwitchIsIsolatedCommand { get; }

        public ObservableCollection<string> ControlParameterNames => new ObservableCollection<string>(selectedClient.GetControlParameterNames(SelectedAlgorithm.Name).OrderBy(x => x));
        public string SelectedControlParameterName { get { return selectedControlParameterName; } set { SetSelectedControlParameterName(value); } }
        public object ControlParameter { get { return selectedClient.GetControlParameter(SelectedAlgorithm.Name, SelectedControlParameterName); } set { SetControlParameter(value); } }

        public ObservableCollection<string> ControlOutputNames => new ObservableCollection<string>(selectedClient.GetControlOutputNames(SelectedAlgorithm.Name).OrderBy(x => x));
        public string SelectedControlOutputName { get { return selectedControlOutputName; } set { SetSelectedControlOutputName(value); } }
        public object ControlOutput => PrintObjectIfComplexType(selectedClient.GetControlOutput(SelectedAlgorithm.Name, SelectedControlOutputName));

        public ICommand StartAlgorithmCommand { get; }
        public ICommand StopAlgorithmCommand { get; }
        public ICommand RestartAlgorithmCommand { get; }
        public ICommand PauseAlgorithmCommand { get; }
        public ICommand ResumeAlgorithmCommand { get; }

        public AlgorithmsViewModel()
        {
            SetSelectedClient(Clients.FirstOrDefault());

            SwitchIsIsolatedCommand = new RelayCommand(() => SwitchIsIsolated());
            StartAlgorithmCommand = new RelayCommand(() => StartAlgorithm(), CanStartAlgorithm);
            StopAlgorithmCommand = new RelayCommand(() => StopAlgorithm(), CanStopAlgorithm);
            RestartAlgorithmCommand = new RelayCommand(() => RestartAlgorithm(), CanRestartAlgorithm);
            PauseAlgorithmCommand = new RelayCommand(() => PauseAlgorithm(), CanPauseAlgorithm);
            ResumeAlgorithmCommand = new RelayCommand(() => ResumeAlgorithm(), CanResumeAlgorithm);
        }

        private void SetSelectedClient(ClientVM value)
        {
            if (value != null && selectedClientVM != value)
            {
                selectedClient = value.Client;
                selectedClientVM = value;
                RaisePropertyChanged(nameof(Clients));
                RaisePropertyChanged(nameof(Algorithms));
                SetSelectedAlgorithmName(Algorithms.FirstOrDefault());
            }
        }

        public void SwitchIsIsolated(string name = null)
        {
            if (name == null) selectedClient.SwitchIsIsolated();
            else clients.Single(x => x.Name == name).SwitchIsIsolated();
            RaisePropertyChanged(nameof(Clients));
        }

        private void SetSelectedAlgorithmName(AlgorithmVM value)
        {
            if (value != null && selectedAlgorithmVM != value)
            {
                selectedAlgorithmVM = value;
                RaisePropertyChanged(nameof(Algorithms));
                RaisePropertyChanged(nameof(ControlParameterNames));
                selectedControlParameterName = ControlParameterNames.FirstOrDefault();
                RaisePropertyChanged(nameof(SelectedControlParameterName));
                RaisePropertyChanged(nameof(ControlParameter));
                RaisePropertyChanged(nameof(ControlOutputNames));
                selectedControlOutputName = ControlOutputNames.FirstOrDefault();
                RaisePropertyChanged(nameof(SelectedControlOutputName));
                RaisePropertyChanged(nameof(ControlOutput));
            }
        }

        private void SetSelectedControlParameterName(string value)
        {
            if (value != null && selectedControlParameterName != value)
            {
                selectedControlParameterName = value;
                RaisePropertyChanged(nameof(ControlParameter));
            }
        }

        private void SetSelectedControlOutputName(string value)
        {
            if (value != null && selectedControlOutputName != value)
            {
                selectedControlOutputName = value;
                RaisePropertyChanged(nameof(ControlOutput));
            }
        }

        private void SetControlParameter(object value)
        {
            if (controlParameter != value)
            {
                controlParameter = value;

                selectedClient.SetControlParameter(SelectedAlgorithm.Name, SelectedControlParameterName, value);
            }
        }

        private bool CanStartAlgorithm()
        {
            return SelectedAlgorithm.State == "Initial State" || SelectedAlgorithm.State == "Stopped";
        }

        public void StartAlgorithm(string name = null)
        {
            selectedClient.StartAlgorithm(name ?? SelectedAlgorithm.Name);
            RaisePropertyChanged(nameof(Algorithms));
        }

        private bool CanStopAlgorithm()
        {
            return SelectedAlgorithm.State == "Running";
        }

        public void StopAlgorithm(string name = null)
        {
            selectedClient.StopAlgorithm(name ?? SelectedAlgorithm.Name);
            RaisePropertyChanged(nameof(Algorithms));
        }

        private bool CanRestartAlgorithm()
        {
            return SelectedAlgorithm.State == "Running";
        }

        public void RestartAlgorithm(string name = null)
        {
            selectedClient.RestartAlgorithm(name ?? SelectedAlgorithm.Name);
            RaisePropertyChanged(nameof(Algorithms));
        }

        private bool CanPauseAlgorithm()
        {
            return SelectedAlgorithm.State == "Running";
        }

        public void PauseAlgorithm(string name = null)
        {
            selectedClient.PauseAlgorithm(name ?? SelectedAlgorithm.Name);
            RaisePropertyChanged(nameof(Algorithms));
        }

        private bool CanResumeAlgorithm()
        {
            return SelectedAlgorithm.State == "Paused";
        }

        public void ResumeAlgorithm(string name = null)
        {
            selectedClient.ResumeAlgorithm(name ?? SelectedAlgorithm.Name);
            RaisePropertyChanged(nameof(Algorithms));
        }

        private object PrintObjectIfComplexType(object o)
        {
            return o == null ? "Null" :
                o.Equals("") ? "Empty String" :
                (o is ArrayList) ? "[" + string.Join(", ", (o as ArrayList).ToArray()) + "]" :
                o;
        }
    }
}
