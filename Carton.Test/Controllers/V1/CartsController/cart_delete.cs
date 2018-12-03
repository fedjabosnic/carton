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
    public class cart_delete
    {
        [TestMethod]
        public void returns_status_code_200_when_the_cart_was_successfully_deleted()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Delete("abc"));

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Delete("abc") as OkResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public void returns_status_code_404_when_the_cart_doesnt_exist()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Delete("abc")).Throws<CartNotFoundException>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Delete("abc") as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            var store = new Mock<ICartStore>();

            store.Setup(x => x.Delete("abc")).Throws<Exception>();

            var controller = new Carton.Controllers.V1.CartsController(store.Object);

            var response = controller.Delete("abc") as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}