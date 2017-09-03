using System;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MicroNetCore.Rest
{
    public sealed class RestControllerAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var modelType = controller.ControllerType.GetGenericArguments()[0];
            controller.ControllerName = modelType.Name.Pluralize();
        }
    }
}