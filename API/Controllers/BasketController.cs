using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react_store.Data;
using react_store.Entities;

namespace react_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : BaseApiController
    {
        private readonly ReactStoreContext _context;
        public BasketController(ReactStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasket()
        {
            var basket = await RetrieveBasket();
            
            if (basket == null)
            {
                return NotFound();
            }
            return basket;
        }

        [HttpPost]

        public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
        {
            // Get basket method
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            
            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync();
            
            if (result > 0)
            {
                return StatusCode(201);
            }
            return BadRequest(new ProblemDetails { Title = "Problem Saving item to basket"});
        }

        [HttpDelete]

        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // get basket
           

            // remove item or reduce quantity
            // save changes 
            return Ok();
        }

        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(30)
            };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);

            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);

        }
    }
}