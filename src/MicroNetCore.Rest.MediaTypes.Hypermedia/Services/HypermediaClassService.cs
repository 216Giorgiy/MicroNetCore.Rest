using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Attributes;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaClassService : IHypermediaClassService
    {
        #region Helpers

        private static string GetClass(MemberInfo viewModelType)
        {
            return viewModelType.GetCustomAttribute<ClassAttribute>()?.Class.Camelize() ??
                   viewModelType.Name.Camelize();
        }

        #endregion

        #region IHypermediaClassService

        public string[] Get(IResponseViewModel viewModel)
        {
            return new[]
            {
                GetClass(viewModel.GetType())
            };
        }

        public string[] Get(IEnumerable<IResponseViewModel> viewModels)
        {
            return new[]
            {
                "collection",
                GetClass(viewModels.GetType().GetGenericArguments().First())
            };
        }

        public string[] Get(IEnumerablePage<IResponseViewModel> page)
        {
            return new[]
            {
                "page",
                GetClass(page.GetType().GetGenericArguments().First())
            };
        }

        #endregion

        #region Static

        private static readonly IDictionary<Type, string> Cache;

        static HypermediaClassService()
        {
            Cache = new Dictionary<Type, string>();
        }

        #endregion
    }
}