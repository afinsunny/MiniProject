using ProbabilityCalculator.Api.Services.Interface;
using System.Text;

namespace ProbabilityCalculator.Api.Services
{
    public sealed class FileCalculationLogger : ICalculationLogger
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        private readonly string _filePath;

        public FileCalculationLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void LogSuccess(string line)
        {
            WriteLineAsync(line).GetAwaiter().GetResult();
        }

        private async Task WriteLineAsync(string line)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrWhiteSpace(directory))
                Directory.CreateDirectory(directory);

            await Semaphore.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(_filePath, line + Environment.NewLine, Encoding.UTF8);
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}
