﻿using MicroNetCore.Models;

namespace MicroNetCore.Rest.Abstractions
{
    public interface IResponseViewModel : IViewModel
    {
        long Id { get; }
    }

    public interface IResponseViewModel<TModel> : IViewModel<TModel>
        where TModel : class, IModel
    {
    }
}