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
        /// Retrieves a cart
        /// </summary>
        Cart Get(string id);

        /// <summary>
        /// Deletes a cart
        /// </summary>
        void Delete(string id);

        /// <summary>
        /// Clears all items in a cart
        /// </summary>
        Cart Clear(string id);

        /// <summary>
        /// Adds an item to a cart
        /// </summary>
        Item AddItem(string id, string product, int quantity);

        /// <summary>
        /// Updates an item in a cart
        /// </summary>
        Item UpdateItem(string id, string itemId, string product, int quantity);

        /// <summary>
        /// Removes an item from a cart
        /// </summary>
        void RemoveItem(string id, string itemId);
    }
}