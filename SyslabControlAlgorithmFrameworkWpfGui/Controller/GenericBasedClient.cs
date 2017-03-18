using System;
using System.Collections.Generic;
using System.Linq;
using SyslabControlAlgorithmFrameworkWpfGui.Communication;
using SyslabControlAlgorithmFrameworkWpfGui.Middleware;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System.Collections;
using System.Globalization;

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

        private readonly Dictionary<string, Type> parameterNamesAndTypes = new Dictionary<string, Type>();

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
            DisplayName = String.IsNullOrEmpty(name) ? "Client (" + hostname + ":" + port + ")" : name + " (" + hostname + ":" + port + ")";
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

            if (!parameterNamesAndTypes.ContainsKey(resourceName))
                parameterNamesAndTypes.Add(resourceName, value?.GetType());

            return value;
        }

        // TODO
        public void Control(string deviceName, string resourceName, params object[] values)
        {
            //if (parameterNamesAndTypes[parameterName] == null) return;

            //value = Convert.ChangeType(value, parameterNamesAndTypes[parameterName], CultureInfo.InvariantCulture);

            client.Push("control", deviceName, resourceName, values);
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
