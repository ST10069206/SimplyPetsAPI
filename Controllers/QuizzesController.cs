using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models;
using PetAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        public readonly DataContext _context;

        public QuizzesController(DataContext context)
        {
            _context = context;
        }

        // Get a single quiz
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound();

            return quiz;
        }

        // Get all quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }

        // Add a new quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> AddQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
            // return Ok(await _context.Quizzes.ToListAsync());
        }

        // Update a quiz
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, Quiz quiz)
        {
            var dbQuiz = await _context.Quizzes.FindAsync(id);
            if (dbQuiz == null)
            {
                return NotFound();
            }

            // Update the properties
            dbQuiz.Title = quiz.Title;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Delete a quiz
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound();

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
