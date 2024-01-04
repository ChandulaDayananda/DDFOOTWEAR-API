using DD_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DD_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebOrdersController : ControllerBase
    {
        private readonly AddFootwearContext _dbContext;

        public WebOrdersController(AddFootwearContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebOrders>>> GetWebOrdersAsync()
        {
            try
            {
                var webOrders = await _dbContext.WebOrders.ToListAsync();
                return Ok(webOrders);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebOrders>> GetWebOrdersByIdAsync(int id)
        {
            try
            {
                var webOrder = await _dbContext.WebOrders.FindAsync(id);

                if (webOrder == null)
                {
                    return NotFound();
                }

                return Ok(webOrder);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<WebOrders>> PostWebOrdersAsync(WebOrders webOrders)
        {
            try
            {
                _dbContext.WebOrders.Add(webOrders);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetWebOrdersByIdAsync), new { id = webOrders.W_ID }, webOrders);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebOrdersAsync(int id)
        {
            try
            {
                var webOrder = await _dbContext.WebOrders.FindAsync(id);

                if (webOrder == null)
                {
                    return NotFound();
                }

                _dbContext.WebOrders.Remove(webOrder);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
