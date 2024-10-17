namespace quizlyApi.DTOs
{
    public record UserDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Email { get; init; }
    }
}