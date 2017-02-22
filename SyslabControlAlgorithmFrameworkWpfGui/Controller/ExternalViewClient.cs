using SyslabControlAlgorithmFrameworkWpfGui.Communication;
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
        private static ExternalViewClient instance = new ExternalViewClient();
        private CommunicationClient client = CommunicationFactory.GetCommunicationClient(MiddlewareType.YAMI4, SerializationType.JsonNewtonsoft, "localhost", 5531);

        private Dictionary<string, Type> parameterNamesAndTypes = new Dictionary<string, Type>();

        public static ExternalViewClient Instance => instance;

        private ExternalViewClient() { }

        public List<string> getControlAlgorithmNames() => 
            ((ArrayList)client.Request("getControlAlgorithmNames")).Cast<string>().ToList();

        public int getControlAlgorithmRunIntervalMillis(string algorithmName) => 
            (int) client.Request("getControlAlgorithmRunIntervalMillis", algorithmName);

        //public Dictionary<string, object> getControlParameters(string algorithmName)
        //{
        //    return null;
        //}

        public List<string> getControlParameterNames(string algorithmName) => 
            ((ArrayList)client.Request("getControlParameterNames", algorithmName)).Cast<string>().ToList();

        public object getControlParameter(string algorithmName, string parameterName)
        {
            object value = client.Request("getControlParameter", algorithmName, parameterName);

            if (!parameterNamesAndTypes.ContainsKey(parameterName))
                parameterNamesAndTypes.Add(parameterName, value.GetType());

            return value;
        }

        public void setControlParameter(string algorithmName, string parameterName, object value)
        {
            value = Convert.ChangeType(value, parameterNamesAndTypes[parameterName], CultureInfo.InvariantCulture);

            client.Push("setControlParameter", algorithmName, parameterName, value);
        }

        //public Dictionary<string, object> getControlOutputs(string algorithmName)
        //{
        //    return null;
        //}

        public List<string> getControlOutputNames(string algorithmName) => 
            ((ArrayList)client.Request("getControlOutputNames", algorithmName)).Cast<string>().ToList();

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

        // TODO: Debug.
        public List<string> getCurrentAddresses()
        {
            return ((ArrayList)client.Request("getCurrentAddresses")).Cast<string>().ToList();
        }
    }
}
