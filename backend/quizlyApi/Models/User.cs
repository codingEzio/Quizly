using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quizlyApi.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("email")]
        [StringLength(500)]
        public string? Email { get; set; }

        [Required]
        [Column("password")]
        [StringLength(1000)]
        public string Password { get; set; }
    }
}