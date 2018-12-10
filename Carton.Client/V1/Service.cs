using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Carton.Client.Utilities;
using Carton.Client.V1.Model;

[assembly:InternalsVisibleTo("Carton.Client.Test")]

namespace Carton.Client.V1
{
    public class Service : IDisposable
    {
        private readonly IHttp http;
        private readonly string domain;

        public static Service Create(string domain)
        {
            return new Service(new Http(), domain);
        }

        internal Service(IHttp http, string domain)
        {
            this.http = http;
            this.domain = domain;
        }

        public async Task<Response<Cart>> CreateCart()
        {
            var address = $"{domain}/api/v1/carts";

            return await http.Post<object, Cart>(address, null);
        }

        public async Task<Response<Cart>> GetCart(string cartId)
        {
            var address = $"{domain}/api/v1/carts/{cartId}";

            return await http.Get<Cart>(address);
        }

        public async Task<Response<Cart>> DeleteCart(string cartId)
        {
            var address = $"{domain}/api/v1/carts/{cartId}";

            return await http.Delete<Cart>(address);
        }

        public async Task<Response<Cart>> ClearCart(string cartId)
        {
            var address = $"{domain}/api/v1/carts/{cartId}/items";

            return await http.Delete<Cart>(address);
        }

        public async Task<Response<Item>> AddItemToCart(string cartId, string product, int quantity)
        {
            var address = $"{domain}/api/v1/carts/{cartId}/items";
            var content = new Item { Product = product, Quantity = quantity };

            return await http.Post<Item, Item>(address, content);
        }

        public async Task<Response<Item>> UpdateItemInCart(string cartId, string itemId, string product, int quantity)
        {
            var address = $"{domain}/api/v1/carts/{cartId}/items/{itemId}";
            var content = new Item { ItemId = itemId, Product = product, Quantity = quantity };

            return await http.Put<Item, Item>(address, content);
        }

        public async Task<Response<Item>> RemoveItemFromCart(string cartId, string itemId)
        {
            var address = $"{domain}/api/v1/carts/{cartId}/items/{itemId}";

            return await http.Delete<Item>(address);
        }

        public void Dispose()
        {
            http?.Dispose();
        }
    }
}
