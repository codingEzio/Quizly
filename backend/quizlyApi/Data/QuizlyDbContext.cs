using Microsoft.EntityFrameworkCore;
using quizlyApi.Models;

namespace quizlyApi.Data
{
    public class QuizlyDbContext : DbContext
    {
        public QuizlyDbContext()
        {
        }

        public QuizlyDbContext(DbContextOptions<QuizlyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<QuizConfig> QuizConfigs { get; set; }
        public DbSet<QuizContent> QuizContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<QuizConfig>().HasKey(qc => qc.Id);
            modelBuilder.Entity<QuizContent>().HasKey(qc => new { qc.QuizId, qc.UserId });

            modelBuilder.Entity<QuizContent>()
                .HasOne(qc => qc.QuizConfig)
                .WithMany()
                .HasForeignKey(qc => qc.QuizId);

            modelBuilder.Entity<QuizContent>()
                .HasOne(qc => qc.User)
                .WithMany()
                .HasForeignKey(qc => qc.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConnectionString = "Server=localhost;Port=3307;Database=quizly;User=root;Password = root; ";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
            }
        }
    }
}
