using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quizlyApi.Models
{
    public enum Language
    {
        ZH,
        EN
    }

    [Table("quiz_content")]
    public class QuizContent
    {
        [Key]
        [Column("quizId")]
        public int QuizId { get; set; }

        [Key]
        [Column("userId")]
        public int UserId { get; set; }

        [Required]
        [Column("lang")]
        public Language Language { get; set; }

        [Column("rawContent")]
        public string? RawContent { get; set; }

        [Column("postProcessedContent")]
        public string? PostProcessedContent { get; set; }

        [ForeignKey("QuizId")]
        public QuizConfig QuizConfig { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}