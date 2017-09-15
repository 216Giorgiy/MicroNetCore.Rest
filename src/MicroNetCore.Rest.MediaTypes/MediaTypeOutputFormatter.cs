using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MicroNetCore.Rest.DataTransferObjects;
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
            var restObject = (RestObject) context.Object;
            var serializer = context.HttpContext.RequestServices.GetService<TSerializer>();

            await context.HttpContext.Response.WriteAsync(
                serializer.Serialize(restObject, selectedEncoding),
                selectedEncoding,
                context.HttpContext.RequestAborted);
        }

        protected sealed override bool CanWriteType(Type type)
        {
            return typeof(RestObject).IsAssignableFrom(type);
        }

        #endregion

        #region Helpers

        private IEnumerable<MediaTypeHeaderValue> GetMediaTypes()
        {
            return GetType().GetCustomAttribute<MediaTypesAttribute>()
                .MediaTypes;
        }

        private IEnumerable<Encoding> GetEncodings()
        {
            return GetType()
                .GetCustomAttribute<EncodingsAttribute>()
                .Encodings;
        }

        #endregion
    }
}