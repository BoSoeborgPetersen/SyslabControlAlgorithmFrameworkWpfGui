using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SyslabControlAlgorithmFrameworkWpfGui.Controller;
using SyslabControlAlgorithmFrameworkWpfGui.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class PlottingViewModel : ViewModelBase
    {
        private double time = 0;

        private readonly IEnumerable<GenericBasedClient> genericBasedClients = MyConfiguration.GenericBasedClients();
        public PlotModel ClientData { get; }

        private readonly Dictionary<GenericBasedClient, CompositeMeasurement> activePowers = new Dictionary<GenericBasedClient, CompositeMeasurement>();
        private double TotalActivePower => activePowers.Sum(x => x.Value?.Value ?? 0);

        public PlottingViewModel()
        {
            ClientData = new PlotModel();

            foreach (var client in genericBasedClients)
            {
                ClientData.Series.Add(new LineSeries() { Title = client.DisplayName });
            }
            ClientData.Series.Add(new LineSeries() { Title = "Total" });

            ClientData.LegendBorder = OxyColors.Black;
            ClientData.LegendBackground = OxyColors.White;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    time++;

                    GetDataPoints();

                    RaisePropertyChanged(() => ClientData);

                    Thread.Sleep(1000);
                }
            }).Start();
        }

        public void GetDataPoints()
        {
            int i = 0;
            foreach (var client in genericBasedClients)
            {
                var activePower = (CompositeMeasurement)client.Resource("", "getACActivePower");
                if (activePowers.ContainsKey(client)) activePowers.Remove(client);
                activePowers.Add(client, activePower);
                if (activePower != null)
                {
                    var series = ClientData.Series[i] as LineSeries;
                    series.Points.Add(new DataPoint(time, activePower.Value));
                    if (series.Points.Count > 60) series.Points.RemoveAt(0);
                }

                i++;
            }

            var totalSeries = ClientData.Series[i] as LineSeries;
            totalSeries.Points.Add(new DataPoint(time, TotalActivePower));
            if (totalSeries.Points.Count > 60) totalSeries.Points.RemoveAt(0);

            ClientData.InvalidatePlot(true);
        }
    }
}
