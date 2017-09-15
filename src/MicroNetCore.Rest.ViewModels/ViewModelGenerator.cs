using System;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup.Extensions;

namespace MicroNetCore.Rest.ViewModels
{
    public sealed class ViewModelGenerator : IViewModelGenerator
    {
        private readonly ModuleBuilder _moduleBuilder;

        public ViewModelGenerator()
        {
            _moduleBuilder = CreateModuleBuilder();
        }

        #region Constants

        private const TypeAttributes VmTypeAttributes =
            TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;

        private const MethodAttributes VmMethodAttributes =
            MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

        #endregion

        #region IViewModelGenerator

        public Type CreateGetModel(Type type)
        {
            ValidateDataModel(type);

            var name = $"{type.Name}GetViewModel";
            var viewModel = typeof(IResponseViewModel<>).MakeGenericType(type);

            var typeBuilder = GetTypeBuilder(name, viewModel);

            foreach (var property in type.GetShowProperties())
                Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public Type CreatePostModel(Type type)
        {
            ValidateDataModel(type);

            var name = $"{type.Name}PostViewModel";
            var viewModel = typeof(IRequestViewModel<>).MakeGenericType(type);

            var typeBuilder = GetTypeBuilder(name, viewModel);

            foreach (var property in type.GetAddProperties())
                Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public Type CreatePutModel(Type type)
        {
            ValidateDataModel(type);

            var name = $"{type.Name}PutViewModel";
            var viewModel = typeof(IRequestViewModel<>).MakeGenericType(type);

            var typeBuilder = GetTypeBuilder(name, viewModel);

            foreach (var property in type.GetEditProperties())
                Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        #endregion

        #region Helpers

        private static void ValidateDataModel(Type type)
        {
            if (!typeof(IModel).IsAssignableFrom(type))
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

        private static void Addproperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                property.PropertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                property.PropertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static MethodBuilder GetSetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo field)
        {
            var setMethod = typeBuilder.DefineMethod(
                $"set_{property.Name}",
                VmMethodAttributes,
                null,
                new[] {property.PropertyType});

            var ilGenerator = setMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Stfld, field);
            ilGenerator.Emit(OpCodes.Ret);

            return setMethod;
        }

        private static MethodBuilder GetGetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo field)
        {
            var getMethod = typeBuilder.DefineMethod(
                $"get_{property.Name}",
                VmMethodAttributes,
                property.PropertyType,
                Type.EmptyTypes);

            var ilGenerator = getMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, field);
            ilGenerator.Emit(OpCodes.Ret);

            return getMethod;
        }

        #endregion
    }
}