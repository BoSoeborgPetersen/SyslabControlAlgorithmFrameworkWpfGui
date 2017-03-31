﻿using Inspirel.YAMI;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SyslabControlAlgorithmFrameworkWpfGui.Middleware
{
    public class Yami4MiddlewareClient : IMiddlewareClient
    {
        private const int defaultPriority = 0;
        private const string stringReqRepName = "stringreqrep";
        private const string binaryReqRepName = "binaryreqrep";
        private const string stringPushPullName = "stringpushpull";
        private const string binaryPushPullName = "binarypushpull";
        private const string stringPubSubName = "stringpubsub";
        private const string binaryPubSubName = "binarypubsub";

        private readonly Agent agent;
        private readonly string serverURI;

        public Yami4MiddlewareClient(string host, int port)
        {
            serverURI = "tcp://" + host + ":" + port;

            Console.WriteLine("YAMI4 Client '" + serverURI + "' Connecting To Server");

            Parameters parameters = new Parameters();
            parameters.SetBoolean(OptionNames.DELIVER_AS_RAW_BINARY, true);
            agent = new Agent(parameters);
        }

        public string Request(string message)
        {
            return Request(message, defaultPriority);
        }

        public string Request(string message, int priority)
        {
            OutgoingMessage outMessage = agent.Send(serverURI, stringReqRepName, "", PackMessage(message), priority);

            outMessage.WaitForCompletion(1000);

            if (outMessage.State == OutgoingMessage.MessageState.REPLIED)
            {
                byte[] data = outMessage.RawReply;
                outMessage.Close();
                return Unpackstring(data);
            }

            return null;
        }

        public byte[] Request(byte[] message) => Request(message, defaultPriority);

        public byte[] Request(byte[] message, int priority)
        {
            OutgoingMessage outMessage = agent.Send(serverURI, binaryReqRepName, "", PackMessage(message), priority);

            outMessage.WaitForCompletion(1000);

            if (outMessage.State == OutgoingMessage.MessageState.REPLIED)
            {
                byte[] data = outMessage.RawReply;
                outMessage.Close();
                return UnpackBinary(data);
            }

            return null;
        }

        public void Push(string message)
        {
            Push(message, defaultPriority);
        }

        public void Push(string message, int priority)
        {
            agent.SendOneWay(serverURI, stringPushPullName, "", PackMessage(message), priority);
        }

        public void Push(byte[] message)
        {
            Push(message, defaultPriority);
        }

        public void Push(byte[] message, int priority)
        {
            agent.SendOneWay(serverURI, binaryPushPullName, "", PackMessage(message), priority);
        }

        public void SubscribeString(Action<string> subscribeCallback)
        {
            agent.RegisterObject(stringPubSubName, (object sender, IncomingMessageArgs message) =>
                subscribeCallback(UnpackstringMessage(message.Message)));

            agent.SendOneWay(serverURI, stringPubSubName, "", null);
        }

        public /*async*/ void SubscribeBinary(Action<byte[]> subscribeCallback)
        {
            //await Task.Factory.StartNew(() =>
            //{
                agent.RegisterObject(binaryPubSubName, (object sender, IncomingMessageArgs message) =>
                    subscribeCallback(UnpackBinaryMessage(message.Message)));

                agent.SendOneWay(serverURI, binaryPubSubName, "", null);
            //});
        }

        public void Shutdown()
        {
            agent.Close();
        }

        private static RawBinaryDataSource PackMessage(string message)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(message);
            var padArray = new byte[] { 0x80, 0x80, 0xf0, 0xf0 };
            int addedLength = ((4 - (encoded.Length % 4)) % 4) + 4;

            Array.Resize(ref encoded, message.Length + addedLength);
            for (int i = 1; i <= addedLength; i++)
                encoded[encoded.Length - addedLength + (i - 1)] = i > 4 ? (byte)0xff : padArray[i - 1];
            return new RawBinaryDataSource(encoded);
        }

        private static RawBinaryDataSource PackMessage(byte[] message)
        {
            var padArray = new byte[] { 0x80, 0x80, 0xf0, 0xf0 };
            int addedLength = ((4 - (message.Length % 4)) % 4) + 4;

            Array.Resize(ref message, message.Length + addedLength);
            for (int i = 1; i <= addedLength; i++)
                message[message.Length - addedLength + (i - 1)] = i > 4 ? (byte)0xff : padArray[i - 1];

            return new RawBinaryDataSource(message);
        }

        private static string UnpackstringMessage(IncomingMessage message) => Encoding.UTF8.GetString(Trim(message.RawContent)).Trim();

        private static byte[] UnpackBinaryMessage(IncomingMessage message) => Trim(message.RawContent);

        private static string Unpackstring(byte[] message) => Encoding.UTF8.GetString(Trim(message));

        private static byte[] UnpackBinary(byte[] message) => Trim(message);

        private static byte[] Trim(byte[] bytes)
        {
            int i = bytes.Length - 1;
            while (i >= 0 && bytes[i] == 0xff)
            {
                --i;
            }
            i -= 4;

            Array.Resize(ref bytes, i + 1);
            return bytes;
        }
    }
}
