using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Controller
{
    public static class MyConfiguration
    {
        // Config.
        private static readonly TestSetupType setupType = TestSetupType.LocalOnly;

        private static readonly string hostname = Dns.GetHostName();
        private static readonly Tuple<string, int, string> genericBasedConnectionInfoLocalOnly = new Tuple<string, int, string>(hostname, 9000, "Localhost");
        private static readonly Tuple<string, int, string> externalViewConnectionInfoLocalOnly = new Tuple<string, int, string>(hostname, 5531, "Localhost");
        private static readonly List<Tuple<string, int, string>> genericBasedConnectionInfoDuevejTestSetup = new List<Tuple<string, int, string>>()
        {
            new Tuple<string, int, string>("185.118.251.66", 9031, "Dell 1"),
            new Tuple<string, int, string>("185.118.251.66", 9032, "Dell 2"),
            new Tuple<string, int, string>("185.118.251.66", 9021, "RPi 1"),
            new Tuple<string, int, string>("185.118.251.66", 9022, "RPi 2")
        };
        private static readonly List<Tuple<string, int, string>> externalViewConnectionInfoDuevejTestSetup = new List<Tuple<string, int, string>>()
        {
            new Tuple<string, int, string>("185.118.251.66", 5531, "Dell 1"),
            new Tuple<string, int, string>("185.118.251.66", 5532, "Dell 2"),
            new Tuple<string, int, string>("185.118.251.66", 5521, "RPi 1"),
            new Tuple<string, int, string>("185.118.251.66", 5522, "RPi 2")
        };
        private static readonly List<Tuple<string, int, string>> genericBasedConnectionInfoRisø = new List<Tuple<string, int, string>>()
        {
            new Tuple<string, int, string>("10.42.241.2", 9000, "Diesel"),
            new Tuple<string, int, string>("10.42.241.3", 9000, "Gaia"),
            new Tuple<string, int, string>("10.42.241.5", 9000, "Dumpload"),
            new Tuple<string, int, string>("10.42.241.7", 9000, "PV 117"),
            new Tuple<string, int, string>("10.42.241.10", 9000, "PV 715"),
            new Tuple<string, int, string>("10.42.241.12", 9000, "Vanadium Battery"),
            new Tuple<string, int, string>("10.42.241.16", 9000, "Mobile Load 1"),
            new Tuple<string, int, string>("10.42.241.24", 9000, "PV 319")
        };
        private static readonly List<Tuple<string, int, string>> externalViewConnectionInfoRisø= new List<Tuple<string, int, string>>()
        {
            new Tuple<string, int, string>("10.42.241.2", 5531, "Diesel"),
            new Tuple<string, int, string>("10.42.241.3", 5531, "Gaia"),
            new Tuple<string, int, string>("10.42.241.5", 5531, "Dumpload"),
            new Tuple<string, int, string>("10.42.241.7", 5531, "PV 117"),
            new Tuple<string, int, string>("10.42.241.10", 5531, "PV 715"),
            new Tuple<string, int, string>("10.42.241.12", 5531, "Vanadium Battery"),
            new Tuple<string, int, string>("10.42.241.16", 5531, "Mobile Load 1"),
            new Tuple<string, int, string>("10.42.241.24", 5531, "PV 319")
        };

        public static IEnumerable<GenericBasedClient> GenericBasedClients()
        {
            if (setupType == TestSetupType.Risø)
            {
                foreach (var entry in genericBasedConnectionInfoRisø)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2, genericBasedConnectionInfoLocalOnly.Item3);
                foreach (var entry in genericBasedConnectionInfoRisø)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.Duevej)
            {
                foreach (var entry in genericBasedConnectionInfoDuevejTestSetup)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2, genericBasedConnectionInfoLocalOnly.Item3);
                foreach (var entry in genericBasedConnectionInfoDuevejTestSetup)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.LocalOnly)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2, genericBasedConnectionInfoLocalOnly.Item3);
            }
        }

        public static IEnumerable<ExternalViewClient> ExternalViewClients()
        {
            if (setupType == TestSetupType.Risø)
            {
                foreach (var entry in externalViewConnectionInfoRisø)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                yield return Controller.ExternalViewClient.Instance(externalViewConnectionInfoLocalOnly.Item1, externalViewConnectionInfoLocalOnly.Item2, externalViewConnectionInfoLocalOnly.Item3);
                foreach (var entry in externalViewConnectionInfoRisø)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.Duevej)
            {
                foreach (var entry in externalViewConnectionInfoDuevejTestSetup)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                yield return Controller.ExternalViewClient.Instance(externalViewConnectionInfoLocalOnly.Item1, externalViewConnectionInfoLocalOnly.Item2, externalViewConnectionInfoLocalOnly.Item3);
                foreach (var entry in externalViewConnectionInfoDuevejTestSetup)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2, entry.Item3);
            }
            else if (setupType == TestSetupType.LocalOnly)
            {
                yield return Controller.ExternalViewClient.Instance(externalViewConnectionInfoLocalOnly.Item1, externalViewConnectionInfoLocalOnly.Item2, externalViewConnectionInfoLocalOnly.Item3);
            }
        }
    }

    internal enum TestSetupType
    {
        Risø = 0,
        RisøAndLocal = 1,
        Duevej = 2,
        DuevejAndLocal = 3,
        LocalOnly = 4
    }
}
