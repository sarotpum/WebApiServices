using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedService.LogProvider.Interface;
using System.Text.RegularExpressions;
using System.Xml;

namespace SharedService.LogProvider.Implement
{
    public class LoggerServiceImpl : ILoggerService
    {
        private readonly ILogger<LoggerServiceImpl> _logger;
        private LogDesc _logDesc = null;

        public LoggerServiceImpl(ILogger<LoggerServiceImpl> logger)
        {
            _logger = logger;
        }

        private void LogData(LogLevel logLevel, string key = "", string value = "", string className = "")
        {
            var logMessage = "";
            if (_logDesc == null)
            {
                logMessage = $" KEY: [ {key.Trim()} ] VALUE: [ {value.Trim()} ] CLASS: [{className.Trim()}] ";
                _logger.LogInformation(logMessage);
            }
            else
            {
                if (Thread.CurrentThread.Name == null)
                {
                    Thread.CurrentThread.Name = "MainThread";
                }

                logMessage = (
                    $@" {logLevel} APPNAME: {_logDesc.AppName} IPCON: {_logDesc.IpConnection} HOST: {_logDesc.Host} THREAD: {Thread.CurrentThread.Name} CLASS: {className} REIP: {_logDesc.RemoteIpAddress} BASEURI: {_logDesc.BaseApiUrl} UUID: {_logDesc.Uuid} METH: {_logDesc.Method} PATH: {_logDesc.Path} KEY: {key.Trim()} VALUE: {value.Trim()} ");
                _logger.LogInformation(logMessage);
            }
        }

        public void LogInfo(string key, string value, string className, LogLevel logLevel = LogLevel.Information)
        {
            value = string.IsNullOrEmpty(value) ? value : value.Replace(Environment.NewLine, " ");
            LogData(logLevel, key, value, className);
        }

        public void LogError(string key, string value, string className, LogLevel logLevel = LogLevel.Error)
        {
            LogData(logLevel, key, value, className);
        }

        public void LogObj(string key, object obj, string className, LogLevel logLevel = LogLevel.Information)
        {
            var jsonContent = JsonConvert.SerializeObject(obj).Replace(Environment.NewLine, " ");
            jsonContent = Regex.Replace(jsonContent, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
            LogInfo(key, jsonContent, className, logLevel);
        }

        public void SetLogDesc(LogDesc logDesc)
        {
            _logDesc = logDesc;
        }

        public LogDesc GetLogDesc()
        {
            return _logDesc;
        }
    }
}
