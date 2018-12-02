using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using Carton.Storage;
using Microsoft.AspNetCore.Mvc;
using Carton.Model;

namespace Carton.Test.Controllers.V1.CartsController
{
    [TestClass]
    public class a_get_request
    {
        [TestMethod]
        public void returns_status_code_200_and_the_correct_cart_when_the_cart_exists()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Get("abc")).Returns(new Cart { CartId = "abc", Created = DateTime.MaxValue, Updated = DateTime.MaxValue });

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Get("abc").Result as ObjectResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(200);

            response.Value.Should().NotBe(null);
            response.Value.Should().BeOfType<Cart>();

            response.Value.As<Cart>().CartId.Should().Be("abc");
            response.Value.As<Cart>().Created.Should().Be(DateTime.MaxValue);
            response.Value.As<Cart>().Updated.Should().Be(DateTime.MaxValue);
        }

        [TestMethod]
        public void returns_status_code_404_when_the_cart_does_not_exist()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Get(It.IsAny<string>())).Throws<CartNotFoundException>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Get("abc").Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Get(It.IsAny<string>())).Throws<Exception>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Get("abc").Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}