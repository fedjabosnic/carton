using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Carton.Model;

namespace Carton.Storage
{
    [Serializable]
    internal class ProductNotValidException : Exception
    {
        public ProductNotValidException()
        {
        }

        public ProductNotValidException(string message) : base(message)
        {
        }

        public ProductNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}