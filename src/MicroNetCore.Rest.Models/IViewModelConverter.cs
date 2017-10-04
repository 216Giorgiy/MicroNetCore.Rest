using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using MicroNetCore.Rest.Abstractions;

namespace MicroNetCore.Rest.Models
{
    public interface IViewModelConverter
    {
        IResponseViewModel Convert(IEntityModel model);
        IEnumerable<IResponseViewModel> Convert(IEnumerable<IEntityModel> models);
        IEnumerablePage<IResponseViewModel> Convert(IEnumerablePage<IEntityModel> page);
    }
}