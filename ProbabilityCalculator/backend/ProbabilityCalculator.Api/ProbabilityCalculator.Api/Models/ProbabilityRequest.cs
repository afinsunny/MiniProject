using System.ComponentModel.DataAnnotations;

namespace ProbabilityCalculator.Api.Models
{
    public sealed class ProbabilityRequest
    {
        [Required]
        public string? Type { get; init; }

        [Required]
        public string? A { get; init; }

        [Required]
        public string? B { get; init; }
    }
}
