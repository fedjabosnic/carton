using System;
using Carton.Client;
using Carton.Client.Utilities;
using Carton.Client.V1;
using Carton.Client.V1.Model;

namespace Carton.Client.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using(var service = Carton.Client.V1.Service.Create("http://localhost:5000"))
            {
                var cart = service.CreateCart().Result;

                var item1 = service.AddItemToCart(cart.Data.CartId, "knife", 1).Result;
                var item2 = service.AddItemToCart(cart.Data.CartId, "forks", 2).Result;
                var item3 = service.AddItemToCart(cart.Data.CartId, "spoon", 3).Result;

                service.ClearCart(cart.Data.CartId).Wait();

                var item4 = service.AddItemToCart(cart.Data.CartId, "knife", 7).Result;
                var item5 = service.AddItemToCart(cart.Data.CartId, "forks", 8).Result;
                var item6 = service.AddItemToCart(cart.Data.CartId, "spoon", 9).Result;

                var item7 = service.RemoveItemFromCart(cart.Data.CartId, item6.Data.ItemId).Result;
                var item8 = service.UpdateItemInCart(cart.Data.CartId, item5.Data.ItemId, item5.Data.Product, 10).Result;

                var final = service.GetCart(cart.Data.CartId).Result;

                Console.WriteLine($"Cart: '{final.Data.CartId}' contains ({final.Data.Items.Count} items):");

                foreach(var item in final.Data.Items)
                {
                    Console.WriteLine($"- '{item.ItemId}': {item.Quantity} x {item.Product}");
                }

                Console.ReadLine();
            }
        }
    }
}
