using System;
using System.Collections.Generic;
using System.Linq;
using SyslabControlAlgorithmFrameworkWpfGui.Communication;
using SyslabControlAlgorithmFrameworkWpfGui.Middleware;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Collections.Concurrent;

namespace SyslabControlAlgorithmFrameworkWpfGui.Controller
{
    public class GenericBasedClient
    {
        private static readonly Dictionary<string, GenericBasedClient> instances = new Dictionary<string, GenericBasedClient>();
        private readonly CommunicationClient client;
        public string Hostname { get; }
        public string Name { get; }
        public string DisplayName { get; }
        private readonly int port;

        private readonly ConcurrentDictionary<string, Type> parameterNamesAndTypes = new ConcurrentDictionary<string, Type>();

        public static GenericBasedClient Instance(string hostname, int port, string name)
        {
            if (!instances.ContainsKey(hostname + ":" + port))
                instances.Add(hostname + ":" + port, new GenericBasedClient(hostname, port, name, CommunicationFactory.GetCommunicationClient(MiddlewareType.YAMI4, SerializationType.JsonNewtonsoft, hostname, port)));

            return instances[hostname + ":" + port];
        }

        private GenericBasedClient(string hostname, int port, string name, CommunicationClient client)
        {
            Hostname = hostname;
            Name = name;
            var displayHostname = MyConfiguration.TranslateHostname(hostname, port);
            DisplayName = String.IsNullOrEmpty(name) ? "Client (" + displayHostname + ":" + port + ")" : name + " (" + displayHostname + ":" + port + ")";
            this.port = port;
            this.client = client;
        }

        public List<string> DeviceNames() =>
            ((ArrayList)client.Request("deviceNames")).Cast<string>().ToList();

        public List<string> ResourceNames(string deviceName)
        {
            var result = client.Request("resourceNames", deviceName);

            return result == null ? new List<string>() : ((ArrayList)result).Cast<string>().ToList();
        }

        // TODO
        public object Resource(string deviceName, string resourceName)
        {
            if (resourceName == null) return null;
            if (resourceName.Contains("[")) return "Requires Parameters";

            object value = client.Request("resource", deviceName, resourceName);
            
            parameterNamesAndTypes.TryAdd(resourceName, value?.GetType());

            return value;
        }

        // TODO
        public void Control(string deviceName, string resourceName, params object[] values)
        {
            //if (parameterNamesAndTypes[parameterName] == null) return;

            //value = Convert.ChangeType(value, parameterNamesAndTypes[parameterName], CultureInfo.InvariantCulture);

            client.Push(PackVarargs("control", deviceName, resourceName, values));
        }

        private object[] PackVarargs(string requestName, string deviceName, string resourceName, object[] args)
        {
            var parameters = new object[args.Length + 3];
            parameters[0] = requestName;
            parameters[1] = deviceName;
            parameters[2] = resourceName;
            for (int i = 0; i < args.Length; i++)
                parameters[i + 3] = args[i];

            return parameters;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
