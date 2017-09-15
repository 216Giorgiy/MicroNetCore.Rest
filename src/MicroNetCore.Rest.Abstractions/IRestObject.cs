using System;

namespace MicroNetCore.Rest.Abstractions
{
    public interface IRestResult
    {
        Type Type { get; }
        object Object { get; }
    }
}