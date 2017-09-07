using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
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
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(GetRestApplicationPart(restTypes)));

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

        private static RestApplicationPart GetRestApplicationPart(IEnumerable<Type> types)
        {
            var moduleBuilder = CreateModuleBuilder();

            var controllerTypes = types.Select(t => CreateControllerType(moduleBuilder, t));
            var applicationPart = new RestApplicationPart(controllerTypes);

            return applicationPart;
        }

        private static ModuleBuilder CreateModuleBuilder()
        {
            var assemblyName = new AssemblyName(Guid.NewGuid().ToString());
            var assembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            return assembly.DefineDynamicModule(Guid.NewGuid().ToString());
        }

        private static Type CreateControllerType(ModuleBuilder moduleBuilder, Type modelType)
        {
            const TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;

            var typeName = modelType.GetControllerName();
            var parentType = modelType.GetControllerType();

            var typeBuidler = moduleBuilder
                .DefineType(typeName, typeAttributes, parentType)
                .WithPassThroughConstructors(parentType);

            return typeBuidler.CreateType();
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static string GetControllerName(this Type modelType)
        {
            return $"{modelType.Name.Pluralize()}Controller";
        }

        private static Type GetControllerType(this Type modelType)
        {
            return typeof(RestController<>).MakeGenericType(modelType);
        }

        #endregion
    }
}