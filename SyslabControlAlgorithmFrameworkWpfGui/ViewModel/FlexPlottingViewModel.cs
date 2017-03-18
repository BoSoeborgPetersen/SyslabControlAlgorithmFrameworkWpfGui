﻿using GalaSoft.MvvmLight;
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
    public class FlexPlottingViewModel : ViewModelBase
    {
        private double time = 0;

        private readonly ExternalViewClient externalViewClient = MyConfiguration.ExternalViewClients().SingleOrDefault(x => x.Hostname.Equals("10.42.241.5"));

        private readonly IEnumerable<GenericBasedClient> genericBasedClients = MyConfiguration.GenericBasedClients();
        public Dictionary<string, PlotModel> ClientData { get; } = new Dictionary<string, PlotModel>();

        private readonly Dictionary<GenericBasedClient, CompositeMeasurement> activePowers = new Dictionary<GenericBasedClient, CompositeMeasurement>();
        public double TotalActivePower => activePowers.Sum(x => x.Value?.Value ?? 0);

        public FlexPlottingViewModel()
        {
            ClientData = genericBasedClients.ToDictionary(x => x.DisplayName, x =>
            {
                var model = new PlotModel();
                model.Series.Add(new LineSeries());
                model.Series.Add(new LineSeries());
                //model.Axes.Add(new OxyPlot.Axes.LinearAxis()
                //{
                //    Position = AxisPosition.Bottom,
                //    MaximumRange = 60,
                //    Key = "xAxis",
                //    MajorGridlineStyle = LineStyle.Solid,
                //    MinorGridlineStyle = LineStyle.Dot,
                //});
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

            int i = 0;
            foreach (var client in genericBasedClients)
            {
                var activePower = (CompositeMeasurement)client.Resource("", "getACActivePower");
                if (activePowers.ContainsKey(client)) activePowers.Remove(client);
                activePowers.Add(client, activePower);
                if (activePower != null)
                {
                    var series1 = ClientData.ElementAt(i).Value.Series[0] as LineSeries;
                    series1.Points.Add(new DataPoint(time, activePower.Value));
                    if (series1.Points.Count > 60) series1.Points.RemoveAt(0);
                }
                //double setpoint = client.Hostname.Equals("10.42.241.5") ? (double)(externalViewClient.getControlOutput("DumploadControlAlgorithm", "setP") ?? default(double)) :
                //    (double)(externalViewClient.getControlOutput("FlexibilityAlgorithm", "setP1") ?? default(double));
                const double setpoint = 0;

                var series2 = ClientData.ElementAt(i).Value.Series[1] as LineSeries;
                series2.Points.Add(new DataPoint(time, setpoint));
                if (series2.Points.Count > 60) series2.Points.RemoveAt(0);

                ClientData.ElementAt(i).Value.InvalidatePlot(true);

                i++;
            }
        }
    }
}
