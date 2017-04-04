using GalaSoft.MvvmLight;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class ScadaViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();
        private ClientVM selectedClient;
        private string selectedRequest;

        public ObservableCollection<ClientVM> Clients => new ObservableCollection<ClientVM>(clients.Select(x =>
            new ClientVM()
            {
                Client = x,
                Name = x.Name,
                Host = MyConfiguration.TranslateHostname(x.Hostname, x.Port),
                Port = x.Port,
                IsIsolated = x.IsIsolated
            }));
        public ClientVM SelectedClient { get => selectedClient; set => SetSelectedClient(value); }

        public ObservableCollection<string> Requests => new ObservableCollection<string>(selectedClient.Client.GetPreviousRequests());
        public string SelectedRequest { get { return selectedRequest; } set { SetSelectedRequest(value); } }
        public string SelectedFormattedRequestHead => FormatRequestHead(SelectedRequest);
        public string SelectedFormattedRequestArgs => FormatRequestArgs(SelectedRequest);
        public string SelectedFormattedRequestResult => FormatRequestResult(SelectedRequest);

        public ScadaViewModel()
        {
            selectedClient = Clients.FirstOrDefault();
            selectedRequest = Requests.FirstOrDefault();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    RaisePropertyChanged(() => Requests);
                    SelectedRequest = Requests.SingleOrDefault(x => x == selectedRequest) ?? Requests.FirstOrDefault();

                    Thread.Sleep(10000);
                }
            }).Start();
        }

        private string FormatRequestHead(string request)
        {
            var head = Regex.Replace(request ?? "", "^([0-9]{2}:[0-9]{2}:[0-9]{2}) - Request \\[hostname: ([0-9\\.]+), deviceName: ([^,]+), resourceName: ([^,]+),.+", "Time: $1\nHostname: $2\nDeviceName: $3\nResourceName: $4");
            if (String.IsNullOrWhiteSpace(head)) return "No head";
            return head;
        }

        private string FormatRequestArgs(string request)
        {
            var arguments = Regex.Replace(request ?? "", ".+, returnValue: (.*), args: \\[(.*)\\]\\]$", "$2");
            if (String.IsNullOrWhiteSpace(arguments)) return "No arguments";
            return arguments;
        }

        private string FormatRequestResult(string request)
        {
            string result = Regex.Replace(request ?? "", ".+, returnValue: (.*), args: \\[(.*)\\]\\]$", "$1");
            if (result.Contains("["))
            {
                var typeName = Regex.Replace(result, "(.*) \\[(.*)\\]", "$1");
                var content = Regex.Replace(result, "(.*) \\[(.*)\\]", "$2");
                var contentList = content.Split(',');

                return typeName + "\n[" + contentList.Aggregate("", (l, x) => l + "\n  " + x.Trim() + ",") + "\n]";
            }
            else
            {
                if (String.IsNullOrWhiteSpace(result)) return "No result";
                return result;
            }
        }

        private void SetSelectedClient(ClientVM value)
        {
            if (value != null && selectedClient != value)
            {
                selectedClient = value;
                RaisePropertyChanged(nameof(Requests));
                SetSelectedRequest(Requests.FirstOrDefault());
                RaisePropertyChanged(nameof(SelectedRequest));
            }
        }

        private void SetSelectedRequest(string value)
        {
            if (value != null && selectedRequest != value)
            {
                selectedRequest = value;
                RaisePropertyChanged(nameof(SelectedFormattedRequestHead));
                RaisePropertyChanged(nameof(SelectedFormattedRequestArgs));
                RaisePropertyChanged(nameof(SelectedFormattedRequestResult));
            }
        }
    }
}
