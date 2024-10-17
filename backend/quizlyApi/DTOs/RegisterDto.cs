using System.ComponentModel.DataAnnotations;

namespace quizlyApi.DTOs
{
    public record RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; init; } = default!;

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; init; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; init; } = default!;
    }
}