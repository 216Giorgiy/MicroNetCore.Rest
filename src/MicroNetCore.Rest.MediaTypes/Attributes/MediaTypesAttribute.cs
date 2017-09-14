using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;

namespace MicroNetCore.Rest.MediaTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MediaTypesAttribute : Attribute
    {
        public MediaTypesAttribute(params string[] mediaTypeName)
        {
            MediaTypes = mediaTypeName.Select(mtn => MediaTypeHeaderValue.Parse(mtn));
        }

        public IEnumerable<MediaTypeHeaderValue> MediaTypes { get; }
    }
}