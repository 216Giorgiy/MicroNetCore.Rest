using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddRest(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRestService<>), typeof(RestService<>));

            return services;
        }
    }
}