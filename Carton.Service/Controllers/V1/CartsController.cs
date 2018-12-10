using System;
using Microsoft.AspNetCore.Mvc;
using Carton.Service.Storage;
using Carton.Service.Model;
using Carton.Service.Model.Exceptions;

namespace Carton.Service.Controllers.V1
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
        /// Creates a new cart
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Cart))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Cart> Create()
        {
            try
            {
                var cart = store.Create();

                return Created($"/api/v1/carts/{cart.CartId}", cart);
            }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>
        /// Retrieves a cart
        /// </summary>
        [HttpGet("{cartId}")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Cart> Get(string cartId)
        {
            try
            {
                var cart = store.Get(cartId);

                return Ok(cart);
            }
            catch (CartNotFoundException) { return StatusCode(404); }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>
        /// Deletes a cart
        /// </summary>
        [HttpDelete("{cartId}")]
        [ProducesResponseType(200, Type = typeof(void))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult Delete(string cartId)
        {
            try
            {
                store.Delete(cartId);

                return Ok();
            }
            catch (CartNotFoundException) { return StatusCode(404); }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>
        /// Clears all items from a cart
        /// </summary>
        [HttpDelete("{cartId}/items")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Cart> Clear(string cartId)
        {
            try
            {
                var cart = store.Clear(cartId);

                return Ok(cart);
            }
            catch (CartNotFoundException) { return StatusCode(404); }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>
        /// Adds an item to a cart
        /// </summary>
        [HttpPost("{cartId}/items")]
        [ProducesResponseType(201, Type = typeof(Item))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Item> AddItem(string cartId, [FromBody] Item item)
        {
            try
            {
                item = store.AddItem(cartId, item.Product, item.Quantity);

                return Created($"/api/v1/carts/{cartId}/items/{item.ItemId}", item);
            }
            catch (ValidationException) { return StatusCode(400); }
            catch (CartNotFoundException) { return StatusCode(404); }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>
        /// Updates an item in a cart
        /// </summary>
        [HttpPut("{cartId}/items/{itemId}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(400, Type = typeof(void))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult<Item> UpdateItem(string cartId, string itemId, [FromBody] Item item)
        {
            try
            {
                item = store.UpdateItem(cartId, itemId, item.Product, item.Quantity);

                return Ok(item);
            }
            catch (ValidationException) { return StatusCode(400); }
            catch (CartNotFoundException) { return StatusCode(404); }
            catch (ItemNotFoundException) { return StatusCode(404); }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>
        /// Removes an item from a cart
        /// </summary>
        [HttpDelete("{cartId}/items/{itemId}")]
        [ProducesResponseType(201, Type = typeof(void))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        public ActionResult RemoveItem(string cartId, string itemId)
        {
            try
            {
                store.RemoveItem(cartId, itemId);

                return Ok();
            }
            catch (CartNotFoundException) { return StatusCode(404); }
            catch (ItemNotFoundException) { return StatusCode(404); }
            catch (Exception) { return StatusCode(500); }
        }
    }
}
