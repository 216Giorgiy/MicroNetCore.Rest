using System;
using System.Collections.Generic;
using MicroNetCore.Rest.Hypermedia.Extensions;
using MicroNetCore.Rest.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddMvcWithRestControllers(this IServiceCollection services,
            IEnumerable<Type> restTypes)
        {
            services.AddCustomMvc()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(new RestApplicationPart(restTypes)));

            services.AddTransient(typeof(IRestService<>), typeof(RestService<>));
            services.AddRestHypermedia();

            return services;
        }

        #region Helpers

        private static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
        {
            var builder = services.AddMvcCore();

            builder.AddMvcOptions(o => o.RespectBrowserAcceptHeader = true);

            builder.AddJsonFormatters();
            builder.AddJsonOptions(o => o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);

            builder.AddXmlDataContractSerializerFormatters();

            return builder;
        }

        #endregion
    }
}