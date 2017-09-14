using System.Collections.Generic;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaClassService
    {
        string[] Get(IResponseViewModel viewModel);
        string[] Get(IEnumerable<IResponseViewModel> viewModels);
        string[] Get(IEnumerablePage<IResponseViewModel> page);
    }
}