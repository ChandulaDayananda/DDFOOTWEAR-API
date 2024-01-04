using DD_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DD_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrdersController : ControllerBase
    {
        private readonly AddFootwearContext _dbContext;

        public ProductOrdersController(AddFootwearContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOrders>>> GetProductOrders()
        {


            if (_dbContext.ProductOrders == null)
            {
                return NotFound();
            }
            return await _dbContext.ProductOrders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrders>> GetProductOrders(int id)
        {
            if (_dbContext.ProductOrders == null)
            {
                return NotFound();
            }
            var ProductOrders = await _dbContext.ProductOrders.FindAsync(id);
            if (ProductOrders == null)
            {
                return NotFound();
            }
            return ProductOrders;
        }
        [HttpPost]
        public async Task<ActionResult<ProductOrders>> PostProductOrders(ProductOrders productorders)
        {
            _dbContext.ProductOrders.Add(productorders);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductOrders), new { id = productorders.O_ID }, productorders);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductOrder(int id)
        {
            var productOrder = await _dbContext.ProductOrders.FindAsync(id);

            if (productOrder == null)
            {
                return NotFound();
            }

            _dbContext.ProductOrders.Remove(productOrder);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


    }
}
