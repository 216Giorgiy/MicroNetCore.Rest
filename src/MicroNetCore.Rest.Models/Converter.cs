using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Models
{
    internal static class Converter
    {
        public static object Convert<TSource, TDestination>(object source)
            where TSource : class
            where TDestination : class, new()
        {
            var destination = new TDestination();

            foreach (var destinationProperty in typeof(TDestination).GetProperties())
            {
                var sourceProperty = typeof(TSource).GetProperty(destinationProperty.Name);

                if (IsSimpleCopy(sourceProperty, destinationProperty))
                {
                    var value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);

                    continue;
                }

                if (IsRelationModelCopy(sourceProperty, destinationProperty))
                {

                    continue;
                }
            }

            return destination;
        }

        private static bool IsSimpleCopy(PropertyInfo source, PropertyInfo destination)
        {
            return source.PropertyType == destination.PropertyType;
        }

        private static bool IsRelationModelCopy(PropertyInfo source, PropertyInfo destination)
        {
            return typeof(IEnumerable<IRelationModel>).IsAssignableFrom(source.PropertyType) &&
                   source.PropertyType.GetGenericArguments().Contains(destination.PropertyType);
        }
    }
}