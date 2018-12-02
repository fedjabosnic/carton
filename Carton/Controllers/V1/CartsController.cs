using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Carton.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartsController : ControllerBase
    {
        /// <summary>
        /// Creates a new cart and returns the identifier.
        /// </summary>
        /// <response code="200">Cart created</response>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a cart.
        /// </summary>
        /// <response code="200">Cart returned</response>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the cart.
        /// </summary>
        /// <response code="200">Cart updated</response>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a cart.
        /// </summary>
        /// <response code="200">Cart deleted</response>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
