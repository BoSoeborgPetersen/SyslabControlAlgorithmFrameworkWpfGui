using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class ExternalViewViewModel : ViewModelBase
    {
        private ExternalViewClient client = ExternalViewClient.Instance;

        private object controlParameter;
        private string selectedAlgorithmName;
        private string selectedControlParameterName;
        private string selectedControlOutputName;

        public ObservableCollection<string> AlgorithmNames => new ObservableCollection<string>(client.getControlAlgorithmNames());
        public string SelectedAlgorithmName { get { return selectedAlgorithmName; } set { setSelectedAlgorithmName(value); } }
        public int RunIntervalMillis => client.getControlAlgorithmRunIntervalMillis(SelectedAlgorithmName);

        public ObservableCollection<string> ControlParameterNames => new ObservableCollection<string>(client.getControlParameterNames(SelectedAlgorithmName));
        public string SelectedControlParameterName { get { return selectedControlParameterName; } set { setSelectedControlParameterNames(value); } }
        public object ControlParameter { get { return client.getControlParameter(SelectedAlgorithmName, SelectedControlParameterName); } set { setControlParameter(value); } }

        public ObservableCollection<string> ControlOutputNames => new ObservableCollection<string>(client.getControlOutputNames(SelectedAlgorithmName));
        public string SelectedControlOutputName { get { return selectedControlOutputName; } set { setSelectedControlOutputName(value); } }
        public object ControlOutput => client.getControlOutput(SelectedAlgorithmName, ControlOutputNames[0]);

        public ICommand StartAlgorithmCommand { get; }
        public ICommand StopAlgorithmCommand { get; }
        public ICommand RestartAlgorithmCommand { get; }
        public ICommand PauseAlgorithmCommand { get; }
        public ICommand ResumeAlgorithmCommand { get; }

        public ExternalViewViewModel()
        {
            selectedAlgorithmName = AlgorithmNames.FirstOrDefault();

            //ControlParameterNames = new ObservableCollection<string>(client.getControlParameterNames(AlgorithmNames[0]));
            selectedControlParameterName = ControlParameterNames.FirstOrDefault();
            //controlParameter = client.getControlParameter(AlgorithmNames[0], ControlParameterNames[0]);

            //ControlOutputNames = new ObservableCollection<string>(client.getControlOutputNames(AlgorithmNames[0]));
            selectedControlOutputName = ControlOutputNames.FirstOrDefault();
            //ControlOutput = client.getControlOutput(AlgorithmNames[0], ControlOutputNames[0]);

            StartAlgorithmCommand = new RelayCommand(StartAlgorithm);
            StopAlgorithmCommand = new RelayCommand(StopAlgorithm);
            RestartAlgorithmCommand = new RelayCommand(RestartAlgorithm);
            PauseAlgorithmCommand = new RelayCommand(PauseAlgorithm);
            ResumeAlgorithmCommand = new RelayCommand(ResumeAlgorithm);
        }

        private void setSelectedAlgorithmName(string value)
        {
            if (value != null && selectedAlgorithmName != value)
            {
                selectedAlgorithmName = value;
                RaisePropertyChanged(nameof(RunIntervalMillis));
                RaisePropertyChanged(nameof(ControlParameterNames));
                RaisePropertyChanged(nameof(ControlOutputNames));
            }
        }

        private void setSelectedControlParameterNames(string value)
        {
            if (value != null && selectedControlParameterName != value)
            {
                selectedControlParameterName = value;
                RaisePropertyChanged(nameof(ControlParameter));
            }
        }

        private void setSelectedControlOutputName(string value)
        {
            if (value != null && selectedControlOutputName != value)
            {
                selectedControlOutputName = value;
                RaisePropertyChanged(nameof(ControlOutput));
            }
        }

        private void setControlParameter(object value)
        {
            if (controlParameter != value)
            {
                controlParameter = value;

                client.setControlParameter(SelectedAlgorithmName, SelectedControlParameterName, value);
            }
        }

        private void StartAlgorithm()
        {
            client.StartAlgorithm(SelectedAlgorithmName);
        }

        private void StopAlgorithm()
        {
            client.StopAlgorithm(SelectedAlgorithmName);
        }

        private void RestartAlgorithm()
        {
            client.RestartAlgorithm(SelectedAlgorithmName);
        }

        private void PauseAlgorithm()
        {
            client.PauseAlgorithm(SelectedAlgorithmName);
        }

        private void ResumeAlgorithm()
        {
            client.ResumeAlgorithm(SelectedAlgorithmName);
        }
    }
}
