using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddRestControllers(this IMvcBuilder builder, IEnumerable<Type> types)
        {
            var controllerTypes = types.Select(t => typeof(RestController<>).MakeGenericType(t));
            var applicationPart = new RestApplicationPart(controllerTypes);

            builder.ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(applicationPart));

            return builder;
        }
    }
}