using Microsoft.Extensions.Logging;

namespace SharedService.LogProvider.Interface
{
    public interface ILoggerService
    {
        void LogInfo(string key, string value, string className, LogLevel logLevel = LogLevel.Information);
        void LogError(string key, string value, string className, LogLevel logLevel = LogLevel.Error);
        void LogObj(string key, object obj, string className, LogLevel logLevel = LogLevel.Information);
        void SetLogDesc(LogDesc logDesc);
        LogDesc GetLogDesc();
    }
}
