

using ProbabilityCalculator.Api.Services;

namespace ProbabilityCalculator.Tests
{
    public class FileCalculationLoggerTests
    {
        [Fact]
        public void LogSuccess_Appends_Line_To_File()
        {
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var filePath = Path.Combine(tempDir, "test.log");

            var logger = new FileCalculationLogger(filePath);

            var line = "Test log line";
            logger.LogSuccess(line);

            Assert.True(File.Exists(filePath));

            var content = File.ReadAllText(filePath);
            Assert.Contains(line, content);
        }
    }
}
