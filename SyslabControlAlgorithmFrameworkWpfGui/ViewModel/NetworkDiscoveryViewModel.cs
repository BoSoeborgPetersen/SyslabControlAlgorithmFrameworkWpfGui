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
        private ExternalViewClient client1 = MyConfiguration.ExternalViewClient(1);
        private ExternalViewClient client2 = MyConfiguration.ExternalViewClient(2);
        private ExternalViewClient client3 = MyConfiguration.ExternalViewClient(3);
        private ExternalViewClient client4 = MyConfiguration.ExternalViewClient(4);

        public string Name1 => "Client (" + client1.Hostname + ")";
        public string Name2 => "Client (" + client2.Hostname + ")";
        public string Name3 => "Client (" + client3.Hostname + ")";
        public string Name4 => "Client (" + client4.Hostname + ")";

        public ObservableCollection<string> CurrentAddresses1 => new ObservableCollection<string>(client1.getCurrentAddresses());
        public ObservableCollection<string> CurrentAddresses2 => new ObservableCollection<string>(client2.getCurrentAddresses());
        public ObservableCollection<string> CurrentAddresses3 => new ObservableCollection<string>(client3.getCurrentAddresses());
        public ObservableCollection<string> CurrentAddresses4 => new ObservableCollection<string>(client4.getCurrentAddresses());

        public NetworkDiscoveryViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    RaisePropertyChanged(() => CurrentAddresses1);
                    RaisePropertyChanged(() => CurrentAddresses2);
                    RaisePropertyChanged(() => CurrentAddresses3);
                    RaisePropertyChanged(() => CurrentAddresses4);

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
