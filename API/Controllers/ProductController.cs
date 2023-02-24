using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react_store.Data;
using react_store.Entities;
using react_store.Extensions;

namespace react_store.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly ReactStoreContext _context;
      
        public ProductController(ReactStoreContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct(string orderBy, string searchTerm, string brands, string types)
        {
            var query =  _context.Products
                .Sort(orderBy)
                .Search(searchTerm)
                .Filter(brands, types)
                .AsQueryable();

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}