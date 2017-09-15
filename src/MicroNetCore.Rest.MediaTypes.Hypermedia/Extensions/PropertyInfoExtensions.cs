using System.Collections.Generic;
using System.Reflection;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Extensions
{
    internal static class PropertyInfoExtensions
    {
        public static bool IsSubEntityType(this PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            return typeof(IModel).IsAssignableFrom(type) || typeof(IEnumerable<IModel>).IsAssignableFrom(type);
        }
    }
}