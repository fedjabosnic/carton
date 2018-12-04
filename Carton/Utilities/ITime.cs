using System;

namespace Carton.Utilities
{
    internal interface ITime
    {
        DateTime UtcNow { get; }
    }
}