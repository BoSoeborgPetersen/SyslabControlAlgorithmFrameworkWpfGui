﻿using GalaSoft.MvvmLight;
using OxyPlot;
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

        private ExternalViewClient externalViewClient = MyConfiguration.ExternalViewClients().SingleOrDefault(x => x.Hostname.Equals("10.42.241.5"));

        private IEnumerable<GenericBasedClient> genericBasedClients = MyConfiguration.GenericBasedClients();
        public Dictionary<string, PlotModel> ClientData { get; private set; } = new Dictionary<string, PlotModel>();

        private Dictionary<GenericBasedClient, CompositeMeasurement> activePowers = new Dictionary<GenericBasedClient, CompositeMeasurement>();
        public double TotalActivePower => activePowers.Sum(x => x.Value?.value ?? 0);

        public PlottingViewModel()
        {
            ClientData = genericBasedClients.ToDictionary(x => "Client (" + x.Hostname + ")", x =>
            {
                var model = new PlotModel();
                model.Series.Add(new LineSeries());
                model.Series.Add(new LineSeries());
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
            int i = 0;
            foreach (var client in genericBasedClients)
            {
                var activePower = (CompositeMeasurement)client.Resource("", "getACActivePower");
                if (activePowers.ContainsKey(client)) activePowers.Remove(client);
                activePowers.Add(client, activePower);
                if (activePower != null)
                {
                    (ClientData.ElementAt(i).Value.Series[0] as LineSeries).Points.Add(new DataPoint(time, activePower.value));
                }
                double setpoint = client.Hostname.Equals("10.42.241.5") ? (double)(externalViewClient.getControlOutput("DumploadControlAlgorithm", "setP") ?? default(double)) :
                    (double)(externalViewClient.getControlOutput("FlexibilityAlgorithm", "setP1") ?? default(double));
                (ClientData.ElementAt(i).Value.Series[1] as LineSeries).Points.Add(new DataPoint(time, setpoint));
                ClientData.ElementAt(i).Value.InvalidatePlot(true);

                i++;
            }
        }
    }
}
