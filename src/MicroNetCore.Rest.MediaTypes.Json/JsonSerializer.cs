using Newtonsoft.Json;

namespace MicroNetCore.Rest.MediaTypes.Json
{
    public sealed class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}