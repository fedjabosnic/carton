using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using Carton.Service.Storage;
using Carton.Service.Model;
using Carton.Service.Model.Exceptions;

namespace Carton.Service.Test.Controllers.V1.CartsController
{
    [TestClass]
    public class clearing_a_cart
    {
        private Mock<ICartStore> store;
        private Carton.Service.Controllers.V1.CartsController controller;

        [TestInitialize]
        public void Setup()
        {
            store = new Mock<ICartStore>();
            controller = new Carton.Service.Controllers.V1.CartsController(store.Object);
        }

        [TestMethod]
        public void returns_status_code_200_when_the_cart_was_successfully_cleared()
        {
            store
                .Setup(x => x.Clear("abc"))
                .Returns(new Cart
                    {
                        CartId = "abc",
                        Created = DateTime.MaxValue,
                        Updated = DateTime.MaxValue,
                        Items = new List<Item>()
                    });

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
            store.Setup(x => x.Clear("abc")).Throws<CartNotFoundException>();

            var response = controller.Clear("abc").Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            store.Setup(x => x.Clear("abc")).Throws<Exception>();

            var response = controller.Clear("abc").Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}