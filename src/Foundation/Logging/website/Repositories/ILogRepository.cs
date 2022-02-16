namespace Headlixbase.Foundation.Logging.Repositories
{
    public interface ILogRepository
    {
        void Debug(string message);

        void Debug(string message, params object[] args);

        void Error(string message);

        void SingleError(string message);

        void SingleWarn(string message);

        void Info(string message);

        void Info(string message, params object[] args);

        void Warn(string message);

        void Fatal(string message);
    }
}
