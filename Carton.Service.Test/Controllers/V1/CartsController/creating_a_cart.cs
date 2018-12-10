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
    public class creating_a_cart
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
        public void returns_status_code_201_and_the_cart_when_the_cart_was_created_successfully()
        {
            store
                .Setup(x => x.Create())
                .Returns(new Cart
                    {
                        CartId = "abc",
                        Created = DateTime.MaxValue,
                        Updated = DateTime.MaxValue,
                        Items = new List<Item>()
                    });

            var response = controller.Create().Result as CreatedResult;

            response.Should().NotBe(null);
            response.Location.Should().Be("/api/v1/carts/abc");
            response.StatusCode.Should().Be(201);

            response.Value.Should().NotBe(null);
            response.Value.Should().BeOfType<Cart>();

            // Just to prove we're returning what the store returned
            response.Value.As<Cart>().CartId.Should().Be("abc");
            response.Value.As<Cart>().Created.Should().Be(DateTime.MaxValue);
            response.Value.As<Cart>().Updated.Should().Be(DateTime.MaxValue);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            store.Setup(x => x.Create()).Throws<Exception>();

            var response = controller.Create().Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}