using System;
using System.Reflection;
using System.Reflection.Emit;
using MicroNetCore.Models.Extensions;
using MicroNetCore.Models.Markup.Extensions;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models
{
    public sealed class ViewModelGenerator : IViewModelGenerator
    {
        private readonly ModuleBuilder _moduleBuilder;
        private readonly IViewModelPropertyGenerator _propertyGenerator;

        public ViewModelGenerator(IViewModelPropertyGenerator propertyGenerator)
        {
            _propertyGenerator = propertyGenerator;
            _moduleBuilder = CreateModuleBuilder();
        }

        #region Constants

        private const TypeAttributes VmTypeAttributes =
            TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;

        #endregion

        #region IViewModelGenerator

        public Type CreateGetModel(Type type)
        {
            ValidateDataModel(type);

            var name = $"{type.Name}GetViewModel";
            var vmBaseType = typeof(IResponseViewModel<>).MakeGenericType(type);

            var typeBuilder = GetTypeBuilder(name, vmBaseType);

            foreach (var property in type.GetShowProperties())
                _propertyGenerator.Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public Type CreatePostModel(Type type)
        {
            ValidateDataModel(type);

            var name = $"{type.Name}PostViewModel";
            var viewModel = typeof(IRequestViewModel<>).MakeGenericType(type);

            var typeBuilder = GetTypeBuilder(name, viewModel);

            foreach (var property in type.GetAddProperties())
                _propertyGenerator.Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public Type CreatePutModel(Type type)
        {
            ValidateDataModel(type);

            var name = $"{type.Name}PutViewModel";
            var viewModel = typeof(IRequestViewModel<>).MakeGenericType(type);

            var typeBuilder = GetTypeBuilder(name, viewModel);

            foreach (var property in type.GetEditProperties())
                _propertyGenerator.Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        #endregion

        #region Helpers

        private static void ValidateDataModel(Type type)
        {
            if (!type.IsEntityModel())
                throw new Exception($"Type {type} is not a Model type.");
        }

        private TypeBuilder GetTypeBuilder(string name, Type viewModelType)
        {
            var builder = _moduleBuilder.DefineType(name, VmTypeAttributes);
            builder.AddInterfaceImplementation(viewModelType);

            return builder;
        }

        private static AssemblyBuilder CreateAssemblyBuilder()
        {
            var assemblyName = new AssemblyName(Guid.NewGuid().ToString());
            return AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        private static ModuleBuilder CreateModuleBuilder()
        {
            var moduleName = Guid.NewGuid().ToString();
            return CreateAssemblyBuilder().DefineDynamicModule(moduleName);
        }
        
        #endregion
    }
}