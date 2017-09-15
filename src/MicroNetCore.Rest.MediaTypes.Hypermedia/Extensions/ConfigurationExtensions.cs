using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IRestBuilder AddHypermedia(this IRestBuilder builder)
        {
            // Services
            builder.ServiceCollection.AddSingleton<IApiHelper, ApiHelper>();
            builder.ServiceCollection.AddTransient<IHypermediaSerializer, HypermediaSerializer>();
            builder.ServiceCollection.AddTransient<IHypermediaActionFormService, HypermediaActionsFormService>();
            builder.ServiceCollection.AddTransient<IHypermediaActionsService, HypermediaActionsService>();
            builder.ServiceCollection.AddTransient<IHypermediaClassService, HypermediaClassService>();
            builder.ServiceCollection.AddSingleton<IHypermediaFieldService, HypermediaFieldService>();
            builder.ServiceCollection.AddTransient<IHypermediaLinksService, HypermediaLinksService>();
            builder.ServiceCollection.AddTransient<IHypermediaPropertiesService, HypermediaPropertiesService>();
            builder.ServiceCollection.AddTransient<IHypermediaSubEntitiesService, HypermediaSubEntitiesService>();
            builder.ServiceCollection.AddTransient<IHypermediaTitleGenerator, HypermediaTitleService>();

            // Formatters
            builder.AddOutputFormatter(new HypermediaOutputFormatter());

            return builder;
        }
    }
}