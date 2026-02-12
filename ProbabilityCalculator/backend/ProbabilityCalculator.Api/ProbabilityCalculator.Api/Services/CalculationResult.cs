namespace ProbabilityCalculator.Api.Services
{
    public class CalculationResult
    {
        public bool IsSuccess { get; init; }
        public double Result { get; init; }
        public string? ErrorMessage { get; init; }
    }
}
