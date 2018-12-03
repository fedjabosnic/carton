using System;

namespace Carton.Utilities
{
    internal class Time : ITime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}