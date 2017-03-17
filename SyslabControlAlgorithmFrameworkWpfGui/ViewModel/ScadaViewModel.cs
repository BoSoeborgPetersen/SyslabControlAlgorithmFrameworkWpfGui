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
        private readonly IEnumerable<ExternalViewClient> clients = MyConfiguration.ExternalViewClients();

        public Dictionary<string, List<string>> ClientData =>
            clients.ToDictionary(x => "Client (" + x.Hostname + ")", x => x.GetPreviousRequests().OrderByDescending(y => y).ToList());

        public ScadaViewModel()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    RaisePropertyChanged(() => ClientData);

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
