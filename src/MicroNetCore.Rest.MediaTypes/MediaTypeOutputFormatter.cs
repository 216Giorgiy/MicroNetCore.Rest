using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MicroNetCore.Models;
using MicroNetCore.Rest.MediaTypes.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace MicroNetCore.Rest.MediaTypes
{
    public abstract class MediaTypeOutputFormatter<TSerializer> : TextOutputFormatter
        where TSerializer : class, IObjectSerializer
    {
        protected MediaTypeOutputFormatter()
        {
            foreach (var mediaTypeHeaderValue in GetMediaTypes())
                SupportedMediaTypes.Add(mediaTypeHeaderValue);

            foreach (var encoding in GetEncodings())
                SupportedEncodings.Add(encoding);
        }

        #region TextOutputFormatter

        public sealed override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var serializer = context.HttpContext.RequestServices.GetService<TSerializer>();

            await context.HttpContext.Response.WriteAsync(
                serializer.Serialize(context.Object),
                selectedEncoding,
                context.HttpContext.RequestAborted);
        }

        protected sealed override bool CanWriteType(Type type)
        {
            return typeof(IModel).IsAssignableFrom(type);
        }

        #endregion

        #region Helpers

        private static IEnumerable<MediaTypeHeaderValue> GetMediaTypes()
        {
            return typeof(TSerializer)
                .GetCustomAttribute<MediaTypesAttribute>()
                .MediaTypes;
        }

        private static IEnumerable<Encoding> GetEncodings()
        {
            return typeof(TSerializer)
                .GetCustomAttribute<EncodingsAttribute>()
                .Encodings;
        }

        #endregion
    }
}