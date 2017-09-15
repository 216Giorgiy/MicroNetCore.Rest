using System;
using System.Net.Http;
using Humanizer;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Actions
{
    public sealed class DeleteAction : Action
    {
        public DeleteAction(Type modelType, string href)
        {
            Name = $"delete-{modelType.Name.Camelize()}";
            Href = href;
            Method = HttpMethod.Delete.ToString();
            Title = $"Delete {modelType.Name}";
        }
    }
}