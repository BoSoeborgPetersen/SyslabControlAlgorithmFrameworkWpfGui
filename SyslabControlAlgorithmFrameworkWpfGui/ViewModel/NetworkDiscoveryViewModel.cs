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
    public class NetworkDiscoveryViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();

        public Dictionary<string, IOrderedEnumerable<string>> ClientData => clients.ToDictionary(x => "Client (" + x.Hostname + ")", x => x.GetCurrentAddresses().OrderBy(y => y.Equals("Error") ? 1 : int.Parse(y.Substring(y.LastIndexOf(".") + 1))));

        public NetworkDiscoveryViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    RaisePropertyChanged(() => ClientData);

                    Thread.Sleep(10000);
                }
            }).Start();
        }
    }
}
