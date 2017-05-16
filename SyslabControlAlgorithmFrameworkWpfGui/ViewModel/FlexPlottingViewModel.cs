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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.ViewModel
{
    public class FlexPlottingViewModel : ViewModelBase
    {
        private double time = 0;

        private readonly IEnumerable<ExternalViewClient> externalViewClients = MyConfiguration.ExternalViewClients();

        private readonly IEnumerable<GenericBasedClient> genericBasedClients = MyConfiguration.GenericBasedClients();
        public Dictionary<string, PlotModel> ClientData { get; } = new Dictionary<string, PlotModel>();

        private readonly Dictionary<GenericBasedClient, CompositeMeasurement> activePowers = new Dictionary<GenericBasedClient, CompositeMeasurement>();
        public double TotalActivePower => activePowers.ToList().Sum(x => x.Value?.Value ?? 0);

        public FlexPlottingViewModel()
        {
            ClientData = genericBasedClients.ToDictionary(x => x.DisplayName, x =>
            {
                var model = new PlotModel();
                model.Series.Add(new LineSeries());
                model.Series.Add(new LineSeries());
                model.Axes.Add(new LinearAxis()
                {
                    Position = AxisPosition.Left,
                    MinimumRange = 10,
                    Key = "yAxis",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                });
                return model;
            });

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    time++;

                    GetDataPoints();

                    RaisePropertyChanged(() => ClientData);
                    RaisePropertyChanged(() => TotalActivePower);

                    Thread.Sleep(1000);
                }
            }).Start();
        }

        public void GetDataPoints()
        {
            //genericBasedClients.Single(x => x.Hostname.Equals("10.42.241.7")).Control("-", "setPacLimit", 10);
            //genericBasedClients.Single(x => x.Hostname.Equals("10.42.241.10")).Control("-", "setPacLimit", 10);
            //genericBasedClients.Single(x => x.Hostname.Equals("10.42.241.24")).Control("-", "setPacLimit", 10);

            int j = 0;
            foreach (var client in genericBasedClients)
            {
                var activePower = (CompositeMeasurement)client.Resource("", "getACActivePower");
                if (activePowers.ContainsKey(client)) activePowers.Remove(client);
                activePowers.Add(client, activePower);

                var series1 = ClientData.ElementAt(j).Value.Series[0] as LineSeries;
                series1.Points.Add(new DataPoint(time, activePower?.Value ?? 0));
                if (series1.Points.Count > 60) series1.Points.RemoveAt(0);

                j++;
            }

            j = 0;
            foreach (var client in externalViewClients)
            {
                if (client.Hostname.Equals("10.42.241.5"))
                {
                    double? setpoint = -1 * (double?)(client.GetControlOutput("DumploadControlAlgorithm", "setP_10.42.241.5"));

                    if (setpoint != null)
                    {
                        var series2 = ClientData.ElementAt(j).Value.Series[1] as LineSeries;
                        series2.Points.Add(new DataPoint(time, setpoint.Value));
                        if (series2.Points.Count > 60) series2.Points.RemoveAt(0);
                    }
                }
                else
                {
                    double? setpoint;
                    FlexibilityExecutions flex = (FlexibilityExecutions)(client.GetControlOutput("FlexibilityDecentrilizedMEAlgorithm", "FlexibilityExecution"));
                    setpoint = flex?.Duration == 0 ? 0 : 1000 * flex?.Kwh / flex?.Duration;

                    if (flex != null)
                    {
                        //var series2 = ClientData.ElementAt(i).Value.Series[1] as BarSeries;
                        //series2.Items.Add(new BarItem(setpoint.Value));
                        var series2 = ClientData.ElementAt(j).Value.Series[1] as LineSeries;
                        series2.Points.Add(new DataPoint(time, setpoint.Value));
                        if (series2.Points.Count > 60) series2.Points.RemoveAt(0);
                    }
                }

                j++;
            }


            for(int i = 0; i < genericBasedClients.Count(); i++)
            {
                // Update axis range to have 0 in the middle.
                var yAxis = ClientData.ElementAt(i).Value.Axes[0] as LinearAxis;
                if (Math.Abs(yAxis.ActualMaximum) > Math.Abs(yAxis.ActualMinimum))
                    yAxis.Minimum = (-1) * yAxis.ActualMaximum;
                else if (Math.Abs(yAxis.ActualMaximum) < Math.Abs(yAxis.ActualMinimum))
                    yAxis.Maximum = (-1) * yAxis.ActualMinimum;
                else if (yAxis.DataMinimum < yAxis.Minimum)
                    yAxis.Minimum = yAxis.DataMinimum - 1;
                else if (yAxis.DataMaximum > yAxis.Maximum)
                    yAxis.Maximum = yAxis.DataMaximum + 1;

                ClientData.ElementAt(i).Value.InvalidatePlot(true);
            }
        }
    }
}
