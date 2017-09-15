using System.Text;
using MicroNetCore.Rest.DataTransferObjects;

namespace MicroNetCore.Rest.MediaTypes
{
    public interface IObjectSerializer
    {
        string Serialize(RestObject obj, Encoding encoding);
    }
}