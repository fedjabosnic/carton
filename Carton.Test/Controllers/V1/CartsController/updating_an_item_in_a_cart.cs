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
    public class updating_an_item_in_a_cart
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
        public void returns_status_code_200_and_the_item_when_the_item_was_updated_successfully()
        {
            store
                .Setup(x => x.UpdateItem("abc", "123", "apple", 3))
                .Returns(new Item
                    {
                        ItemId = "123",
                        Product = "apple",
                        Quantity = 3
                    });

            var response = controller.UpdateItem("abc", "123", new Item { Product = "apple", Quantity = 3 }).Result as ObjectResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(200);

            response.Value.Should().NotBe(null);
            response.Value.Should().BeOfType<Item>();

            // Just to prove we're returning what the store returned
            response.Value.As<Item>().ItemId.Should().Be("123");
            response.Value.As<Item>().Product.Should().Be("apple");
            response.Value.As<Item>().Quantity.Should().Be(3);
        }

        [TestMethod]
        public void returns_status_code_400_when_validation_on_the_item_fails()
        {
            store.Setup(x => x.UpdateItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Throws<ValidationException>();

            var response = controller.UpdateItem("abc", "123", new Item { Product = "apple", Quantity = 3 }).Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public void returns_status_code_404_when_the_cart_does_not_exist()
        {
            store.Setup(x => x.UpdateItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Throws<CartNotFoundException>();

            var response = controller.UpdateItem("abc", "123", new Item { Product = "apple", Quantity = 3 }).Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public void returns_status_code_500_when_the_cart_store_throws_an_exception()
        {
            store.Setup(x => x.UpdateItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();

            var response = controller.UpdateItem("abc", "123", new Item { Product = "apple", Quantity = 3 }).Result as StatusCodeResult;

            response.Should().NotBe(null);
            response.StatusCode.Should().Be(500);
        }
    }
}