using NLog;

namespace PaySlip.LoggerService
{
    public interface ILoggerService
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }

    public class LoggerService : ILoggerService
    {
        private readonly Logger _logger;

        public LoggerService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void Info(string message)
        {
            _logger.Info(message);
        }
        public void Debug(string message)
        {
            _logger.Debug(message);
        }
        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
