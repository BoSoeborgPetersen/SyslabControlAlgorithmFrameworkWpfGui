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
        private ExternalViewClient client = ExternalViewClient.Instance;

        public ObservableCollection<string> PreviousMessages => new ObservableCollection<string>(client.getPreviousMessages());

        public MessageExchangeViewModel()
        {

        }
    }
}
