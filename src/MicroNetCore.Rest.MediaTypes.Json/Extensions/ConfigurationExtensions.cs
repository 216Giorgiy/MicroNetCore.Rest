using MicroNetCore.Rest.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.MediaTypes.Json.Extensions
{
    public static class ConfigurationExtensions
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static IRestBuilder AddJson(this IRestBuilder builder, JsonSerializerSettings jsonSerializerSettings = null)
        {
            // Services
            builder.ServiceCollection.AddTransient<IJsonSerializer, JsonSerializer>();
            builder.ServiceCollection.AddSingleton(jsonSerializerSettings ?? DefaultJsonSerializerSettings);

            // Formatters
            builder.AddOutputFormatter(new JsonOutputFormatter());

            return builder;
        }
    }
}