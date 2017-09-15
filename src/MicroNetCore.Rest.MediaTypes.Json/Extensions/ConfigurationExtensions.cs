using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.MediaTypes.Json.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddJson(this IServiceCollection services)
        {
            services.AddTransient<IJsonSerializer, JsonSerializer>();

            return services;
        }
    }
}