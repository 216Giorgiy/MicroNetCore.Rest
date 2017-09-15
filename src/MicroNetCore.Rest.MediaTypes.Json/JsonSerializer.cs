using System.Text;
using MicroNetCore.Rest.Abstractions;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.MediaTypes.Json
{
    public sealed class JsonSerializer : IJsonSerializer
    {
        public string Serialize(IRestResult obj, Encoding encoding)
        {
            return JsonConvert.SerializeObject(obj.Object);
        }
    }
}