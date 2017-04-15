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
    public class MessageExchangeViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();
        private ClientVM selectedClient1;
        private ClientVM selectedClient2;
        private string selectedMessage1;
        private string selectedMessage2;

        public ObservableCollection<ClientVM> Clients => new ObservableCollection<ClientVM>(clients.Select(x =>
            new ClientVM()
            {
                Client = x,
                Name = x.Name,
                Host = MyConfiguration.TranslateHostname(x.Hostname, x.Port),
                Port = x.Port,
                IsIsolated = x.IsIsolated
            }));
        public ClientVM SelectedClient1 { get { return selectedClient1; } set { SetSelectedClient1(value); } }
        public ClientVM SelectedClient2 { get { return selectedClient2; } set { SetSelectedClient2(value); } }

        public ObservableCollection<string> Messages1 => new ObservableCollection<string>(FilterMessages(selectedClient1.Client.GetPreviousMessages(), true));
        public ObservableCollection<string> Messages2 => new ObservableCollection<string>(FilterMessages(selectedClient2.Client.GetPreviousMessages(), false));
        public string SelectedMessage1 { get { return selectedMessage1; } set { SetSelectedMessage1(value); } }
        public string SelectedMessage2 { get { return selectedMessage2; } set { SetSelectedMessage2(value); } }
        public string FormattedMessage1 => FormatMessage(SelectedMessage1);
        public string FormattedMessage2 => FormatMessage(SelectedMessage2);

        public MessageExchangeViewModel()
        {
            selectedClient1 = Clients.FirstOrDefault();
            selectedClient2 = Clients.Skip(1).FirstOrDefault() ?? Clients.FirstOrDefault();
            selectedMessage1 = Messages1.FirstOrDefault();
            selectedMessage2 = Messages2.FirstOrDefault();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while(true)
                {
                    RaisePropertyChanged(() => Messages1);
                    RaisePropertyChanged(() => Messages2);
                    SelectedMessage1 = Messages1.SingleOrDefault(x => x == selectedMessage1) ?? Messages1.FirstOrDefault();
                    SelectedMessage2 = Messages2.SingleOrDefault(x => x == selectedMessage2) ?? Messages2.FirstOrDefault();
                    RaisePropertyChanged(() => FormattedMessage1);
                    RaisePropertyChanged(() => FormattedMessage2);

                    Thread.Sleep(10000);
                }
            }).Start();
        }

        private IEnumerable<string> FilterMessages(IEnumerable<string> messages, bool isClient1)
        {
            string hostnameOfOtherClient = MyConfiguration.TranslateHostname(!isClient1 ? selectedClient1.Host : selectedClient2.Host, !isClient1 ? selectedClient1.Port : selectedClient2.Port);
            return messages.Where(x => (x.Contains("Received [") && x.Contains("sender: " + hostnameOfOtherClient))
                || (x.Contains("Broadcast ["))
                || (x.Contains("Send [") && x.Contains("receiver: " + hostnameOfOtherClient)));
        }

        private string FormatMessage(string message)
        {
            message = Regex.Replace(message ?? "", "^([0-9]{2}:[0-9]{2}:[0-9]{2}) - ", "Time: $1\n");
            if (message.Contains("Received ["))
            {
                if (message.Contains("Message Received ["))
                {
                    message = Regex.Replace(message ?? "", "Message Received \\[sender: ([0-9\\.]+), message: (.+)\\]", "Message Received\nSender: $1\nMessage: \n$2");
                }
                else if (message.Contains("Data Received ["))
                {
                    message = Regex.Replace(message ?? "", "Data Received \\[sender: ([0-9\\.]+), data: \\[(.+)\\]\\]", "Data Received\nSender: $1\nData: \n$2");
                }
            }
            else if (message.Contains("Broadcast ["))
            {
                if (message.Contains("Message Broadcast ["))
                {
                    message = Regex.Replace(message ?? "", "Message Broadcast \\[sender: ([0-9\\.]+), message: (.+), receivers: \\[([0-9\\., ]+)\\]\\]", "Message Broadcast\nReceivers: $3\nMessage: \n$2");
                }
                else if (message.Contains("Data Broadcast ["))
                {
                    message = Regex.Replace(message ?? "", "Data Broadcast \\[sender: ([0-9\\.]+), data: \\[(.+)\\], receivers: \\[([0-9\\., ]+)\\]\\]", "Data Broadcast\nReceivers: $3\nData: \n$2");
                }
            }
            else if (message.Contains("Send ["))
            {
                if (message.Contains("Message Send ["))
                {
                    message = Regex.Replace(message ?? "", "Message Send \\[sender: ([0-9\\.]+), receiver: ([0-9\\.]+), message: (.+)\\]", "Message Send\nReceiver: $2\nMessage: \n$3");
                }
                else if (message.Contains("Data Send ["))
                {
                    message = Regex.Replace(message ?? "", "Data Send \\[sender: ([0-9\\.]+), receiver: ([0-9\\.]+), data: \\[(.+)\\]\\]", "Data Send\nReceiver: $2\nData: \n$3");
                }
            }
            return message;
        }

        private void SetSelectedClient1(ClientVM value)
        {
            if (value != null && selectedClient1 != value)
            {
                selectedClient1 = value;
                RaisePropertyChanged(nameof(Messages1));
                SetSelectedMessage1(Messages1.FirstOrDefault());
                RaisePropertyChanged(nameof(SelectedMessage1));
            }
        }

        private void SetSelectedClient2(ClientVM value)
        {
            if (value != null && selectedClient2 != value)
            {
                selectedClient2 = value;
                RaisePropertyChanged(nameof(Messages2));
                SetSelectedMessage2(Messages2.FirstOrDefault());
                RaisePropertyChanged(nameof(SelectedMessage2));
            }
        }

        private void SetSelectedMessage1(string value)
        {
            if (value != null && selectedMessage1 != value)
            {
                selectedMessage1 = value;
                RaisePropertyChanged(() => SelectedMessage1);
                RaisePropertyChanged(nameof(FormattedMessage1));
            }
        }

        private void SetSelectedMessage2(string value)
        {
            if (value != null && selectedMessage2 != value)
            {
                selectedMessage2 = value;
                RaisePropertyChanged(() => SelectedMessage2);
                RaisePropertyChanged(nameof(FormattedMessage2));
            }
        }
    }
}
