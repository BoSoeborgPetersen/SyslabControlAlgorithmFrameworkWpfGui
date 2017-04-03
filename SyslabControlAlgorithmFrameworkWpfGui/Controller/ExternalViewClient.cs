using SyslabControlAlgorithmFrameworkWpfGui.Communication;
using SyslabControlAlgorithmFrameworkWpfGui.Middleware;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace SyslabControlAlgorithmFrameworkWpfGui.Controller
{
    public class ExternalViewClient
    {
        private static readonly Dictionary<string, ExternalViewClient> instances = new Dictionary<string, ExternalViewClient>();
        private readonly CommunicationClient client;
        public string Hostname { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public int Port { get; }

        private readonly Dictionary<string, Type> parameterNamesAndTypes = new Dictionary<string, Type>();

        public static ExternalViewClient Instance(string hostname, int port, string name)
        {
            if (!instances.ContainsKey(hostname + ":" + port))
                instances.Add(hostname + ":" + port, new ExternalViewClient(hostname, port, name, CommunicationFactory.GetCommunicationClient(MiddlewareType.YAMI4, SerializationType.JsonNewtonsoft, hostname, port)));

            return instances[hostname + ":" + port];
        }

        private ExternalViewClient(string hostname, int port, string name, CommunicationClient client)
        {
            Hostname = hostname;
            Name = name;
            var displayHostname = MyConfiguration.TranslateHostname(hostname, port);
            DisplayName = String.IsNullOrEmpty(name) ? "Client (Url: " + displayHostname + ":" + port + ")" : name + " (Url: " + displayHostname + ":" + port + ")";
            Port = port;
            this.client = client;
        }

        public Boolean IsIsolated =>
            (Boolean)client.Request("isIsolated");

        public List<string> GetControlAlgorithmNames() =>
            ((ArrayList)client.Request("getControlAlgorithmNames")).Cast<string>().ToList();
        public string GetControlAlgorithmState(string algorithmName) =>
            (string)client.Request("getControlAlgorithmState", algorithmName);

        public int GetControlAlgorithmRunIntervalMillis(string algorithmName) =>
            (int)(client.Request("getControlAlgorithmRunIntervalMillis", algorithmName) ?? -1);

        //public Dictionary<string, object> getControlParameters(string algorithmName)
        //{
        //    return null;
        //}

        public List<string> GetControlParameterNames(string algorithmName)
        {
            var result = client.Request("getControlParameterNames", algorithmName);

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public object GetControlParameter(string algorithmName, string parameterName)
        {
            if (parameterName == null) return null;

            object value = client.Request("getControlParameter", algorithmName, parameterName);

            if (value != null && !parameterNamesAndTypes.ContainsKey(parameterName))
                parameterNamesAndTypes.Add(parameterName, value.GetType());

            return value;
        }

        public void SetControlParameter(string algorithmName, string parameterName, object value)
        {
            if (parameterNamesAndTypes[parameterName] == null) return;

            value = Convert.ChangeType(value, parameterNamesAndTypes[parameterName], CultureInfo.InvariantCulture);

            client.Push("setControlParameter", algorithmName, parameterName, value);
        }

        //public Dictionary<string, object> getControlOutputs(string algorithmName)
        //{
        //    return null;
        //}

        public List<string> GetControlOutputNames(string algorithmName)
        {
            var result = client.Request("getControlOutputNames", algorithmName);

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public object GetControlOutput(string algorithmName, string outputName)
        {
            return client.Request("getControlOutput", algorithmName, outputName);
        }

        public void SwitchIsIsolated()
        {
            client.Push("switchIsIsolated");
        }

        public void StartAlgorithm(string algorithmName)
        {
            client.Push("startAlgorithm", algorithmName);
        }

        public void StopAlgorithm(string algorithmName)
        {
            client.Push("stopAlgorithm", algorithmName);
        }

        public void RestartAlgorithm(string algorithmName)
        {
            client.Push("restartAlgorithm", algorithmName);
        }

        public void PauseAlgorithm(string algorithmName)
        {
            client.Push("pauseAlgorithm", algorithmName);
        }

        public void ResumeAlgorithm(string algorithmName)
        {
            client.Push("resumeAlgorithm", algorithmName);
        }

        public List<string> GetCurrentAddresses()
        {
            var result = client.Request("getCurrentAddresses");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> GetTypes()
        {
            var result = client.Request("getTypes");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> GetRoles()
        {
            var result = client.Request("getRoles");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> GetServices()
        {
            var result = client.Request("getServices");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> GetPreviousMessages()
        {
            var result = client.Request("getPreviousMessages");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> GetPreviousRequests()
        {
            var result = client.Request("getPreviousRequests");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
