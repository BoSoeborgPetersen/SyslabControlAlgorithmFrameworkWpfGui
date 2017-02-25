using GalaSoft.MvvmLight;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class MessageExchangeViewModel : ViewModelBase
    {
        private ExternalViewClient client1 = ExternalViewClient.Instance("185.118.251.66", 5531);
        private ExternalViewClient client2 = ExternalViewClient.Instance("185.118.251.66", 5532);
        private ExternalViewClient client3 = ExternalViewClient.Instance("185.118.251.66", 5521);
        private ExternalViewClient client4 = ExternalViewClient.Instance("185.118.251.66", 5522);

        public ObservableCollection<string> PreviousMessages1 => new ObservableCollection<string>(client1.getPreviousMessages());
        public ObservableCollection<string> PreviousMessages2 => new ObservableCollection<string>(client2.getPreviousMessages());
        public ObservableCollection<string> PreviousMessages3 => new ObservableCollection<string>(client3.getPreviousMessages());
        public ObservableCollection<string> PreviousMessages4 => new ObservableCollection<string>(client4.getPreviousMessages());

        public MessageExchangeViewModel()
        {

        }
    }
}
