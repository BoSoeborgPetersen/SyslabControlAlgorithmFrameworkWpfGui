using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class ExternalViewViewModel : ViewModelBase
    {
        private IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();
        private ExternalViewClient selectedClient;

        private object controlParameter;
        private string selectedAlgorithmName;
        private string selectedControlParameterName;
        private string selectedControlOutputName;

        public ObservableCollection<ExternalViewClient> Clients => new ObservableCollection<ExternalViewClient>(clients);
        public ExternalViewClient SelectedClient { get { return selectedClient; } set { SetSelectedClient(value); } }

        public ObservableCollection<string> AlgorithmNames => new ObservableCollection<string>(selectedClient.getControlAlgorithmNames());
        public string SelectedAlgorithmName { get { return selectedAlgorithmName; } set { SetSelectedAlgorithmName(value); } }
        public int RunIntervalMillis => selectedClient.getControlAlgorithmRunIntervalMillis(SelectedAlgorithmName);

        public ObservableCollection<string> ControlParameterNames => new ObservableCollection<string>(selectedClient.getControlParameterNames(SelectedAlgorithmName).OrderBy(x => x));
        public string SelectedControlParameterName { get { return selectedControlParameterName; } set { SetSelectedControlParameterName(value); } }
        public object ControlParameter { get { return selectedClient.getControlParameter(SelectedAlgorithmName, SelectedControlParameterName); } set { SetControlParameter(value); } }

        public ObservableCollection<string> ControlOutputNames => new ObservableCollection<string>(selectedClient.getControlOutputNames(SelectedAlgorithmName).OrderBy(x => x));
        public string SelectedControlOutputName { get { return selectedControlOutputName; } set { SetSelectedControlOutputName(value); } }
        public object ControlOutput => printObjectIfComplexType(selectedClient.getControlOutput(SelectedAlgorithmName, SelectedControlOutputName));

        public ICommand StartAlgorithmCommand { get; }
        public ICommand StopAlgorithmCommand { get; }
        public ICommand RestartAlgorithmCommand { get; }
        public ICommand PauseAlgorithmCommand { get; }
        public ICommand ResumeAlgorithmCommand { get; }

        public ExternalViewViewModel()
        {
            SetSelectedClient(clients.FirstOrDefault());

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
                RaisePropertyChanged(nameof(AlgorithmNames));
                SetSelectedAlgorithmName(AlgorithmNames.FirstOrDefault());
            }
        }

        private void SetSelectedAlgorithmName(string value)
        {
            if (value != null && selectedAlgorithmName != value)
            {
                selectedAlgorithmName = value;
                RaisePropertyChanged(nameof(RunIntervalMillis));
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

                selectedClient.setControlParameter(SelectedAlgorithmName, SelectedControlParameterName, value);
            }
        }

        private void StartAlgorithm()
        {
            selectedClient.StartAlgorithm(SelectedAlgorithmName);
        }

        private void StopAlgorithm()
        {
            selectedClient.StopAlgorithm(SelectedAlgorithmName);
        }

        private void RestartAlgorithm()
        {
            selectedClient.RestartAlgorithm(SelectedAlgorithmName);
        }

        private void PauseAlgorithm()
        {
            selectedClient.PauseAlgorithm(SelectedAlgorithmName);
        }

        private void ResumeAlgorithm()
        {
            selectedClient.ResumeAlgorithm(SelectedAlgorithmName);
        }

        private object printObjectIfComplexType(object o)
        {
            return o == null ? "Null" : 
                o.Equals("") ? "Empty String" : 
                (o is ArrayList) ? "[" + string.Join(", ", (o as ArrayList).ToArray()) + "]" : 
                o;
        }
    }
}
