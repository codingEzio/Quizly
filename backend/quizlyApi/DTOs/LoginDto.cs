using System.ComponentModel.DataAnnotations;

namespace quizlyApi.DTOs
{
    public record LoginDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; init; } = default!;

        [Required]
        public string Password { get; init; } = default!;
    }
}