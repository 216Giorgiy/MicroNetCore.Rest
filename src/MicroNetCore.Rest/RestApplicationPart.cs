using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using MicroNetCore.Rest.Extensions;
using MicroNetCore.Rest.ViewModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MicroNetCore.Rest
{
    public sealed class RestApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        private const string ApplicationPartName = "RestControllers";

        public RestApplicationPart(IEnumerable<Type> types)
        {
            var assemblyName = new AssemblyName(Guid.NewGuid().ToString());
            var assembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            var moduleBuilder = assembly.DefineDynamicModule(Guid.NewGuid().ToString());

            Types = types.Select(t => CreateControllerType(moduleBuilder, t).GetTypeInfo());
        }

        public override string Name => ApplicationPartName;
        public IEnumerable<TypeInfo> Types { get; }

        private static Type CreateControllerType(ModuleBuilder moduleBuilder, Type modelType)
        {
            const TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;

            var getType = new ViewModelGenerator().CreateGetModel(modelType);
            var postType = new ViewModelGenerator().CreatePostModel(modelType);
            var putType = new ViewModelGenerator().CreatePutModel(modelType);

            var typeName = $"{modelType.Name.Pluralize()}Controller";
            var parentType = typeof(RestController<,,,>).MakeGenericType(modelType, getType, postType, putType);

            var typeBuidler = moduleBuilder
                .DefineType(typeName, typeAttributes, parentType)
                .WithPassThroughConstructors(parentType);

            return typeBuidler.CreateType();
        }
    }
}