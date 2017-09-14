﻿using MicroNetCore.Rest.MediaTypes.Hypermedia.Helpers;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddRestHypermedia(this IServiceCollection services)
        {
            services.AddSingleton<IApiHelper, ApiHelper>();

            services.AddTransient<IHypermediaActionsService, HypermediaActionsService>();
            services.AddTransient<IHypermediaClassService, HypermediaClassService>();
            services.AddTransient<IHypermediaLinksService, HypermediaLinksService>();
            services.AddTransient<IHypermediaPropertiesService, HypermediaPropertiesService>();
            services.AddTransient<IHypermediaSubEntitiesService, HypermediaSubEntitiesService>();
            services.AddTransient<IHypermediaTitleGenerator, HypermediaTitleService>();
            services.AddSingleton<IHypermediaFieldService, HypermediaFieldService>();

            return services;
        }

        public static IMvcBuilder AddHypermediaFormatter(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(o => o.OutputFormatters.Add(new HypermediaOutputFormatter()));

            return builder;
        }

        public static IMvcCoreBuilder AddHypermediaFormatter(this IMvcCoreBuilder builder)
        {
            builder.AddMvcOptions(o => o.OutputFormatters.Add(new HypermediaOutputFormatter()));

            return builder;
        }
    }
}