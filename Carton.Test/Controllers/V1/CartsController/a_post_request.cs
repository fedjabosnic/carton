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
    public class a_post_request
    {
        [TestMethod]
        public void returns_status_code_201_and_the_cart_when_the_cart_was_created_successfully()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Create()).Returns(new Cart { CartId = "abc", Created = DateTime.MaxValue, Updated = DateTime.MaxValue });

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Post().Result as CreatedResult;

            response.Should().NotBe(null);
            response.Location.Should().Be("/api/v1/carts/abc");
            response.StatusCode.Should().Be(201);

            response.Value.Should().NotBe(null);
            response.Value.Should().BeOfType<Cart>();

            response.Value.As<Cart>().CartId.Should().Be("abc");
            response.Value.As<Cart>().Created.Should().Be(DateTime.MaxValue);
            response.Value.As<Cart>().Updated.Should().Be(DateTime.MaxValue);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Create()).Throws<Exception>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Post().Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}