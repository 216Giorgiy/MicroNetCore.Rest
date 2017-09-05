using MicroNetCore.Rest.Hypermedia.Extensions;
using MicroNetCore.Rest.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddRest(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRestService<>), typeof(RestService<>));

            services.AddRestHypermedia();

            return services;
        }
    }
}