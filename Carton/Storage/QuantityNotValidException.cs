using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Carton.Model;

namespace Carton.Storage
{
    [Serializable]
    internal class QuantityNotValidException : Exception
    {
        public QuantityNotValidException()
        {
        }

        public QuantityNotValidException(string message) : base(message)
        {
        }

        public QuantityNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QuantityNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}