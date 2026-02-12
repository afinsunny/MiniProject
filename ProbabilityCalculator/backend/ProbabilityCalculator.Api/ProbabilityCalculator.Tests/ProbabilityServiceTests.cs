using ProbabilityCalculator.Api.Services;
using ProbabilityCalculator.Api.Services.Interface;

namespace ProbabilityCalculator.Tests
{
    public class ProbabilityServiceTests
    {
        private readonly ProbabilityService _service;

        public ProbabilityServiceTests()
        {
            var logger = new TestLogger();
            _service = new ProbabilityService(logger);
        }

        [Fact]
        public void CombinedWith_Returns_Correct_Result()
        {
            var result = _service.Calculate("CombinedWith", "0.5", "0.5");

            Assert.True(result.IsSuccess);
            Assert.Equal(0.25, result.Result, 5);
        }

        [Fact]
        public void Either_Returns_Correct_Result()
        {
            var result = _service.Calculate("Either", "0.5", "0.5");

            Assert.True(result.IsSuccess);
            Assert.Equal(0.75, result.Result, 5);
        }

        [Fact]
        public void Invalid_Number_Returns_Error()
        {
            var result = _service.Calculate("Either", "abc", "0.5");

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void Out_Of_Range_Returns_Error()
        {
            var result = _service.Calculate("CombinedWith", "1.2", "0.5");

            Assert.False(result.IsSuccess);
            Assert.Equal("Probability A must be between 0 and 1.", result.ErrorMessage);
        }

        private sealed class TestLogger : ICalculationLogger
        {
            public void LogSuccess(string line) { }
        }
    }
}