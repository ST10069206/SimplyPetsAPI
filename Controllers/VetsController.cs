using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models;
using PetAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetsController : ControllerBase
    {
        public readonly DataContext _context;

        public VetsController(DataContext context)
        {
            _context = context;
        }


        // Get a single vet
        [HttpGet("{id}")]
        public async Task<ActionResult<Vet>> GetVet(int id)
        {
            var vet = await _context.Vets.FindAsync(id);
            if (vet == null)
                return NotFound();

            return vet;
        }

        // Get all vets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vet>>> GetVets()
        {
            return await _context.Vets.ToListAsync();
        }

        // Add a new vet
        [HttpPost]
        public async Task<ActionResult<Vet>> PostVet(Vet vet)
        {
            _context.Vets.Add(vet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVet", new { id = vet.Id }, vet);
            //return Ok(await _context.Vets.ToListAsync());
        }

        //// Update a vet
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateVet(int id, Vet vet)
        //{
        //    var dbVet = await _context.Vets.FindAsync(id); // dbVet is the vet in the database
        //    if (dbVet == await _context.Vets.FindAsync(id)) // if the vet in the database is the same as the vet to update
        //    {
        //        dbVet.Name = vet.Name;
        //        dbVet.Location = vet.Location;
        //        dbVet.ContactInfo = vet.ContactInfo;
        //        dbVet.Rating = vet.Rating;
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        // Update a vet
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVet(int id, Vet vet)
        {
            // Check if the vet with the given id exists
            var dbVet = await _context.Vets.FindAsync(id);
            if (dbVet == null) // Change this condition to check for null directly
            {
                return NotFound(); // Return NotFound if the vet does not exist
            }

            // Update the vet's properties
            dbVet.Name = vet.Name;
            dbVet.Location = vet.Location;
            dbVet.ContactInfo = vet.ContactInfo;
            dbVet.Rating = vet.Rating;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent(); // Return NoContent to indicate the update was successful
        }


        // Delete a vet
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVet(int id)
        {
            var vet = await _context.Vets.FindAsync(id);
            if (vet == null)
                return NotFound();

            _context.Vets.Remove(vet);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
