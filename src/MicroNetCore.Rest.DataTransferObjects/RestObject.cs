using System;

namespace MicroNetCore.Rest.DataTransferObjects
{
    public abstract class RestObject
    {
        protected RestObject(Type type, object o)
        {
            Type = type;
            Object = o;
        }

        public Type Type { get; }
        public object Object { get; }
    }
}