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
    public class NetworkDiscoveryViewModel : ViewModelBase
    {
        private ExternalViewClient client = ExternalViewClient.Instance;

        public ObservableCollection<string> CurrentAddresses => new ObservableCollection<string>(client.getCurrentAddresses());

        public NetworkDiscoveryViewModel()
        {

        }
    }
}
