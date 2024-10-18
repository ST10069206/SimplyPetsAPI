using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models;
using PetAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        public readonly DataContext _context;

        public QuestionsController(DataContext context)
        {
            _context = context;
        }

        // Get a single question by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound();

            return question;
        }

        // Get all questions for a specific quiz
        [HttpGet("quiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsForQuiz(int quizId)
        {
            var quizExists = await _context.Quizzes.AnyAsync(q => q.Id == quizId);
            if (!quizExists)
                return NotFound("Quiz not found");

            return await _context.Questions
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
        }

        // Add a new question to a quiz
        [HttpPost]
        public async Task<ActionResult<Question>> AddQuestion(Question question)
        {
            var quizExists = await _context.Quizzes.AnyAsync(q => q.Id == question.QuizId);
            if (!quizExists)
                return BadRequest("Quiz not found");

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // Update a question
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, Question updatedQuestion)
        {
            var dbQuestion = await _context.Questions.FindAsync(id);
            if (dbQuestion == null)
            {
                return NotFound();
            }

            // Update properties
            dbQuestion.QuestionText = updatedQuestion.QuestionText;
            dbQuestion.Answer = updatedQuestion.Answer;
            dbQuestion.QuizId = updatedQuestion.QuizId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a question
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
