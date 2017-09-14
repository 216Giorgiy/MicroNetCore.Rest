using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaLinksService
    {
        Link[] Get(IResponseViewModel viewModel);
        Link[] Get(IEnumerable<IResponseViewModel> viewModels);
        Link[] Get(IEnumerablePage<IResponseViewModel> page);
    }
}