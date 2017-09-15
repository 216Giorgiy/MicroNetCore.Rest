using MicroNetCore.Rest.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.MediaTypes.Json.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IRestBuilder AddJson(this IRestBuilder builder)
        {
            // Services
            builder.ServiceCollection.AddTransient<IJsonSerializer, JsonSerializer>();

            // Formatters
            builder.AddOutputFormatter(new JsonOutputFormatter());

            return builder;
        }
    }
}