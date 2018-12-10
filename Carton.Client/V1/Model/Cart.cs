using System;
using System.Collections.Generic;

namespace Carton.Client.V1.Model
{
    public class Cart
    {
        public string CartId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public List<Item> Items { get; set; }
    }
}
