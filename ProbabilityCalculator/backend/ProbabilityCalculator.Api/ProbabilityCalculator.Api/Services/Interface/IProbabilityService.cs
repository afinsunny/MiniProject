namespace ProbabilityCalculator.Api.Services.Interface
{
    public interface IProbabilityService
    {
        CalculationResult Calculate(string type, string aInput, string bInput);
    }
}
