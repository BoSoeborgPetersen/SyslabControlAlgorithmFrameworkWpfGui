using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Middleware
{
    public class MiddlewareFactory
    {
        public static MiddlewareClient GetMiddlewareClient(MiddlewareType type, string host, int port)
        {
            if (type == MiddlewareType.YAMI4) return new Yami4MiddlewareClient(host, port);

            throw new ArgumentException("Wrong raw type");
        }
    }
}
