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
        private ExternalViewClient client1 = MyConfiguration.ExternalViewClient(1);
        private ExternalViewClient client2 = MyConfiguration.ExternalViewClient(2);
        private ExternalViewClient client3 = MyConfiguration.ExternalViewClient(3);
        private ExternalViewClient client4 = MyConfiguration.ExternalViewClient(4);

        public ObservableCollection<string> PreviousMessages1 => new ObservableCollection<string>(client1.getPreviousMessages());
        public ObservableCollection<string> PreviousMessages2 => new ObservableCollection<string>(client2.getPreviousMessages());
        public ObservableCollection<string> PreviousMessages3 => new ObservableCollection<string>(client3.getPreviousMessages());
        public ObservableCollection<string> PreviousMessages4 => new ObservableCollection<string>(client4.getPreviousMessages());

        public MessageExchangeViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while(true)
                {
                    RaisePropertyChanged(() => PreviousMessages1);
                    RaisePropertyChanged(() => PreviousMessages2);
                    RaisePropertyChanged(() => PreviousMessages3);
                    RaisePropertyChanged(() => PreviousMessages4);

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
