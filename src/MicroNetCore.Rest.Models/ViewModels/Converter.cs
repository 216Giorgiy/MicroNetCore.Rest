namespace MicroNetCore.Rest.Models.ViewModels
{
    internal static class Converter
    {
        public static TDestination Convert<TDestination, TSource>(TSource source)
            where TSource : class
            where TDestination : class, new()
        {
            var destination = new TDestination();

            foreach (var property in destination.GetType().GetProperties())
            {
                var value = source.GetType().GetProperty(property.Name).GetValue(source);

                if (value == null)
                    continue;

                property.SetValue(destination, value);
            }

            return destination;
        }
    }
}