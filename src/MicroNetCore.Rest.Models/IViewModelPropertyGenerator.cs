using System.Reflection;
using System.Reflection.Emit;

namespace MicroNetCore.Rest.Models
{
    public interface IViewModelPropertyGenerator
    {
        void Addproperty(TypeBuilder typeBuilder, PropertyInfo property);
    }
}