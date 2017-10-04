using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using MicroNetCore.Models;
using MicroNetCore.Models.Extensions;

namespace MicroNetCore.Rest.Models
{
    public sealed class ViewModelPropertyGenerator : IViewModelPropertyGenerator
    {
        #region Constants

        private const MethodAttributes VmMethodAttributes =
            MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;

        #endregion

        public void Addproperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var propertyType = property.PropertyType;

            if (typeof(IEnumerable<IModel>).IsAssignableFrom(propertyType))
            {
                var modelType = propertyType.GetGenericArguments().First();

                if (modelType.IsRelationModel())
                    AddRelationCollectionProperty(typeBuilder, property);
                else if (modelType.IsEntityModel())
                    AddEntityCollectionProperty(typeBuilder, property);
                else
                    AddSimpleProperty(typeBuilder, property);
            }
            else
            {
                if (propertyType.IsEntityModel())
                    AddEntityProperty(typeBuilder, property);
                else
                    AddSimpleProperty(typeBuilder, property);
            }
        }

        private static void AddSimpleProperty(TypeBuilder typeBuilder, PropertyInfo property)
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

        // Make property of SimpleGetViewModel<> type instead of Model
        private static void AddEntityProperty(TypeBuilder typeBuilder, PropertyInfo property)
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

        // Make property of ICollection<SimpleGetViewModel<>> type instead ICollection<>
        private static void AddEntityCollectionProperty(TypeBuilder typeBuilder, PropertyInfo property)
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

        // Make property of ICollection<SimpleGetViewModel<>> type instead ICollection<>
        private static void AddRelationCollectionProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var relationType = property.PropertyType.GetGenericArguments().First();
            var relatedType = relationType.GetRelationType(property.DeclaringType);

            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                typeof(ICollection<>).MakeGenericType(relatedType),
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                typeof(ICollection<>).MakeGenericType(relatedType),
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
                new[] { property.PropertyType });

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
    }
}