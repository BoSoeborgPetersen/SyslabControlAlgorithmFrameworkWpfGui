using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Middleware
{
    public interface MiddlewareClient
    {
        string Request(string message);
        byte[] Request(byte[] message);

        void Push(string message);
        void Push(string message, int priority);
        void Push(byte[] message);
        void Push(byte[] message, int priority);

        void SubscribeString(Action<string> subscribeCallback);
        void SubscribeBinary(Action<byte[]> subscribeCallback);

        void Shutdown();
    }
}