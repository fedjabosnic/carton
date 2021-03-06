using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Carton.Service.Model;

namespace Carton.Service.Model.Exceptions
{
    [Serializable]
    internal class CartNotFoundException : Exception
    {
        public CartNotFoundException()
        {
        }

        public CartNotFoundException(string message) : base(message)
        {
        }

        public CartNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CartNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}