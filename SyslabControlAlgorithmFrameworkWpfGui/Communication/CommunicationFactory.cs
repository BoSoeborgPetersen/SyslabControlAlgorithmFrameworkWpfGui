using SyslabControlAlgorithmFrameworkWpfGui.Middleware;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class CommunicationFactory
    {
        public static CommunicationClient GetCommunicationClient(MiddlewareType middlewareType, SerializationType serializationType, string host, int port) => 
            new CommunicationClient(middlewareType, serializationType, host, port);
    }
}
