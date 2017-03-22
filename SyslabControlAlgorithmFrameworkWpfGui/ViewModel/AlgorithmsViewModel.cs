using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model;
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

        private object controlParameter;
        private DescriptiveName selectedAlgorithmName;
        private string selectedControlParameterName;
        private string selectedControlOutputName;

        public ObservableCollection<ExternalViewClient> Clients => new ObservableCollection<ExternalViewClient>(clients);
        public ExternalViewClient SelectedClient { get { return selectedClient; } set { SetSelectedClient(value); } }

        public ObservableCollection<DescriptiveName> AlgorithmNames => new ObservableCollection<DescriptiveName>(selectedClient.GetControlAlgorithmNames()?.Select(x => new DescriptiveName() { Name = x, DisplayName = x + " [Interval: " + selectedClient?.GetControlAlgorithmRunIntervalMillis(x) + "]", Color = (selectedClient.GetControlAlgorithmState(x)?.Equals("Running") ?? false) ? Brushes.DarkGreen : Brushes.DarkRed }));
        public DescriptiveName SelectedAlgorithmName { get { return selectedAlgorithmName; } set { SetSelectedAlgorithmName(value); } }
        //public int RunIntervalMillis => selectedClient.GetControlAlgorithmRunIntervalMillis(SelectedAlgorithmName.Name);
        public string AlgorithmState => selectedClient.GetControlAlgorithmState(SelectedAlgorithmName.Name);
        public bool IsIsolated => selectedClient.IsIsolated;
        public ICommand SwitchIsIsolatedCommand { get; }

        public ObservableCollection<string> ControlParameterNames => new ObservableCollection<string>(selectedClient.GetControlParameterNames(SelectedAlgorithmName.Name).OrderBy(x => x));
        public string SelectedControlParameterName { get { return selectedControlParameterName; } set { SetSelectedControlParameterName(value); } }
        public object ControlParameter { get { return selectedClient.GetControlParameter(SelectedAlgorithmName.Name, SelectedControlParameterName); } set { SetControlParameter(value); } }

        public ObservableCollection<string> ControlOutputNames => new ObservableCollection<string>(selectedClient.GetControlOutputNames(SelectedAlgorithmName.Name).OrderBy(x => x));
        public string SelectedControlOutputName { get { return selectedControlOutputName; } set { SetSelectedControlOutputName(value); } }
        public object ControlOutput => PrintObjectIfComplexType(selectedClient.GetControlOutput(SelectedAlgorithmName.Name, SelectedControlOutputName));

        public ICommand StartAlgorithmCommand { get; }
        public ICommand StopAlgorithmCommand { get; }
        public ICommand RestartAlgorithmCommand { get; }
        public ICommand PauseAlgorithmCommand { get; }
        public ICommand ResumeAlgorithmCommand { get; }

        public AlgorithmsViewModel()
        {
            SetSelectedClient(clients.FirstOrDefault());

            SwitchIsIsolatedCommand = new RelayCommand(SwitchIsIsolated);
            StartAlgorithmCommand = new RelayCommand(StartAlgorithm);
            StopAlgorithmCommand = new RelayCommand(StopAlgorithm);
            RestartAlgorithmCommand = new RelayCommand(RestartAlgorithm);
            PauseAlgorithmCommand = new RelayCommand(PauseAlgorithm);
            ResumeAlgorithmCommand = new RelayCommand(ResumeAlgorithm);
        }

        private void SetSelectedClient(ExternalViewClient value)
        {
            if (value != null && selectedClient != value)
            {
                selectedClient = value;
                RaisePropertyChanged(nameof(IsIsolated));
                RaisePropertyChanged(nameof(AlgorithmNames));
                SetSelectedAlgorithmName(AlgorithmNames.FirstOrDefault());
            }
        }

        private void SwitchIsIsolated()
        {
            selectedClient.SwitchIsIsolated();
            RaisePropertyChanged(nameof(IsIsolated));
        }

        private void SetSelectedAlgorithmName(DescriptiveName value)
        {
            if (value != null && selectedAlgorithmName != value)
            {
                selectedAlgorithmName = value;
                RaisePropertyChanged(nameof(AlgorithmState));
                //RaisePropertyChanged(nameof(RunIntervalMillis));
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

                selectedClient.SetControlParameter(SelectedAlgorithmName.Name, SelectedControlParameterName, value);
            }
        }

        private void StartAlgorithm()
        {
            selectedClient.StartAlgorithm(SelectedAlgorithmName.Name);
        }

        private void StopAlgorithm()
        {
            selectedClient.StopAlgorithm(SelectedAlgorithmName.Name);
        }

        private void RestartAlgorithm()
        {
            selectedClient.RestartAlgorithm(SelectedAlgorithmName.Name);
        }

        private void PauseAlgorithm()
        {
            selectedClient.PauseAlgorithm(SelectedAlgorithmName.Name);
        }

        private void ResumeAlgorithm()
        {
            selectedClient.ResumeAlgorithm(SelectedAlgorithmName.Name);
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
