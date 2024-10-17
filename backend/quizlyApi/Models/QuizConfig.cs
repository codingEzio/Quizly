using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quizlyApi.Models
{
    public enum Difficulty
    {
        MostEasy,
        Easy,
        Medium,
        Hard,
        MostHard
    }

    [Table("quiz_config")]
    public class QuizConfig
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [Column("context")]
        public string Context { get; set; }

        [Required]
        [Column("difficulty")]
        public Difficulty Difficulty { get; set; }
    }
}