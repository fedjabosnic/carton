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
    public class deleting_a_cart
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
            var response = service.DeleteCart("abc").Result;

            http.Verify(x => x.Delete<Cart>("carton-api/api/v1/carts/abc"), Times.Once());
        }

        [TestMethod]
        public void returns_the_http_response()
        {
            // Note: There is not need to test every response type since we just return what is returned by the http abstraction
            
            http
                .Setup(x => x.Delete<Cart>("carton-api/api/v1/carts/abc"))
                .Returns(Task.FromResult(new Response<Cart>{ StatusCode = 200, Data = null }));

            var response = service.DeleteCart("abc").Result;

            response.StatusCode.Should().Be(200);
            response.Data.Should().BeNull();
        }
    }
}