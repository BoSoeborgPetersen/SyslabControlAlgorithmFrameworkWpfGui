using GalaSoft.MvvmLight;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class MessageExchangeViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();

        public Dictionary<string, List<string>> ClientData { get; } = new Dictionary<string, List<string>>();

        public MessageExchangeViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while(true)
                {
                    Update();
                    RaisePropertyChanged(() => ClientData);

                    Thread.Sleep(10000);
                }
            }).Start();
        }

        private void Update()
        {
            ClientData.Clear();
            var newClientData = clients.ToDictionary(x => "Client (" + x.Hostname + ")", x => x.GetPreviousMessages().OrderByDescending(y => y).ToList());
            foreach (var data in newClientData)
                ClientData.Add(data.Key, data.Value);
        }
    }
}
