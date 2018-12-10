using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carton.Service.Model
{
    /// <summary>
    /// Represents a cart
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// The cart's unique identifier
        /// </summary>
        public string CartId { get; set; }

        /// <summary>
        /// The date and time when the cart was first created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The date and time when the cart was last updated
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// The items in the cart
        /// </summary>
        public List<Item> Items { get; set; }
    }
}