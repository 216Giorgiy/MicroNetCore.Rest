using System;
using System.Collections.Generic;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IRestBuilder AddRest(this IServiceCollection services, IEnumerable<Type> types)
        {
            var restPart = new RestApplicationPart(types);

            var mvcCoreBuilder = services
                .AddMvcCore()
                .AddMvcOptions(o => o.RespectBrowserAcceptHeader = true)
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(restPart));

            return new RestBuilder(mvcCoreBuilder, services);
        }
    }
}