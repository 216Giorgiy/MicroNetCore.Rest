using System.Text;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.MediaTypes
{
    public interface IRestSerializer
    {
        string Serialize(IRestResult result, Encoding encoding);
    }
}