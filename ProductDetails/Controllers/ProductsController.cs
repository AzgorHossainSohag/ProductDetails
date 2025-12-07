using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDetails.Models;

namespace ProductDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _db;

        public ProductsController(ProductDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetProduct()
        {
            return await _db.Categories.Include(io => io.Product).ToListAsync();
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetProduct(int id)
        {
            if (await _db.Categories.Include(io => io.Product).AnyAsync(op => op.CategoryId == id))
            {
                return await _db.Categories.Include(io => io.Product).SingleAsync(op => op.CategoryId == id);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<Category>> PostProduct(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = category.CategoryId }, category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var delete = await _db.Categories.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(delete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();

            }

            var edit = await _db.Categories.Include(io => io.Product).FirstOrDefaultAsync(op => op.CategoryId == id);

            if (edit == null)
            {
                return NotFound();
            }
            else
            {
                _db.Entry(edit).CurrentValues.SetValues(category);
                _db.Products.RemoveRange(edit.Product);
                edit.Product = category.Product;
                await _db.SaveChangesAsync();
                return Ok(edit);
            }
        }
    }
}
