namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes
{
    public sealed class TitleAttribute : HypermediaAttribute
    {
        public TitleAttribute(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}