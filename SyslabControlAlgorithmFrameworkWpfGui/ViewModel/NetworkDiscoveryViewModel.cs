using GalaSoft.MvvmLight;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using SyslabControlAlgorithmFrameworkWpfGui.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class NetworkDiscoveryViewModel : ViewModelBase
    {
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();

        public Dictionary<Header, IOrderedEnumerable<string>> ClientData
        {
            get
            {
                var data = clients.ToDictionary(x => new Header() { Hostname = x.Hostname, Title = x.DisplayName, Color = Brushes.Purple }, x => x.GetCurrentAddresses().OrderBy(y => y.Equals("Error") ? 1 : int.Parse(y.Substring(y.LastIndexOf(".") + 1))));

                foreach (var item in data)
                    item.Key.Color = data.All(x => item.Key.Hostname.Equals(x.Key.Hostname) || item.Value.Any(y => y.Equals(x.Key.Hostname))) ? Brushes.DarkGreen : Brushes.DarkRed;

                return data;
            }
        }

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
