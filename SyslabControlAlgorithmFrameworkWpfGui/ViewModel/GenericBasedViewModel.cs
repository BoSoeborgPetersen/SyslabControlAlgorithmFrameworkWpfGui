using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class GenericBasedViewModel : ViewModelBase
    {
        private readonly IEnumerable<GenericBasedClient> clients = MyConfiguration.GenericBasedClients();
        private GenericBasedClient selectedClient;

        //private object control;
        private string selectedDeviceName;
        private string selectedResourceName;
        private string selectedControlName;

        public ObservableCollection<GenericBasedClient> Clients => new ObservableCollection<GenericBasedClient>(clients);
        public GenericBasedClient SelectedClient { get { return selectedClient; } set { SetSelectedClient(value); } }

        public ObservableCollection<string> DeviceNames => new ObservableCollection<string>(selectedClient.DeviceNames());
        public string SelectedDeviceName { get { return selectedDeviceName; } set { SetSelectedDeviceName(value); } }

        public ObservableCollection<string> ResourceNames => new ObservableCollection<string>(selectedClient.ResourceNames(SelectedDeviceName).Where(x => (x.Contains("get") || x.Contains("is") || x.Contains("has") || x.Contains("can")) && !x.Contains("[") && !x.Contains("hashcode") && !x.Contains("getClass")).OrderBy(x => x));
        public string SelectedResourceName { get { return selectedResourceName; } set { SetSelectedResourceName(value); } }
        public object Resource { get { return PrintObject(selectedClient.Resource(SelectedDeviceName, SelectedResourceName)); } }

        public ObservableCollection<string> ControlNames => new ObservableCollection<string>(selectedClient.ResourceNames(SelectedDeviceName).Where(x => !x.Contains("get") && !x.Contains("is") && !x.Contains("has") && !x.Contains("can") && !x.Contains("notify") && !x.Contains("toString") && !x.Contains("wait")).OrderBy(x => x));
        public string SelectedControlName { get => selectedControlName; set => SetSelectedControlName(value); }

        private List<Type> parameterTypes;
        private bool HasParam1 => (selectedControlName?.Contains("[") ?? false);
        public Visibility Param1Visibility => HasParam1 ? Visibility.Visible : Visibility.Collapsed;
        private bool HasParam2 => (selectedControlName?.Contains(",") ?? false);
        public Visibility Param2Visibility => HasParam2 ? Visibility.Visible : Visibility.Collapsed;
        public ICommand ControlCommand { get; }

        public GenericBasedViewModel()
        {
            SetSelectedClient(clients.FirstOrDefault());
            RaisePropertyChanged(nameof(SelectedClient));

            ControlCommand = new RelayCommand<string>(Control, CanControl);
        }

        private void SetSelectedClient(GenericBasedClient value)
        {
            if (value != null && selectedClient != value)
            {
                selectedClient = value;
                RaisePropertyChanged(nameof(DeviceNames));
                SetSelectedDeviceName(DeviceNames.FirstOrDefault());
                RaisePropertyChanged(nameof(SelectedDeviceName));
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

                selectedControlName = ControlNames.FirstOrDefault();
                RaisePropertyChanged(nameof(SelectedControlName));

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
                
                if (selectedControlName.Contains("[") && selectedControlName.Contains("]"))
                {
                    var startArrayIndex = selectedControlName.IndexOf("[") + 1;
                    var endArrayIndex = selectedControlName.IndexOf("]") - 1;
                    var paramsLength = endArrayIndex - startArrayIndex + 1;
                    var paramsNames = selectedControlName.Substring(startArrayIndex, paramsLength);
                    var paramsNamesList = paramsNames.Split(',');
                    parameterTypes = paramsNamesList.Select(x => GetTypeByName(x)).ToList();
                }

                RaisePropertyChanged(nameof(Param1Visibility));
                RaisePropertyChanged(nameof(Param2Visibility));
            }
        }

        private bool CanControl(string param1)
        {
            return !selectedControlName?.Contains(",") ?? false;
        }

        private void Control(string param1)
        {
            if (selectedControlName.Contains(",")) return;
            if (selectedControlName.Contains("[") && string.IsNullOrWhiteSpace(param1)) return;

            if (HasParam1 && !string.IsNullOrWhiteSpace(param1))
            {
                if (parameterTypes == null || parameterTypes.Count < 1 || parameterTypes[0] == null) return;

                var value = Convert.ChangeType(param1, parameterTypes[0], CultureInfo.InvariantCulture);

                //var parameters = new List<object>();
                //parameters.Add(value);

                var newSelectedControlName = selectedControlName.Substring(0, selectedControlName.IndexOf("["));
                selectedClient.Control(SelectedDeviceName, newSelectedControlName, value);
            }
            else
            {
                selectedClient.Control(SelectedDeviceName, selectedControlName);
            }
        }

        private string PrintObject(object o)
        {
            if (o is string) return o.ToString();
            if (o is ArrayList) return "[" + string.Join(", ", (o as ArrayList).ToArray()) + "]";
            if (o is IEnumerable) return "[" + string.Join(", ", ((IEnumerable<Object>)o).ToArray()) + "]";
            return o == null ? "Null" : o.Equals("") ? "Empty String" : o.ToString();
        }
        private static Type GetTypeByName(string className)
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(var assemblyType in a.GetTypes())
                {
                    if (assemblyType.Name.Equals(className, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return assemblyType;
                    }
                }
            }

            return null;
        }
    }
}
