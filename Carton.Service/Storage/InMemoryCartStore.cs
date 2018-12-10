using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Carton.Service.Model;
using Carton.Service.Model.Exceptions;
using Carton.Service.Storage;
using Carton.Service.Utilities;

namespace Carton.Service.Storage
{
    /// <summary>
    /// A simple in memory implementation of a cart store
    /// </summary>
    /// <remarks>
    /// Concurrency is dealt with using simple exclusive lock semantics
    /// </remarks>
    internal class InMemoryCartStore : ICartStore
    {
        private readonly ITime time;
        private readonly Dictionary<string, Cart> carts;

        public InMemoryCartStore(ITime time)
        {
            this.carts = new Dictionary<string, Cart>();
            this.time = time;
        }

        public Cart Create()
        {
            lock(carts)
            {
                var id = Guid.NewGuid().ToString();
                var now = time.UtcNow;

                carts.Add(id, new Cart { CartId = id, Created = now, Updated = now, Items = new List<Item>() });

                return carts[id];
            }
        }

        public Cart Get(string id)
        {
            lock(carts)
            {
                AssertCartExists(id);

                return carts[id];
            }
        }

        public void Delete(string id)
        {
            lock(carts)
            {
                AssertCartExists(id);

                carts.Remove(id);
            }
        }

        public Cart Clear(string id)
        {
            lock(carts)
            {
                AssertCartExists(id);

                carts[id].Items.Clear();

                return carts[id];
            }
        }

        public Item AddItem(string id, string product, int quantity)
        {
            lock(carts)
            {
                AssertCartExists(id);
                AssertProductIsValid(product);
                AssertQuantityIsValid(quantity);

                var cart = carts[id];
                var item = new Item { ItemId = Guid.NewGuid().ToString(), Product = product, Quantity = quantity };

                cart.Items.Add(item);

                return item;
            }
        }

        public Item UpdateItem(string id, string itemId, string product, int quantity)
        {
            lock(carts)
            {
                AssertCartExists(id);
                AssertCartItemExists(id, itemId);
                AssertQuantityIsValid(quantity);

                var cart = carts[id];
                var item = cart.Items.First(x => x.ItemId == itemId);

                item.Product = product;
                item.Quantity = quantity;

                return item;
            }
        }

        public void RemoveItem(string id, string itemId)
        {
            lock(carts)
            {
                AssertCartExists(id);
                AssertCartItemExists(id, itemId);

                carts[id].Items.RemoveAll(x => x.ItemId == itemId);
            }
        }

        private void AssertCartExists(string id)
        {
            if (!carts.ContainsKey(id)) throw new CartNotFoundException();
        }

        private void AssertCartItemExists(string id, string itemId)
        {
            if (!carts[id].Items.Any(x => x.ItemId == itemId)) throw new ItemNotFoundException();
        }

        private void AssertProductIsValid(string product)
        {
            if (string.IsNullOrEmpty(product)) throw new ValidationException();
        }

        private void AssertQuantityIsValid(int quantity)
        {
            if (quantity < 1) throw new ValidationException();
        }
    }
}