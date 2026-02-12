using ProbabilityCalculator.Api.Services.Interface;
using System.Globalization;

namespace ProbabilityCalculator.Api.Services
{
    public sealed class ProbabilityService : IProbabilityService
    {
        private readonly ICalculationLogger _logger;

        public ProbabilityService(ICalculationLogger logger)
        {
            _logger = logger;
        }

        public CalculationResult Calculate(string type, string aInput, string bInput)
        {
            if (string.IsNullOrWhiteSpace(type))
                return new CalculationResult { IsSuccess = false, ErrorMessage = "Calculation type is required." };

            if (!double.TryParse(aInput, NumberStyles.Float, CultureInfo.InvariantCulture, out var a))
                return new CalculationResult { IsSuccess = false, ErrorMessage = "Probability A must be a valid number." };

            if (!double.TryParse(bInput, NumberStyles.Float, CultureInfo.InvariantCulture, out var b))
                return new CalculationResult { IsSuccess = false, ErrorMessage = "Probability B must be a valid number." };

            if (a < 0 || a > 1)
                return new CalculationResult { IsSuccess = false, ErrorMessage = "Probability A must be between 0 and 1." };

            if (b < 0 || b > 1)
                return new CalculationResult { IsSuccess = false, ErrorMessage = "Probability B must be between 0 and 1." };

            if (!Enum.TryParse<Models.CalculationType>(type, ignoreCase: true, out var calcType))
                return new CalculationResult { IsSuccess = false, ErrorMessage = "Calculation type must be CombinedWith or Either." };

            double result = calcType switch
            {
                Models.CalculationType.CombinedWith => a * b,
                Models.CalculationType.Either => a + b - (a * b),
                _ => double.NaN
            };

            var logLine = $"{DateTime.UtcNow:O}\t{calcType}\tA={a.ToString(CultureInfo.InvariantCulture)}\tB={b.ToString(CultureInfo.InvariantCulture)}\tResult={result.ToString(CultureInfo.InvariantCulture)}";
            _logger.LogSuccess(logLine);

            return new CalculationResult { IsSuccess = true, Result = result };
        }
    }
}
