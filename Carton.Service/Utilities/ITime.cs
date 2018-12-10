using System;

namespace Carton.Service.Utilities
{
    internal interface ITime
    {
        DateTime UtcNow { get; }
    }
}