using System;

namespace Carton.Service.Utilities
{
    internal class Time : ITime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}