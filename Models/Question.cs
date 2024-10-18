namespace PetAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Answer { get; set; }
        public int QuizId { get; set; } // Foreign key
        //public Quiz Quiz { get; set; } // Navigation property
    }
}
