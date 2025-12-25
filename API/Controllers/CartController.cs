using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await cartService.GetCartAsync(id);
            return Ok(cart ?? new ShoppingCart{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updateCart = await cartService.SetCartAsync(cart);

            if(updateCart == null) return BadRequest("Problem with Cart");

            return updateCart;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCartItem(string id)
        {
            var delete = await cartService.DeleteCartAsync(id);

            if(!delete) return BadRequest("Problem with deleting Cart");

            return Ok();
        }
    }
}
