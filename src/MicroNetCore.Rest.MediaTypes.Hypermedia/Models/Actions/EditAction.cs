using System;
using System.Collections.Generic;
using System.Net.Http;
using Humanizer;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Models.Actions
{
    public sealed class EditAction : Action
    {
        public EditAction(Type modelType, string href, IEnumerable<Field> fields)
        {
            Name = $"edit-{modelType.Name.Camelize()}";
            Href = href;
            Method = HttpMethod.Put.ToString();
            Title = $"Edit {modelType.Name}";
            Type = "application/json";
            Fields = fields;
        }
    }
}