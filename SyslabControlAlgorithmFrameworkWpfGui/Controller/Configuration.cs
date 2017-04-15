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
        private static readonly TestSetupType setupType = TestSetupType.Risø;

        private static readonly string hostname = Dns.GetHostName();
        private static readonly Tuple<string, int, string, bool> genericBasedConnectionInfoLocalOnly = new Tuple<string, int, string, bool>(hostname, 9000, "Localhost", false);
        private static readonly Tuple<string, int, string, bool> externalViewConnectionInfoLocalOnly = new Tuple<string, int, string, bool>(hostname, 5531, "Localhost", false);
        private static readonly List<Tuple<string, int, string, bool>> genericBasedConnectionInfoDuevejTestSetup = new List<Tuple<string, int, string, bool>>()
        {
            new Tuple<string, int, string, bool>("217.61.216.173", 9031, "Dell 1", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 9032, "Dell 2", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 9021, "RPi 1", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 9022, "RPi 2", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 9041, "BBB 1", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 9042, "BBB 2", false)
        };

        private static readonly List<Tuple<string, int, string, bool>> externalViewConnectionInfoDuevejTestSetup = new List<Tuple<string, int, string, bool>>()
        {
            new Tuple<string, int, string, bool>("217.61.216.173", 5531, "Dell 1", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 5532, "Dell 2", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 5521, "RPi 1", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 5522, "RPi 2", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 5541, "BBB 1", false),
            new Tuple<string, int, string, bool>("217.61.216.173", 5542, "BBB 2", false)
        };
        private static readonly List<Tuple<string, int, string, bool>> internalConnectionInfoDuevejTestSetup = new List<Tuple<string, int, string, bool>>()
        {
            new Tuple<string, int, string, bool>("192.168.0.31", 5531, "Dell 1", false),
            new Tuple<string, int, string, bool>("192.168.0.32", 5531, "Dell 2", false),
            new Tuple<string, int, string, bool>("192.168.0.21", 5531, "RPi 1", false),
            new Tuple<string, int, string, bool>("192.168.0.22", 5531, "RPi 2", false),
            new Tuple<string, int, string, bool>("192.168.0.41", 5531, "BBB 1", false),
            new Tuple<string, int, string, bool>("192.168.0.42", 5531, "BBB 2", false)
        };
        private static readonly List<Tuple<string, int, string, bool>> genericBasedConnectionInfoRisø = new List<Tuple<string, int, string, bool>>()
        {
            new Tuple<string, int, string, bool>("10.42.241.2", 9000, "Diesel", false),
            new Tuple<string, int, string, bool>("10.42.241.3", 9000, "Gaia", false),
            new Tuple<string, int, string, bool>("10.42.241.5", 9000, "Dumpload", true),
            new Tuple<string, int, string, bool>("10.42.241.7", 9000, "PV 117", false),
            new Tuple<string, int, string, bool>("10.42.241.10", 9000, "PV 715", false),
            new Tuple<string, int, string, bool>("10.42.241.12", 9000, "Vanadium Battery", false),
            new Tuple<string, int, string, bool>("10.42.241.16", 9000, "Mobile Load 1", true),
            new Tuple<string, int, string, bool>("10.42.241.24", 9000, "PV 319", false)
        };
        private static readonly List<Tuple<string, int, string, bool>> externalViewConnectionInfoRisø= new List<Tuple<string, int, string, bool>>()
        {
            new Tuple<string, int, string, bool>("10.42.241.2", 5531, "Diesel", false),
            new Tuple<string, int, string, bool>("10.42.241.3", 5531, "Gaia", false),
            new Tuple<string, int, string, bool>("10.42.241.5", 5531, "Dumpload", true),
            new Tuple<string, int, string, bool>("10.42.241.7", 5531, "PV 117", false),
            new Tuple<string, int, string, bool>("10.42.241.10", 5531, "PV 715", false),
            new Tuple<string, int, string, bool>("10.42.241.12", 5531, "Vanadium Battery", false),
            new Tuple<string, int, string, bool>("10.42.241.16", 5531, "Mobile Load 1", true),
            new Tuple<string, int, string, bool>("10.42.241.24", 5531, "PV 319", false)
        };

        public static IEnumerable<GenericBasedClient> GenericBasedClients()
        {
            if (setupType == TestSetupType.Risø)
            {
                foreach (var entry in genericBasedConnectionInfoRisø)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3, entry.Item4);
            }
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2, genericBasedConnectionInfoLocalOnly.Item3, genericBasedConnectionInfoLocalOnly.Item4);
                foreach (var entry in genericBasedConnectionInfoRisø)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3, entry.Item4);
            }
            else if (setupType == TestSetupType.Duevej)
            {
                foreach (var entry in genericBasedConnectionInfoDuevejTestSetup)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3, entry.Item4);
            }
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2, genericBasedConnectionInfoLocalOnly.Item3, genericBasedConnectionInfoLocalOnly.Item4);
                foreach (var entry in genericBasedConnectionInfoDuevejTestSetup)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2, entry.Item3, entry.Item4);
            }
            else if (setupType == TestSetupType.LocalOnly)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2, genericBasedConnectionInfoLocalOnly.Item3, genericBasedConnectionInfoLocalOnly.Item4);
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

        public static string DeviceNameFromHostname(string hostname)
        {
            if (hostname == "Error") return hostname;

            if (setupType == TestSetupType.Risø)
            {
                return externalViewConnectionInfoRisø.SingleOrDefault(x => x.Item1 == hostname)?.Item3 ?? hostname;
            }
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                return (externalViewConnectionInfoRisø.SingleOrDefault(x => x.Item1 == hostname) ?? externalViewConnectionInfoLocalOnly).Item3;
            }
            else if (setupType == TestSetupType.Duevej)
            {
                return internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname)?.Item3 ?? hostname;
            }
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                return (internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname) ?? externalViewConnectionInfoLocalOnly).Item3;
            }
            else if (setupType == TestSetupType.LocalOnly)
            {
                return externalViewConnectionInfoLocalOnly.Item3;
            }
            return "Unknown";
        }

        public static int PortFromHostname(string hostname)
        {
            if (hostname == "Error") return -1;

            if (setupType == TestSetupType.Risø)
            {
                return externalViewConnectionInfoRisø.SingleOrDefault(x => x.Item1 == hostname)?.Item2 ?? -1;
            }
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                return (externalViewConnectionInfoRisø.SingleOrDefault(x => x.Item1 == hostname) ?? externalViewConnectionInfoLocalOnly).Item2;
            }
            else if (setupType == TestSetupType.Duevej)
            {
                return internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname)?.Item2 ?? -1;
            }
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                return (internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname) ?? externalViewConnectionInfoLocalOnly).Item2;
            }
            else if (setupType == TestSetupType.LocalOnly)
            {
                return externalViewConnectionInfoLocalOnly.Item2;
            }
            return -1;
        }

        public static string TranslateHostname(string hostname, int port)
        {
            if (hostname == "Error") return hostname;

            if (setupType == TestSetupType.Risø)
            {
                return hostname;
            }
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                return hostname;
            }
            else if (setupType == TestSetupType.Duevej)
            {
                if (externalViewConnectionInfoDuevejTestSetup.Any(x => x.Item2 == port))
                {
                    var name = externalViewConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname && x.Item2 == port)?.Item3 ?? hostname;
                    return internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item3 == name)?.Item1 ?? hostname;
                }
                else
                {
                    var name = genericBasedConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname && x.Item2 == port)?.Item3 ?? hostname;
                    return internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item3 == name)?.Item1 ?? hostname;
                }
            }
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                if (hostname == MyConfiguration.hostname) return hostname;

                if (externalViewConnectionInfoDuevejTestSetup.Any(x => x.Item2 == port))
                {
                    var name = externalViewConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname && x.Item2 == port)?.Item3 ?? hostname;
                    return internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item3 == name)?.Item1 ?? hostname;
                }
                else
                {
                    var name = genericBasedConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item1 == hostname && x.Item2 == port)?.Item3 ?? hostname;
                    return internalConnectionInfoDuevejTestSetup.SingleOrDefault(x => x.Item3 == name)?.Item1 ?? hostname;
                }
            }
            else if (setupType == TestSetupType.LocalOnly)
            {
                return hostname;
            }
            return hostname;
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
