using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using Carton.Storage;
using Microsoft.AspNetCore.Mvc;
using Carton.Model;
using System.Collections.Generic;

namespace Carton.Test.Controllers.V1.CartsController
{
    [TestClass]
    public class cart_clear
    {
        [TestMethod]
        public void returns_status_code_200_when_the_cart_was_successfully_cleared()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Clear("abc")).Returns(new Cart { CartId = "abc", Created = DateTime.MaxValue, Updated = DateTime.MaxValue, Items = new List<Item>() });

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Clear("abc").Result as ObjectResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(200);

            response.Value.Should().NotBe(null);
            response.Value.Should().BeOfType<Cart>();

            // Just to prove we're returning what the store returned
            response.Value.As<Cart>().CartId.Should().Be("abc");
            response.Value.As<Cart>().Created.Should().Be(DateTime.MaxValue);
            response.Value.As<Cart>().Updated.Should().Be(DateTime.MaxValue);
            response.Value.As<Cart>().Items.Count.Should().Be(0);
        }

        [TestMethod]
        public void returns_status_code_404_when_the_cart_doesnt_exist()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Clear("abc")).Throws<CartNotFoundException>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Clear("abc").Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Clear("abc")).Throws<Exception>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Clear("abc").Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}