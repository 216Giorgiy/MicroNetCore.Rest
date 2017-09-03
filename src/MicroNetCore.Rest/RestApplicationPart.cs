using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MicroNetCore.Rest
{
    public sealed class RestApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        public RestApplicationPart(IEnumerable<Type> types)
        {
            Name = nameof(RestApplicationPart);
            Types = types.Select(t => t.GetTypeInfo());
        }

        public override string Name { get; }
        public IEnumerable<TypeInfo> Types { get; }
    }
}