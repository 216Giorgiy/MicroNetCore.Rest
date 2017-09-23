using MicroNetCore.Collections;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IRestBuilder AddRest(this IServiceCollection services, TypeBundle<IModel> models)
        {
            var restPart = new RestApplicationPart(models.Types);

            var mvcCoreBuilder = services
                .AddMvcCore()
                .AddMvcOptions(o => o.RespectBrowserAcceptHeader = true)
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(restPart));

            return new RestBuilder(mvcCoreBuilder, services);
        }
    }
}