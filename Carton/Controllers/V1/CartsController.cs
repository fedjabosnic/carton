using System;
using Microsoft.AspNetCore.Mvc;
using Carton.Storage;
using Carton.Model;

namespace Carton.Controllers.V1
{
    /// <summary>
    /// Restful api for managing carts and items
    /// </summary>
    [ApiController]
    [Route("api/v1/carts")]
    public class CartsController : ControllerBase
    {
        private readonly ICartStore store;

        public CartsController(ICartStore store)
        {
            this.store = store;
        }

        /// <summary>
        /// Creates a new cart.
        /// </summary>
        /// <response code="201">Cart was created</response>
        /// <response code="500">There was an error processing the request</response>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Cart))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Cart> Post()
        {
            try
            {
                var cart = store.Create();

                return Created($"/api/v1/carts/{cart.CartId}", cart);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves a cart
        /// </summary>
        /// <response code="200">Cart was found</response>
        /// <response code="404">The requested cart does not exist</response>
        /// <response code="500">There was an error processing the request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Cart> Get(string id)
        {
            try
            {
                var cart = store.Get(id);

                return Ok(cart);
            }
            catch (CartNotFoundException)
            {
                return StatusCode(404);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates the cart.
        /// </summary>
        /// <response code="200">Cart updated</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Cart> Put(string id, [FromBody] Cart value)
        {
            return StatusCode(500);
        }

        /// <summary>
        /// Deletes a cart.
        /// </summary>
        /// <response code="200">Cart deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult Delete(string id)
        {
            return StatusCode(500);
        }
    }
}
