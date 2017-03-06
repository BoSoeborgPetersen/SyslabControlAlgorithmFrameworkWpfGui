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
    public class ScadaViewModel : ViewModelBase
    {
        private ExternalViewClient client1 = MyConfiguration.ExternalViewClient(1);
        private ExternalViewClient client2 = MyConfiguration.ExternalViewClient(2);
        private ExternalViewClient client3 = MyConfiguration.ExternalViewClient(3);
        private ExternalViewClient client4 = MyConfiguration.ExternalViewClient(4);

        public ObservableCollection<string> PreviousRequests1 => new ObservableCollection<string>(client1.getPreviousRequests());
        public ObservableCollection<string> PreviousRequests2 => new ObservableCollection<string>(client2.getPreviousRequests());
        public ObservableCollection<string> PreviousRequests3 => new ObservableCollection<string>(client3.getPreviousRequests());
        public ObservableCollection<string> PreviousRequests4 => new ObservableCollection<string>(client4.getPreviousRequests());

        public ScadaViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    RaisePropertyChanged(() => PreviousRequests1);
                    RaisePropertyChanged(() => PreviousRequests2);
                    RaisePropertyChanged(() => PreviousRequests3);
                    RaisePropertyChanged(() => PreviousRequests4);

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
