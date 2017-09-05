using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MicroNetCore.Rest.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddRestControllers(this IMvcBuilder builder, IEnumerable<Type> types)
        {
            builder.AddJsonOptions(o => o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
            builder.ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(GetRestApplicationPart(types)));

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
    }
}