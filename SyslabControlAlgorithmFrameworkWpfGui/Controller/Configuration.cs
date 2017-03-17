using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Controller
{
    public class MyConfiguration
    {
        // Config.
        private static TestSetupType setupType = TestSetupType.Risø;

        private static string hostname = Dns.GetHostName();
        private static Tuple<string, int> genericBasedConnectionInfoLocalOnly = new Tuple<string, int>(hostname, 9000);
        private static Tuple<string, int> externalViewConnectionInfoLocalOnly = new Tuple<string, int>(hostname, 5531);
        private static List<Tuple<string, int>> genericBasedConnectionInfoDuevejTestSetup = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("185.118.251.66", 9031),
            new Tuple<string, int>("185.118.251.66", 9032),
            new Tuple<string, int>("185.118.251.66", 9021),
            new Tuple<string, int>("185.118.251.66", 9022)
        };
        private static List<Tuple<string, int>> externalViewConnectionInfoDuevejTestSetup = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("185.118.251.66", 5531),
            new Tuple<string, int>("185.118.251.66", 5532),
            new Tuple<string, int>("185.118.251.66", 5521),
            new Tuple<string, int>("185.118.251.66", 5522)
        };
        private static List<Tuple<string, int>> genericBasedConnectionInfoRisø = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("10.42.241.2", 9000),
            new Tuple<string, int>("10.42.241.3", 9000),
            new Tuple<string, int>("10.42.241.5", 9000),
            new Tuple<string, int>("10.42.241.7", 9000),
            new Tuple<string, int>("10.42.241.10", 9000),
            new Tuple<string, int>("10.42.241.12", 9000),
            new Tuple<string, int>("10.42.241.16", 9000),
            new Tuple<string, int>("10.42.241.24", 9000)
        };
        private static List<Tuple<string, int>> externalViewConnectionInfoRisø= new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("10.42.241.2", 5531),
            new Tuple<string, int>("10.42.241.3", 5531), 
            new Tuple<string, int>("10.42.241.5", 5531),
            new Tuple<string, int>("10.42.241.7", 5531),
            new Tuple<string, int>("10.42.241.10", 5531),
            new Tuple<string, int>("10.42.241.12", 5531),
            new Tuple<string, int>("10.42.241.16", 5531),
            new Tuple<string, int>("10.42.241.24", 5531)
        };

        public static IEnumerable<GenericBasedClient> GenericBasedClients()
        {
            if (setupType == TestSetupType.Risø)
                foreach (var entry in genericBasedConnectionInfoRisø)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2);
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2);
                foreach (var entry in genericBasedConnectionInfoRisø)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2);
            }
            else if (setupType == TestSetupType.Duevej)
                foreach (var entry in genericBasedConnectionInfoDuevejTestSetup)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2);
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2);
                foreach (var entry in genericBasedConnectionInfoDuevejTestSetup)
                    yield return Controller.GenericBasedClient.Instance(entry.Item1, entry.Item2);
            }
            else if (setupType == TestSetupType.LocalOnly)
                yield return Controller.GenericBasedClient.Instance(genericBasedConnectionInfoLocalOnly.Item1, genericBasedConnectionInfoLocalOnly.Item2);
        }

        public static IEnumerable<ExternalViewClient> ExternalViewClients()
        {
            if (setupType == TestSetupType.Risø)
                foreach (var entry in externalViewConnectionInfoRisø)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2);
            else if (setupType == TestSetupType.RisøAndLocal)
            {
                yield return Controller.ExternalViewClient.Instance(externalViewConnectionInfoLocalOnly.Item1, externalViewConnectionInfoLocalOnly.Item2);
                foreach (var entry in externalViewConnectionInfoRisø)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2);
            }
            else if (setupType == TestSetupType.Duevej)
                foreach (var entry in externalViewConnectionInfoDuevejTestSetup)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2);
            else if (setupType == TestSetupType.DuevejAndLocal)
            {
                yield return Controller.ExternalViewClient.Instance(externalViewConnectionInfoLocalOnly.Item1, externalViewConnectionInfoLocalOnly.Item2);
                foreach (var entry in externalViewConnectionInfoDuevejTestSetup)
                    yield return Controller.ExternalViewClient.Instance(entry.Item1, entry.Item2);
            }
            else if (setupType == TestSetupType.LocalOnly)
                yield return Controller.ExternalViewClient.Instance(externalViewConnectionInfoLocalOnly.Item1, externalViewConnectionInfoLocalOnly.Item2);
        }
    }

    enum TestSetupType
    {
        Risø, 
        RisøAndLocal, 
        Duevej,
        DuevejAndLocal, 
        LocalOnly
    }
}
