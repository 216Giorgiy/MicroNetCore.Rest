using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MicroNetCore.Rest
{
    public sealed class RestApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        private const string ApplicationPartName = "RestControllers";

        public RestApplicationPart(IEnumerable<Type> types)
        {
            Types = types.Select(t => t.GetTypeInfo());
        }

        public override string Name => ApplicationPartName;
        public IEnumerable<TypeInfo> Types { get; }
    }
}