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
    public class ServiceDiscoveryViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();

        public Dictionary<string, Tuple<IEnumerable<string>, IEnumerable<string>, IEnumerable<string>>> ClientData =>
            clients.ToDictionary(
                x => "Client (" + x.Hostname + ")",
                x => new Tuple<IEnumerable<string>, IEnumerable<string>, IEnumerable<string>>(x.GetTypes(), x.GetRoles(), x.GetServices()));

        public ServiceDiscoveryViewModel()
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
