using System.Collections.Generic;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicroNetCore.Rest.Models
{
    public sealed class ViewModelResultFilter : IResultFilter
    {
        private readonly IViewModelConverter _viewModelConverter;

        public ViewModelResultFilter(IViewModelConverter viewModelConverter)
        {
            _viewModelConverter = viewModelConverter;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is IEntityModel entityModel)
                {
                    objectResult.Value = _viewModelConverter.Convert(entityModel);
                }
                else if (objectResult.Value is IEnumerable<IEntityModel> entityModels)
                {
                    objectResult.Value = _viewModelConverter.Convert(entityModels);
                }
                else if (objectResult.Value is IEnumerablePage<IEntityModel> page)
                {
                    objectResult.Value = _viewModelConverter.Convert(page);
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}