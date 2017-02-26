using GalaSoft.MvvmLight;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class GenericBasedViewModel : ViewModelBase
    {
        private List<GenericBasedClient> clients = new List<GenericBasedClient>(){
            GenericBasedClient.Instance("185.118.251.66", 9031),
            GenericBasedClient.Instance("185.118.251.66", 9032),
            GenericBasedClient.Instance("185.118.251.66", 9021),
            GenericBasedClient.Instance("185.118.251.66", 9022)};
        private GenericBasedClient selectedClient;

        private object control;
        private string selectedDeviceName;
        private string selectedResourceName;
        private string selectedControlName;

        public ObservableCollection<GenericBasedClient> Clients => new ObservableCollection<GenericBasedClient>(clients);
        public GenericBasedClient SelectedClient { get { return selectedClient; } set { SetSelectedClient(value); } }

        public ObservableCollection<string> DeviceNames => new ObservableCollection<string>(selectedClient.DeviceNames());
        public string SelectedDeviceName { get { return selectedDeviceName; } set { SetSelectedDeviceName(value); } }

        public ObservableCollection<string> ResourceNames => new ObservableCollection<string>(selectedClient.ResourceNames(SelectedDeviceName).Where(x => (x.Contains("get") || x.Contains("is") || x.Contains("has")) && !x.Contains("[") && !x.Contains("hashcode")).OrderBy(x => x));
        public string SelectedResourceName { get { return selectedResourceName; } set { SetSelectedResourceName(value); } }
        public object Resource { get { return printObject(selectedClient.Resource(SelectedDeviceName, SelectedResourceName)); } }

        public ObservableCollection<string> ControlNames => new ObservableCollection<string>(selectedClient.ResourceNames(SelectedDeviceName).Where(x => !x.Contains("get") && !x.Contains("is") && !x.Contains("has") && !x.Contains("[")).OrderBy(x => x));
        public string SelectedControlName { get { return selectedControlName; } set { SetSelectedControlName(value); } }
        public object Control { get { return selectedClient.Resource(SelectedDeviceName, SelectedControlName); } set { SetControl(value); } }

        public GenericBasedViewModel()
        {
            SetSelectedClient(clients.FirstOrDefault());
        }

        private void SetSelectedClient(GenericBasedClient value)
        {
            if (value != null && selectedClient != value)
            {
                selectedClient = value;
                RaisePropertyChanged(nameof(DeviceNames));
                SetSelectedDeviceName(DeviceNames.FirstOrDefault());
            }
        }

        private void SetSelectedDeviceName(string value)
        {
            if (value != null && selectedDeviceName != value)
            {
                selectedDeviceName = value;
                RaisePropertyChanged(nameof(ResourceNames));
                selectedResourceName = ResourceNames.FirstOrDefault();
                RaisePropertyChanged(nameof(SelectedResourceName));
                RaisePropertyChanged(nameof(Resource));
            }
        }

        private void SetSelectedResourceName(string value)
        {
            if (value != null && selectedResourceName != value)
            {
                selectedResourceName = value;
                RaisePropertyChanged(nameof(Resource));
            }
        }

        private void SetSelectedControlName(string value)
        {
            if (value != null && selectedControlName != value)
            {
                selectedControlName = value;
                RaisePropertyChanged(nameof(Control));
            }
        }

        // TODO
        private void SetControl(object value)
        {
            if (control != value)
            {
                control = value;

                selectedClient.Control(SelectedDeviceName, SelectedControlName, value);
            }
        }

        private string printObject(object o)
        {
            return o == null ? "Null" : o.Equals("") ? "Empty String" : !(o is ArrayList) ? o.ToString() : "[" + string.Join(", ", (o as ArrayList).ToArray()) + "]";
        }
    }
}
