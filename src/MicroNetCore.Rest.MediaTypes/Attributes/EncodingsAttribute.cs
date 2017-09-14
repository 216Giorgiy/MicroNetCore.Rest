using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNetCore.Rest.MediaTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EncodingsAttribute : Attribute
    {
        public EncodingsAttribute(params EncodingCode[] encodings)
        {
            Encodings = encodings.Select(ec => Encoding.GetEncoding((int) ec));
        }

        public IEnumerable<Encoding> Encodings { get; }
    }
}