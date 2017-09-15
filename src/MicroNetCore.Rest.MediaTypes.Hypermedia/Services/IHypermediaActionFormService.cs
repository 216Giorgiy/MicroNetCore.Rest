using System;
using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public interface IHypermediaActionFormService
    {
        IEnumerable<Field> GetAddForm(Type model);
        IEnumerable<Field> GetEditForm(Type model);
    }
}