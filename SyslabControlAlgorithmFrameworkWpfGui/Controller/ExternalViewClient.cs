﻿using SyslabControlAlgorithmFrameworkWpfGui.Communication;
using SyslabControlAlgorithmFrameworkWpfGui.Middleware;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SyslabControlAlgorithmFrameworkWpfGui.Controller
{
    public class ExternalViewClient
    {
        private static Dictionary<string, ExternalViewClient> instances = new Dictionary<string, ExternalViewClient>();
        private CommunicationClient client;
        public string Hostname { get; private set; }
        private int port;

        private Dictionary<string, Type> parameterNamesAndTypes = new Dictionary<string, Type>();

        public static ExternalViewClient Instance(string hostname, int port)
        {
            if (!instances.ContainsKey(hostname + ":" + port))
                instances.Add(hostname + ":" + port, new ExternalViewClient(hostname, port, CommunicationFactory.GetCommunicationClient(MiddlewareType.YAMI4, SerializationType.JsonNewtonsoft, hostname, port)));

            return instances[hostname + ":" + port];
        }

        private ExternalViewClient(string hostname, int port, CommunicationClient client)
        {
            this.Hostname = hostname;
            this.port = port;
            this.client = client;
        }

        public List<string> getControlAlgorithmNames() =>
            ((ArrayList)client.Request("getControlAlgorithmNames")).Cast<string>().ToList();
        public string getControlAlgorithmState(string algorithmName) =>
            (string)client.Request("getControlAlgorithmState", algorithmName);

        public int getControlAlgorithmRunIntervalMillis(string algorithmName) =>
            (int)client.Request("getControlAlgorithmRunIntervalMillis", algorithmName);

        //public Dictionary<string, object> getControlParameters(string algorithmName)
        //{
        //    return null;
        //}

        public List<string> getControlParameterNames(string algorithmName)
        {
            var result = client.Request("getControlParameterNames", algorithmName);

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public object getControlParameter(string algorithmName, string parameterName)
        {
            if (parameterName == null) return null;

            object value = client.Request("getControlParameter", algorithmName, parameterName);

            if (value != null && !parameterNamesAndTypes.ContainsKey(parameterName))
                parameterNamesAndTypes.Add(parameterName, value.GetType());

            return value;
        }

        public void setControlParameter(string algorithmName, string parameterName, object value)
        {
            if (parameterNamesAndTypes[parameterName] == null) return;

            value = Convert.ChangeType(value, parameterNamesAndTypes[parameterName], CultureInfo.InvariantCulture);

            client.Push("setControlParameter", algorithmName, parameterName, value);
        }

        //public Dictionary<string, object> getControlOutputs(string algorithmName)
        //{
        //    return null;
        //}

        public List<string> getControlOutputNames(string algorithmName)
        {
            var result = client.Request("getControlOutputNames", algorithmName);

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public object getControlOutput(string algorithmName, string outputName) => 
            client.Request("getControlOutput", algorithmName, outputName);

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

        public List<string> getCurrentAddresses()
        {
            var result = client.Request("getCurrentAddresses");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> getTypes()
        {
            var result = client.Request("getTypes");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> getRoles()
        {
            var result = client.Request("getRoles");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> getServices()
        {
            var result = client.Request("getServices");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> getPreviousMessages()
        {
            var result = client.Request("getPreviousMessages");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public List<string> getPreviousRequests()
        {
            var result = client.Request("getPreviousRequests");

            return result == null ? new List<string>() { "Error" } : ((ArrayList)result).Cast<string>().ToList();
        }

        public override string ToString()
        {
            return "Client [hostname: " + Hostname + ", port: " + port + "]";
        }
    }
}
