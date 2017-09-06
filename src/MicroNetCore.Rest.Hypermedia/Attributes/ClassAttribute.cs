namespace MicroNetCore.Rest.Hypermedia.Attributes
{
    public sealed class ClassAttribute : HypermediaAttribute
    {
        public ClassAttribute(string @class)
        {
            Class = @class;
        }

        public string Class { get; }
    }
}