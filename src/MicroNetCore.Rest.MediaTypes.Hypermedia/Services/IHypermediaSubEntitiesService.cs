using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaSubEntitiesService
    {
        SubEntity[] Get(IResponseViewModel viewModel);
        SubEntity[] Get(IEnumerable<IResponseViewModel> viewModels);
        SubEntity[] Get(IEnumerablePage<IResponseViewModel> page);
    }
}