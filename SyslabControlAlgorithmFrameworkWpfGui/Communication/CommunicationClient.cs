using SyslabControlAlgorithmFrameworkWpfGui.Middleware;
using SyslabControlAlgorithmFrameworkWpfGui.Serialization;
using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Communication
{
    public class CommunicationClient : CommunicationBase
    {
        private int serializationNumber;
        private Serializer serializer;
        private MiddlewareType middlewareType;
        private MiddlewareClient middlewareClient;
        private bool bypassReceivingSerialization = false;
        private bool bypassSendingSerialization = false;

        public CommunicationClient(MiddlewareType middlewareType, SerializationType serializationType,
            string host, int port)
        {
            this.middlewareType = middlewareType;

            serializationNumber = GetSerializationNumber(serializationType);
            serializer = SerializationFactory.GetSerializer(serializationType);

            middlewareClient = MiddlewareFactory.GetMiddlewareClient(middlewareType, host, port);
        }

        public void RemoveSerializer()
        {
            serializer = null;
        }

        public void ChangeSerialization(SerializationType serializationType)
        {
            serializationNumber = GetSerializationNumber(serializationType);
            serializer = SerializationFactory.GetSerializer(serializationType);
        }

        public void ChangeReceivingBypassSerialization(bool bypass)
        {
            bypassReceivingSerialization = bypass;
        }

        public void ChangeSendingBypassSerialization(bool bypass)
        {
            bypassSendingSerialization = bypass;
        }

        public string GetMiddlewareName() => MiddlewareTypeHelper.GetName();

        public bool SupportsPublishSubscribe() => MiddlewareTypeHelper.SupportsPubSub();

        public string GetSerializerName() => serializer.SerializerName;

        public bool IsStringNotBinary() => serializer.StringNotBinary;

        public bool SupportsCompression() => serializer.SupportsCompression;

        public string MessageRequest(string message)
        {
            try
            {
                string returnMessage = middlewareClient.Request("MessageRequest:" + message);
                return returnMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine("CommunicationClient: Message Request Error");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public object Request(params object[] parameters)
        {
            if (bypassSendingSerialization)
            {
                if (IsStringNotBinary())
                    return middlewareClient.Request(parameters[0] as string);
                else
                    return middlewareClient.Request(parameters[0] as byte[]);
            }
            else
            {
                if (IsStringNotBinary())
                {
                    try
                    {
                        string[] serializedParams = new string[parameters.Length];
                        for (int i = 0; i < parameters.Length; i++)
                            serializedParams[i] = serializer.SerializeToString(parameters[i]);

                        string[] serializedParamTypes = new string[parameters.Length];
                        for (int i = 0; i < parameters.Length; i++)
                            serializedParamTypes[i] = MaybeConvertToJavaType(parameters[i].GetType().Name);

                        string guid = Guid.NewGuid().ToString();
                        StringSerializedParametersDTO paramDTO = new StringSerializedParametersDTO() { ResourceName = "request", SerializedParameters = serializedParams, ParameterTypeNames = serializedParamTypes, Guid = guid };
                        string serializedParamDTO = serializer.SerializeToString(paramDTO);

                        try
                        {
                            serializedParamDTO = ScrambleEscapedXmlTags(serializedParamDTO);
                            serializedParamDTO = AppendSerializationNumber(serializedParamDTO, serializationNumber);
                            string serializedResultDTO = middlewareClient.Request(serializedParamDTO);
                            if (serializedResultDTO == null)
                                return null;

                            int serNum = ExtractSerializationNumber(serializedParamDTO);
                            if (serializationNumber != serNum)
                            {
                                Console.WriteLine("CommunicationClient: Lost string req-rep message resurfaced, and was discarded");
                                return null;
                            }
                            serializedParamDTO = RemoveSerializationNumber(serializedParamDTO);
                            serializedResultDTO = UnscrambleEscapedXmlTags(serializedResultDTO);

                            try
                            {
                                StringSerializedResultDTO resultDTO = (StringSerializedResultDTO) serializer.Deserialize(serializedResultDTO, typeof(StringSerializedResultDTO));
                                string serializedResult = resultDTO.SerializedResult;
                                object result = resultDTO.ResultTypeName == null ? null : serializer.Deserialize(serializedResult, Type.GetType(MaybeConvertFromJavaType(resultDTO.ResultTypeName), true, false));
                                return result;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("CommunicationClient: string Request-Reply Error 1");
                                Console.WriteLine("CommunicationClient: [serializedParams: " + serializedParams + "]");
                                Console.WriteLine("CommunicationClient: [guid: " + guid + "]");
                                Console.WriteLine("CommunicationClient: [paramDTO: " + paramDTO + "]");
                                Console.WriteLine("CommunicationClient: [serializedParamDTO: " + serializedParamDTO + "]");
                                Console.WriteLine("CommunicationClient: [serializedResultDTO: " + serializedResultDTO + "]");
                                Console.WriteLine(e.Message);
                                return null;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("CommunicationClient: string Request-Reply Error 2");
                            Console.WriteLine("CommunicationClient: [serializedParams: " + serializedParams + "]");
                            Console.WriteLine("CommunicationClient: [guid: " + guid + "]");
                            Console.WriteLine("CommunicationClient: [paramDTO: " + paramDTO + "]");
                            Console.WriteLine("CommunicationClient: [serializedParamDTO: " + serializedParamDTO + "]");
                            Console.WriteLine(e.Message);
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("CommunicationClient: string Request-Reply Error 3");
                        Console.WriteLine(e.Message);
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        byte[][] serializedParams = new byte[parameters.Length][];
                        for (int i = 0; i < parameters.Length; i++)
                            serializedParams[i] = serializer.SerializeToByte(parameters[i]);

                        string[] serializedParamTypes = new string[parameters.Length];
                        for (int i = 0; i < parameters.Length; i++)
                            serializedParamTypes[i] = MaybeConvertToJavaType(parameters[i].GetType().Name);

                        string guid = Guid.NewGuid().ToString();
                        BinarySerializedParametersDTO paramDTO = new BinarySerializedParametersDTO() { ResourceName = "request", SerializedParameters = serializedParams, ParameterTypeNames = serializedParamTypes, Guid = guid };
                        byte[] serializedParamDTO = serializer.SerializeToByte(paramDTO);

                        try
                        {
                            serializedParamDTO = AppendSerializationNumber(serializedParamDTO, serializationNumber);
                            byte[] serializedResultDTO = middlewareClient.Request(serializedParamDTO);
                            if (serializedResultDTO == null) return null;

                            int serNum = ExtractSerializationNumber(serializedParamDTO);
                            if (serializationNumber != serNum)
                            {
                                Console.WriteLine("CommunicationClient: Lost binary req-rep message resurfaced, and was discarded");
                                return null;
                            }
                            serializedParamDTO = RemoveSerializationNumber(serializedParamDTO);

                            try
                            {
                                BinarySerializedResultDTO resultDTO = (BinarySerializedResultDTO)serializer.Deserialize(serializedResultDTO, typeof(BinarySerializedResultDTO));
                                byte[] serializedResult = resultDTO.SerializedResult;
                                object result = serializer.Deserialize(serializedResult, Type.GetType(MaybeConvertFromJavaType(resultDTO.ResultTypeName), true, false));
                                return result;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("CommunicationClient: Binary Request-Reply Error 1");
                                Console.WriteLine("CommunicationClient: [serializedParams: " + serializedParams + "]");
                                Console.WriteLine("CommunicationClient: [guid: " + guid + "]");
                                Console.WriteLine("CommunicationClient: [paramDTO: " + paramDTO + "]");
                                Console.WriteLine("CommunicationClient: [serializedParamDTO: " + serializedParamDTO + "]");
                                Console.WriteLine("CommunicationClient: [serializedResultDTO: " + serializedResultDTO + "]");
                                Console.WriteLine(e.Message);
                                return null;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("CommunicationClient: Binary Request-Reply Error 2");
                            Console.WriteLine("CommunicationClient: [serializedParams: " + serializedParams + "]");
                            Console.WriteLine("CommunicationClient: [guid: " + guid + "]");
                            Console.WriteLine("CommunicationClient: [paramDTO: " + paramDTO + "]");
                            Console.WriteLine("CommunicationClient: [serializedParamDTO: " + serializedParamDTO + "]");
                            Console.WriteLine(e.Message);
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("CommunicationClient: Binary Request-Reply Error 3");
                        Console.WriteLine(e.Message);
                        return null;
                    }
                }
            }
        }

        public string Push(params object[] parameters) => Push(-1, parameters);

        // TODO: Change to allow multiple parameters.
        public string Push(int priority, params object[] parameters)
        {
            if (bypassSendingSerialization)
            {
                if (IsStringNotBinary())
                    middlewareClient.Push(parameters[0] as string);
                else
                    middlewareClient.Push(parameters[0] as byte[]);

                return null;
            }
            else
            {
                string guid = Guid.NewGuid().ToString();

                if (IsStringNotBinary())
                {
                    string[] serializedParams = new string[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                        serializedParams[i] = serializer.SerializeToString(parameters[i]);

                    string[] serializedParamTypes = new string[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                        serializedParamTypes[i] = MaybeConvertToJavaType(parameters[i].GetType().Name);

                    StringSerializedParametersDTO paramDTO = new StringSerializedParametersDTO() { ResourceName = "push", SerializedParameters = serializedParams, ParameterTypeNames = serializedParamTypes, Guid = guid };
                    string serializedParamDTO = serializer.SerializeToString(paramDTO);
                    serializedParamDTO = ScrambleEscapedXmlTags(serializedParamDTO);
                    serializedParamDTO = AppendSerializationNumber(serializedParamDTO, serializationNumber);
                    if (priority == -1)
                        middlewareClient.Push(serializedParamDTO);
                    else
                        middlewareClient.Push(serializedParamDTO, priority);
                }
                else
                {
                    byte[][] serializedParams = new byte[parameters.Length][];
                    for (int i = 0; i< parameters.Length; i++)
                      serializedParams[i] = serializer.SerializeToByte(parameters[i]);
        
                    string[] serializedParamTypes = new string[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                        serializedParamTypes[i] = MaybeConvertToJavaType(parameters[i].GetType().Name);

                    BinarySerializedParametersDTO paramDTO = new BinarySerializedParametersDTO() { ResourceName = "push", SerializedParameters = serializedParams, ParameterTypeNames = serializedParamTypes, Guid = guid };
                    byte[] serializedParamDTO = serializer.SerializeToByte(paramDTO);
                    serializedParamDTO = AppendSerializationNumber(serializedParamDTO, serializationNumber);
                    if (priority == -1)
                        middlewareClient.Push(serializedParamDTO);
                    else
                        middlewareClient.Push(serializedParamDTO, priority);
                }

                return guid;
            }
        }

        public void subscribe(Action<object, string> subscribeCallback)
        {
            if (!MiddlewareTypeHelper.SupportsPubSub()) throw new ArgumentException("The middleware does not support Publish-Subscribe!");

            middlewareClient.SubscribeString(s =>
                {
                    if (s != null && s.Length > 0)
                    {
                        if (bypassReceivingSerialization)
                        {
                            subscribeCallback(s, null);
                        }
                        else
                        {
                            try
                            {
                                int serNum = ExtractSerializationNumber(s);
                                if (serializationNumber != serNum)
                                {
                                    Console.WriteLine("CommunicationClient: Lost string pub-sub message resurfaced, and was discarded");
                                }
                                else
                                {
                                    s = RemoveSerializationNumber(s);
                                    s = UnscrambleEscapedXmlTags(s);
                                    StringSerializedParametersDTO paramsDTO = (StringSerializedParametersDTO)serializer.Deserialize(s, typeof(StringSerializedParametersDTO));
                                    string[] serializedParams = paramsDTO.SerializedParameters;
                                    object[] parameters = new object[serializedParams.Length];
                                    for (int i = 0; i < serializedParams.Length; i++)
                                        parameters[i] = serializer.Deserialize(serializedParams[i], Type.GetType(MaybeConvertFromJavaType(paramsDTO.ParameterTypeNames[i]), true, false));

                                    if (parameters != null)
                                        subscribeCallback(parameters, paramsDTO.Guid);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("CommunicationClient: Error deserializing string published data");
                                Console.WriteLine("CommunicationClient: [Serializer: " + serializer.SerializerName + "]");
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                });

            middlewareClient.SubscribeBinary(b =>
            {
                if (b != null && b.Length > 0)
                {
                    if (bypassReceivingSerialization)
                    {
                        subscribeCallback(b, null);
                    }
                    else
                    {
                        try
                        {
                            int serNum = ExtractSerializationNumber(b);
                            if (serializationNumber != serNum)
                            {
                                Console.WriteLine("CommunicationClient: Lost binary pub-sub message resurfaced, and was discarded");
                            }
                            else
                            {
                                b = RemoveSerializationNumber(b);
                                BinarySerializedParametersDTO paramsDTO = (BinarySerializedParametersDTO)serializer.Deserialize(b, typeof(BinarySerializedParametersDTO));
                                byte[][] serializedParams = paramsDTO.SerializedParameters;
                                object[] parameters = new object[serializedParams.Length];
                                for (int i = 0; i < serializedParams.Length; i++)
                                    parameters[i] = serializer.Deserialize(serializedParams[i], Type.GetType(MaybeConvertFromJavaType(paramsDTO.ParameterTypeNames[i]), true, false));

                                if (parameters != null)
                                    subscribeCallback(parameters, paramsDTO.Guid);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("CommunicationClient: Error deserializing binary published data");
                            Console.WriteLine("CommunicationClient: [Serializer: " + serializer.SerializerName + "]");
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            });
        }

        public void shutdown()
        {
            middlewareClient.Shutdown();
            middlewareClient = null;
            serializer = null;
        }
    }
}



