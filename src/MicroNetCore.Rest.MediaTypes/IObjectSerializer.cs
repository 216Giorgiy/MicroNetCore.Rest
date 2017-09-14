namespace MicroNetCore.Rest.MediaTypes
{
    public interface IObjectSerializer
    {
        string Serialize(object obj);
    }
}