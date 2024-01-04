using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DD_System.Models;
using Microsoft.EntityFrameworkCore;

namespace DD_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootwearController : ControllerBase
    {
        private readonly AddFootwearContext _dbContext;

        public FootwearController(AddFootwearContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Footwear>>> GetFootwear()
        {
            if (_dbContext.Footwear == null)
            {
                return NotFound();
            }
            return await _dbContext.Footwear.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Footwear>> GetFootwear(int id)
        {
            if (_dbContext.Footwear == null)
            {
                return NotFound();
            }
            var Footwear = await _dbContext.Footwear.FindAsync(id);
            if (Footwear == null)
            {
                return NotFound();
            }
            return Footwear;
        }

        [HttpPost]
        public async Task<ActionResult<Footwear>> PostFootwear(Footwear footwear)
        {
            _dbContext.Footwear.Add(footwear);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFootwear), new { id = footwear.P_ID }, footwear);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFootwear(int id, [FromBody] Footwear updatedFootwear)
        {
            if (id != updatedFootwear.P_ID)
            {
                return BadRequest("Mismatched ID in the request and the entity.");
            }

            var existingFootwear = await _dbContext.Footwear.FindAsync(id);

            if (existingFootwear == null)
            {
                return NotFound($"Footwear with ID {id} not found.");
            }

            // Validate Quantity (Example: Ensure it's a non-negative integer)
            if (updatedFootwear.Quantity < 0)
            {
                return BadRequest("Quantity must be a non-negative integer.");
            }

            // Update only the specific properties you want to allow to be updated
            existingFootwear.ProductName = updatedFootwear.ProductName;
            existingFootwear.Category = updatedFootwear.Category;
            existingFootwear.Gender = updatedFootwear.Gender;
            existingFootwear.Size = updatedFootwear.Size;
            existingFootwear.Color = updatedFootwear.Color;
            existingFootwear.Quantity = updatedFootwear.Quantity;
            existingFootwear.Price = updatedFootwear.Price;

            // Check if a new image is provided; if yes, update the image property
            if (!string.IsNullOrEmpty(updatedFootwear.Image))
            {
                existingFootwear.Image = updatedFootwear.Image;
            }

            existingFootwear.IsActive = updatedFootwear.IsActive;

            try
            {
                await _dbContext.SaveChangesAsync();

                // Return an informative response
                return Ok(existingFootwear); // You can return the updated entity or a success message
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FootwearAvailable(id))
                {
                    return NotFound($"Footwear with ID {id} not found during concurrency check.");
                }
                else
                {
                    throw; // Consider handling the exception in a more specific way if needed
                }
            }
        }




        private bool FootwearAvailable(int id)
        {
            return (_dbContext.Footwear?.Any(x => x.P_ID == id)).GetValueOrDefault();
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteFootwear(int id)
        {
            if (_dbContext.Footwear == null)
            {
                return NotFound();
            }

            var footwear = await _dbContext.Footwear.FindAsync(id);
            if (footwear == null)
            {
                return NotFound();
            }

            _dbContext.Footwear.Remove(footwear);
            await _dbContext.SaveChangesAsync();

            return Ok();

        }



    }
}
