using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Carton.Model;

namespace Carton.Storage
{
    /// <summary>
    /// Represents storage for carts
    /// </summary>
    public interface ICartStore
    {
        /// <summary>
        /// Creates a new cart
        /// </summary>
        Cart Create();

        /// <summary>
        /// Retrieves an existing cart
        /// </summary>
        Cart Get(string id);
    }
}