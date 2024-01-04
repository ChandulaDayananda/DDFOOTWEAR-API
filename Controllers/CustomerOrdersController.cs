using DD_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DD_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly AddFootwearContext _dbContext;

        public CustomerOrdersController(AddFootwearContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerOrders>>> GetCustomerOrders()
        {


            if (_dbContext.CustomerOrders == null)
            {
                return NotFound();
            }
            return await _dbContext.CustomerOrders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrders>> GetCustomerOrders(int id)
        {
            if (_dbContext.CustomerOrders == null)
            {
                return NotFound();
            }
            var CustomerOrders = await _dbContext.CustomerOrders.FindAsync(id);
            if (CustomerOrders == null)
            {
                return NotFound();
            }
            return CustomerOrders;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerOrders>> PostCustomerOrders(CustomerOrders customerorders)
        {
            _dbContext.CustomerOrders.Add(customerorders);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerOrders), new { id = customerorders.C_ID }, customerorders);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrders(int id)
        {
            var customerOrders = await _dbContext.CustomerOrders.FindAsync(id);

            if (customerOrders == null)
            {
                return NotFound();
            }

            _dbContext.CustomerOrders.Remove(customerOrders);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
