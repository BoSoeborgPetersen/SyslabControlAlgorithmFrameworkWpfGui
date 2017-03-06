using GalaSoft.MvvmLight;
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

        private ExternalViewClient client1ex = MyConfiguration.ExternalViewClient(1);

        private GenericBasedClient client1gb = MyConfiguration.GenericBasedClient(1);
        private GenericBasedClient client2gb = MyConfiguration.GenericBasedClient(2);
        private GenericBasedClient client3gb = MyConfiguration.GenericBasedClient(3);
        private GenericBasedClient client4gb = MyConfiguration.GenericBasedClient(4);

        private CompositeMeasurement activePower1;
        private CompositeMeasurement activePower2;
        private CompositeMeasurement activePower3;
        private CompositeMeasurement activePower4;
        
        public PlotModel Chart1 { get; private set; } = new PlotModel();
        private LineSeries Chart1Series1 = new LineSeries();
        private LineSeries Chart1Series2 = new LineSeries();
        public PlotModel Chart2 { get; private set; } = new PlotModel();
        private LineSeries Chart2Series1 = new LineSeries();
        private LineSeries Chart2Series2 = new LineSeries();
        public PlotModel Chart3 { get; private set; } = new PlotModel();
        private LineSeries Chart3Series1 = new LineSeries();
        private LineSeries Chart3Series2 = new LineSeries();
        public PlotModel Chart4 { get; private set; } = new PlotModel();
        private LineSeries Chart4Series1 = new LineSeries();
        private LineSeries Chart4Series2 = new LineSeries();

        public double TotalActivePower => (activePower1?.value ?? 0) + (activePower2?.value ?? 0) + (activePower3?.value ?? 0) + (activePower4?.value ?? 0);

        public PlottingViewModel()
        {
            Chart1.Series.Add(Chart1Series1);
            Chart1.Series.Add(Chart1Series2);
            Chart2.Series.Add(Chart2Series1);
            Chart2.Series.Add(Chart2Series2);
            Chart3.Series.Add(Chart3Series1);
            Chart3.Series.Add(Chart3Series2);
            Chart4.Series.Add(Chart4Series1);
            Chart4.Series.Add(Chart4Series2);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    time++;

                    GetDataPoint1();
                    GetDataPoint2();
                    GetDataPoint3();
                    GetDataPoint4();

                    RaisePropertyChanged(() => Chart1);
                    RaisePropertyChanged(() => Chart2);
                    RaisePropertyChanged(() => Chart3);
                    RaisePropertyChanged(() => Chart4);
                    RaisePropertyChanged(() => TotalActivePower);

                    Thread.Sleep(1000);
                }
            }).Start();
        }

        public void GetDataPoint1()
        {
            activePower1 = (CompositeMeasurement)client1gb.Resource("genset1", "getACActivePower");
            if (activePower1 != null)
            {
                Chart1Series1.Points.Add(new DataPoint(time, activePower1.value));
            }
            double setpoint = (double)(client1ex.getControlParameter("FlexibilityAlgorithm", "setP1") ?? default(double));
            Chart1Series2.Points.Add(new DataPoint(time, setpoint));
            Chart1.InvalidatePlot(true);
        }

        public void GetDataPoint2()
        {
            activePower2 = (CompositeMeasurement)client2gb.Resource("pv117", "getACActivePower");
            if (activePower2 != null)
            {
                Chart2Series1.Points.Add(new DataPoint(time, activePower2.value));
            }
            double setpoint = (double)(client1ex.getControlParameter("FlexibilityAlgorithm", "setP2") ?? default(double));
            Chart2Series2.Points.Add(new DataPoint(time, setpoint));
            Chart2.InvalidatePlot(true);
        }

        public void GetDataPoint3()
        {
            activePower3 = (CompositeMeasurement)client3gb.Resource("load1", "getACActivePower");
            if (activePower3 != null)
            {
                Chart3Series1.Points.Add(new DataPoint(time, activePower3.value));
            }
            double setpoint = (double)(client1ex.getControlParameter("FlexibilityAlgorithm", "setP3") ?? default(double));
            Chart3Series2.Points.Add(new DataPoint(time, setpoint));
            Chart3.InvalidatePlot(true);
        }

        public void GetDataPoint4()
        {
            activePower4 = (CompositeMeasurement)client4gb.Resource("batt1", "getACActivePower");
            if (activePower4 != null)
            {
                Chart4Series1.Points.Add(new DataPoint(time, activePower4.value));
            }
            double setpoint = (double)(client1ex.getControlParameter("FlexibilityAlgorithm", "setP4") ?? default(double));
            Chart4Series2.Points.Add(new DataPoint(time, setpoint));
            Chart4.InvalidatePlot(true);
        }
    }
}
