using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using Carton.Storage;
using Carton.Model;
using Carton.Model.Exceptions;

namespace Carton.Test.Controllers.V1.CartsController
{
    [TestClass]
    public class removing_an_item_from_a_cart
    {
        private Mock<ICartStore> store;
        private Carton.Controllers.V1.CartsController controller;

        [TestInitialize]
        public void Setup()
        {
            store = new Mock<ICartStore>();
            controller = new Carton.Controllers.V1.CartsController(store.Object);
        }

        [TestMethod]
        public void returns_status_code_200_when_the_item_was_removed_successfully()
        {
            store.Setup(x => x.RemoveItem("abc", "123"));

            var response = controller.RemoveItem("abc", "123") as OkResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public void returns_status_code_404_when_the_cart_does_not_exist()
        {
            store.Setup(x => x.RemoveItem("abc", "123")).Throws<CartNotFoundException>();

            var response = controller.RemoveItem("abc", "123") as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_404_when_the_item_does_not_exist()
        {
            store.Setup(x => x.RemoveItem("abc", "123")).Throws<ItemNotFoundException>();

            var response = controller.RemoveItem("abc", "123") as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            store.Setup(x => x.RemoveItem("abc", "123")).Throws<Exception>();

            var response = controller.RemoveItem("abc", "123") as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}