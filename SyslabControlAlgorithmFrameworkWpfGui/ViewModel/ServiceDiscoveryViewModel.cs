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
        private ExternalViewClient client1 = MyConfiguration.ExternalViewClient(1);
        private ExternalViewClient client2 = MyConfiguration.ExternalViewClient(2);
        private ExternalViewClient client3 = MyConfiguration.ExternalViewClient(3);
        private ExternalViewClient client4 = MyConfiguration.ExternalViewClient(4);

        public ObservableCollection<string> Types1 => new ObservableCollection<string>(client1.getTypes());
        public ObservableCollection<string> Types2 => new ObservableCollection<string>(client2.getTypes());
        public ObservableCollection<string> Types3 => new ObservableCollection<string>(client3.getTypes());
        public ObservableCollection<string> Types4 => new ObservableCollection<string>(client4.getTypes());
        public ObservableCollection<string> Roles1 => new ObservableCollection<string>(client1.getRoles());
        public ObservableCollection<string> Roles2 => new ObservableCollection<string>(client2.getRoles());
        public ObservableCollection<string> Roles3 => new ObservableCollection<string>(client3.getRoles());
        public ObservableCollection<string> Roles4 => new ObservableCollection<string>(client4.getRoles());
        public ObservableCollection<string> Services1 => new ObservableCollection<string>(client1.getServices());
        public ObservableCollection<string> Services2 => new ObservableCollection<string>(client2.getServices());
        public ObservableCollection<string> Services3 => new ObservableCollection<string>(client3.getServices());
        public ObservableCollection<string> Services4 => new ObservableCollection<string>(client4.getServices());

        public ServiceDiscoveryViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    RaisePropertyChanged(() => Types1);
                    RaisePropertyChanged(() => Types2);
                    RaisePropertyChanged(() => Types3);
                    RaisePropertyChanged(() => Types4);
                    RaisePropertyChanged(() => Roles1);
                    RaisePropertyChanged(() => Roles2);
                    RaisePropertyChanged(() => Roles3);
                    RaisePropertyChanged(() => Roles4);
                    RaisePropertyChanged(() => Services1);
                    RaisePropertyChanged(() => Services2);
                    RaisePropertyChanged(() => Services3);
                    RaisePropertyChanged(() => Services4);

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
