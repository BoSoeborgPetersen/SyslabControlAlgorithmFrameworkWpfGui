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
        private ExternalViewClient client1 = ExternalViewClient.Instance("185.118.251.66", 5531);
        private ExternalViewClient client2 = ExternalViewClient.Instance("185.118.251.66", 5532);
        private ExternalViewClient client3 = ExternalViewClient.Instance("185.118.251.66", 5521);
        private ExternalViewClient client4 = ExternalViewClient.Instance("185.118.251.66", 5522);

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
