using System.Text;
using MicroNetCore.Rest.Abstractions;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.MediaTypes.Json
{
    public sealed class JsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;
        
        public JsonSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            _settings = jsonSerializerSettings;
        }

        public string Serialize(object obj, Encoding encoding)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }
    }
}