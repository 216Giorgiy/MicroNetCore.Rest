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

        private const TypeAttributes Attributes = TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;

        public ViewModelGenerator()
        {
            _moduleBuilder = CreateModuleBuilder();
        }

        #region IViewModelGenerator

        public Type CreatePostModel(Type type)
        {
            ValidateDataModel(type);

            var typeBuilder = GetTypeBuilder(type, "PostModel");

            foreach (var property in type.GetAddProperties())
                Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public Type CreatePutModel(Type type)
        {
            ValidateDataModel(type);

            var typeBuilder = GetTypeBuilder(type, "PutModel");

            foreach (var property in type.GetEditProperties())
                Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public Type CreateGetModel(Type type)
        {
            ValidateDataModel(type);

            var typeBuilder = GetTypeBuilder(type, "ShowModel");

            foreach (var property in type.GetShowProperties())
                Addproperty(typeBuilder, property);

            return typeBuilder.CreateTypeInfo().AsType();
        }
        
        #endregion

        #region Helpers

        private static MethodAttributes MethodAttributes =>
            MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

        private static void ValidateDataModel(Type type)
        {
            if (!type.GetTypeInfo().IsSubclassOf(typeof(IModel)))
                throw new Exception($"Type {type} is not a Model type.");
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

        private TypeBuilder GetTypeBuilder(Type model, string postfix)
        {
            var name = $"{model.Name}{postfix}";
            var viewModel = typeof(IViewModel<>).MakeGenericType(model);

            return _moduleBuilder.DefineType(name, Attributes, viewModel);
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
                MethodAttributes,
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
                MethodAttributes,
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