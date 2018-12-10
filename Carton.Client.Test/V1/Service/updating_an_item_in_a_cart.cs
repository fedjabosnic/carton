using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Carton.Client.Utilities;
using Carton.Client.V1.Model;
using System.Threading.Tasks;

namespace Carton.Client.Test.V1.Service
{
    [TestClass]
    public class updating_an_item_in_a_cart
    {
        private Mock<IHttp> http;
        private Carton.Client.V1.Service service;

        [TestInitialize]
        public void Setup()
        {
            http = new Mock<IHttp>();
            service = new Carton.Client.V1.Service(http.Object, "carton-api");
        }

        [TestMethod]
        public void sends_the_http_request()
        {
            var response = service.UpdateItemInCart("abc", "def", "book", 5).Result;

            http.Verify(x => x.Put<Item, Item>("carton-api/api/v1/carts/abc/items/def", It.Is<Item>(i => i.Product == "book" && i.Quantity == 5)), Times.Once());
        }

        [TestMethod]
        public void returns_the_http_response()
        {
            // Note: There is not need to test every response type since we just return what is returned by the http abstraction
            
            http
                .Setup(x => x.Put<Item, Item>("carton-api/api/v1/carts/abc/items/def", It.IsAny<Item>()))
                .Returns(Task.FromResult(new Response<Item>{ StatusCode = 200, Data = new Item { ItemId = "def", Product = "book", Quantity = 5 } }));

            var response = service.UpdateItemInCart("abc", "def", "book", 3).Result;

            response.StatusCode.Should().Be(200);
            response.Data.ItemId.Should().Be("def");
            response.Data.Product.Should().Be("book");
            response.Data.Quantity.Should().Be(5);
        }
    }
}