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

        public Dictionary<AddressVM, List<AddressVM>> ClientData
        {
            get
            {
                var data = clients.ToDictionary(
                    x => new AddressVM() { Hostname = MyConfiguration.TranslateHostname(x.Hostname, x.Port), Name = x.Name, Port = x.Port },
                    x => x.GetCurrentAddresses().OrderBy(y => y.Equals("Error") ? 1 : int.Parse(y.Substring(y.LastIndexOf(".") + 1))).Select(
                        y => new AddressVM() { Hostname = y, Name = MyConfiguration.DeviceNameFromHostname(y), Port = MyConfiguration.PortFromHostname(y) }).ToList());

                foreach (var item in data)
                    item.Key.IsKnown = data.All(x => item.Key.Hostname.Equals(x.Key.Hostname) || x.Value.Any(y => y.Hostname.Equals(item.Key.Hostname)));

                foreach (var item in data)
                    foreach (var valueItem in item.Value)
                        valueItem.IsKnown = data.Any(x => x.Key.Hostname.Equals(valueItem.Hostname));

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
