using System;
using System.Net.Http;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Actions
{
    public sealed class AddAction : Action
    {
        public AddAction(Type modelType, string href, Field[] fields)
        {
            Name = $"add-{modelType.Name}".Camelize();
            Href = href;
            Method = HttpMethod.Post.ToString();
            Title = $"Add {modelType.Name}";
            Type = "application/json";
            Fields = fields;
        }
    }
}