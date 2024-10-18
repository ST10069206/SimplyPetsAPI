using Microsoft.EntityFrameworkCore;
using PetAPI.Models;

namespace PetAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<PetResource> PetResources { get; set; }
        public DbSet<Question> Questions { get; set; }

    }

        

}
