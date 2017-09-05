using MicroNetCore.Rest.Hypermedia.Helpers;
using MicroNetCore.Rest.Hypermedia.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Hypermedia.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddRestHypermedia(this IServiceCollection services)
        {
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddTransient<IHypermediaService, HypermediaService>();
            services.AddTransient<IHypermediaActionsGenerator, HypermediaActionsGenerator>();
            services.AddTransient<IHypermediaClassGenerator, HypermediaClassGenerator>();
            services.AddTransient<IHypermediaLinksGenerator, HypermediaLinksGenerator>();
            services.AddTransient<IHypermediaPropertiesGenerator, HypermediaPropertiesGenerator>();
            services.AddTransient<IHypermediaSubEntitiesGenerator, HypermediaSubEntitiesGenerator>();
            services.AddTransient<IHypermediaTitleGenerator, HypermediaTitleGenerator>();

            return services;
        }
    }
}