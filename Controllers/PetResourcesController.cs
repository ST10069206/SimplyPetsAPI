using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models;
using PetAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetResourcesController : ControllerBase
    {
        public readonly DataContext _context;

        public PetResourcesController(DataContext context)
        {
            _context = context;
        }

        // Get a single pet resource
        [HttpGet("{id}")]
        public async Task<ActionResult<PetResource>> GetPetResource(int id)
        {
            var petResource = await _context.PetResources.FindAsync(id);
            if (petResource == null)
                return NotFound();

            return petResource;
        }

        // Get all pet resources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetResource>>> GetPetResources()
        {
            return await _context.PetResources.ToListAsync();
        }

        // Add a new pet resource
        [HttpPost]
        public async Task<ActionResult<PetResource>> AddPetResource(PetResource petResource)
        {
            _context.PetResources.Add(petResource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPetResource", new { id = petResource.Id }, petResource);
            // return Ok(await _context.PetResources.ToListAsync());
        }

        // Update a pet resource
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePetResource(int id, PetResource petResource)
        {
            var dbPetResource = await _context.PetResources.FindAsync(id);
            if (dbPetResource == await _context.PetResources.FindAsync(id))
            {
                dbPetResource.Title = petResource.Title;
                dbPetResource.Content = petResource.Content;
                dbPetResource.DatePosted = petResource.DatePosted;
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete a pet resource
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetResource(int id)
        {
            var petResource = await _context.PetResources.FindAsync(id);
            if (petResource == null)
                return NotFound();

            _context.PetResources.Remove(petResource);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
