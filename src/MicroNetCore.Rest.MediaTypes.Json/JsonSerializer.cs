using System.Text;
using MicroNetCore.Rest.DataTransferObjects;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.MediaTypes.Json
{
    public sealed class JsonSerializer : IJsonSerializer
    {
        public string Serialize(RestObject obj, Encoding encoding)
        {
            return JsonConvert.SerializeObject(obj.Object);
        }
    }
}