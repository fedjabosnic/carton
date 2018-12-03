using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carton.Model
{
    /// <summary>
    /// Represents an item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The product's unique identifier
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// The product's name or id'
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// The quantity of the product
        /// </summary>
        public int Quantity { get; set; }
    }
}