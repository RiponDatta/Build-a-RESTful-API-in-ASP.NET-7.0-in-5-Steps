using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext productDbContext)
        {
            _context = productDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();

            if(products==null)
                return NotFound();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, Product updateProductRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if(product==null)
                return NotFound();

            product.Name = updateProductRequest.Name;
            product.Type = updateProductRequest.Type;
            product.Price = updateProductRequest.Price;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]    
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product==null)   
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet]
        //[Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product==null)
                return NotFound();

            return Ok(product);
        }
    }
}
