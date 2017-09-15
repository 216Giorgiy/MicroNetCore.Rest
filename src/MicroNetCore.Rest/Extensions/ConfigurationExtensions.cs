using System;
using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions;
using MicroNetCore.Rest.MediaTypes.Json;
using MicroNetCore.Rest.MediaTypes.Json.Extensions;
using MicroNetCore.Rest.MediaTypes.Xml;
using MicroNetCore.Rest.MediaTypes.Xml.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddMvcWithRestControllers(this IServiceCollection services,
            IEnumerable<Type> restTypes)
        {
            services.AddCustomMvc()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(new RestApplicationPart(restTypes)));

            services.AddRestHypermedia();
            services.AddJson();
            services.AddXml();

            return services;
        }

        #region Helpers

        private static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
        {
            return services.AddMvcCore().AddAcceptHeaders().AddFormatters();
        }

        private static IMvcCoreBuilder AddAcceptHeaders(this IMvcCoreBuilder builder)
        {
            builder.AddMvcOptions(o => o.RespectBrowserAcceptHeader = true);

            return builder;
        }

        private static IMvcCoreBuilder AddFormatters(this IMvcCoreBuilder builder)
        {
            builder.AddMvcOptions(o =>
            {
                o.OutputFormatters.Add(new JsonOutputFormatter());
                o.OutputFormatters.Add(new XmlOutputFormatter());
                o.OutputFormatters.Add(new HypermediaOutputFormatter());
            });

            return builder;
        }

        #endregion
    }
}