using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Service
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void SetLogError(Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        public void SetLogInfo(string information)
        {
            _logger.LogInformation(information);
        }

        public void SetLogWarning(string information)
        {
            _logger.LogWarning(information);
        }
    }
}
