using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Serialization
{
    public class JsonNewtonsoftSerializer : StringSerializerBase
    {
        public override string SerializerName => "JSON (GSON)";
        public override bool StringNotBinary => true;
        public override bool SupportsCompression => true;

        private readonly JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public override string SerializeToString<T>(T data) => JsonConvert.SerializeObject(data, settings);
        public override object Deserialize(string serialized, Type type) => JsonConvert.DeserializeObject(serialized, type, settings);
    }
}