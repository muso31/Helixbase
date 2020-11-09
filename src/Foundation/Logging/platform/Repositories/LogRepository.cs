using Sitecore.Diagnostics;

namespace Helixbase.Foundation.Logging.Platform.Repositories
{
    public class LogRepository: ILogRepository
    {
        public void Debug(string message) => Log.Debug(message, this);

        public void Debug(string message, params object[] args) => Log.Debug(string.Format(message, args));

        public void Error(string message) => Log.Error(message, this);

        public void SingleError(string message) => Log.SingleError(message, this);

        public void SingleWarn(string message) => Log.SingleWarn(message, this);

        public void Info(string message) => Log.Info(message, this);

        public void Info(string message, params object[] args) => Log.Debug(string.Format(message, args));

        public void Warn(string message) => Log.Warn(message, this);

        public void Fatal(string message) => Log.Fatal(message, this);
    }
}
