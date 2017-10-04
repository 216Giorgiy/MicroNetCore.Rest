using MicroNetCore.Collections;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;
using MicroNetCore.Rest.Builder;
using MicroNetCore.Rest.Models;
using MicroNetCore.Rest.Models.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IRestBuilder AddRest(this IServiceCollection services, TypeBundle<IEntityModel> models)
        {
            var restPart = new RestApplicationPart(models.Types);

            var mvcCoreBuilder = services
                .AddMvcCore()
                .AddMvcOptions(o => o.RespectBrowserAcceptHeader = true)
                .AddMvcOptions(o => o.Filters.Add<ViewModelResultFilter>())
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(restPart));

            services.AddViewModels();

            return new RestBuilder(mvcCoreBuilder, services);
        }

        public static IApplicationBuilder UseRest(this IApplicationBuilder app)
        {
            return app.UseMvc();
        }
    }
}