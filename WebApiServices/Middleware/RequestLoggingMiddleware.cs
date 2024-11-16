using SharedService.LogProvider;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Http.Features;

namespace WebApiServices.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly IHttpContextAccessor _accessor;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, IHttpContextAccessor accessor)
        {
            _next = next;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;

            if (request.Method == "POST" || request.Method == "GET")
            {
                //Console.WriteLine(httpContext.Request.Headers["Referer"]);
                var isFromSwagger = httpContext.Request.Headers["Referer"].ToString().Contains("swagger/index.html");

                var requestTime = DateTime.Now.ToLocalTime();
                if (Thread.CurrentThread.Name == null)
                {
                    Thread.CurrentThread.Name = "MainThread";
                }

                var logDesc = new LogDesc
                {
                    UserId = Guid.NewGuid().ToString("N").ToUpper(),
                    RemoteIpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    Host = httpContext.Request.Host.Value,
                    Method = httpContext.Request.Method,
                    Path = httpContext.Request.Path,
                    AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                    RequestTime = DateTime.Now.ToLocalTime().ToString("O"),
                    IpConnection = httpContext.Request.HttpContext?.Features?.Get<IHttpConnectionFeature>()
                        ?.RemoteIpAddress
                        ?.ToString(),
                    BaseApiUrl = httpContext.Request.Headers.Where(w => w.Key.Equals("Origin")).Select(s => s.Value)
                        .FirstOrDefault(),
                    ThreadName = Thread.CurrentThread.Name,
                    IsFromClient = httpContext.Request.Headers.Any(w => w.Key.Equals("User-Agent")) ? "[CLIENT]" : ""
                };
                httpContext.Items.Add("logDesc", logDesc);

                try
                {
                    await ReadRequestBody(request, logDesc);
                    var originalBodyStream = httpContext.Response.Body;
                    using (var responseBody = new MemoryStream())
                    {
                        httpContext.Response.Body = responseBody;

                        await _next(httpContext);

                        await ReadResponseBody(httpContext.Response, logDesc);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }

                    var responseTime = DateTime.Now.ToLocalTime() - requestTime;
                    var logMessage = (
                        $@" {LogLevel.Information} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} RESPONSE TIME(s): {responseTime.TotalSeconds} "
                    );
                    _logger.LogInformation(logMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error");
                    await _next(httpContext);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request, LogDesc logDesc)
        {
            try
            {
                if (request.Body.CanRead)
                {
                    var logMessage = (
                        $@" {LogLevel.Information} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} Enabled Rewind "
                    );
                    _logger.LogInformation(logMessage);
                    request.EnableBuffering();
                     
                    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                    await request.Body.ReadAsync(buffer, 0, buffer.Length);
                    logMessage = (
                        $@" {LogLevel.Information} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} ReadAsync "
                    );
                    _logger.LogInformation(logMessage);
                    string bodyAsText;
                    bodyAsText = Encoding.UTF8.GetString(buffer);
                    request.Body.Seek(0, SeekOrigin.Begin);
                    logMessage = (
                        $@" {LogLevel.Information} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} ReadComplete "
                    );
                    _logger.LogInformation(logMessage);
                    if (request.Path.Value.ToLower().Contains("ldap") && request.Path.Value.ToLower().Contains("login"))
                    {
                        bodyAsText = "";
                    }
                    else
                    {
                        bodyAsText = bodyAsText.Replace(Environment.NewLine, " ");
                        bodyAsText = Regex.Replace(bodyAsText, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                    }

                    logMessage = (
                        $@" {LogLevel.Information} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} REQUEST BODY: {bodyAsText}"
                    );

                    //Console.WriteLine(logMessage);
                    _logger.LogInformation(logMessage);
                    return bodyAsText;
                }
            }
            catch (Exception e)
            {
                var errMessage = (
                    $@" {LogLevel.Error} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} ERROR READ REQUEST BODY " +
                    e.ToString());
                //Console.WriteLine(errMessage);
                _logger.LogInformation(errMessage);
            }

            return null;
        }


        private async Task ReadResponseBody(HttpResponse response, LogDesc logDesc)
        {
            if (response.Body.CanRead)
            {
                try
                {
                    var bodyAsText = response.ContentType;
                    logDesc.ResponseTime = DateTime.Now.ToLocalTime().ToString("O");
                    response.Body.Seek(0, SeekOrigin.Begin);
                    bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
                    response.Body.Seek(0, SeekOrigin.Begin);

                    if ((!string.IsNullOrEmpty(response.ContentType) && !response.ContentType.Contains("json")) ||
                        logDesc.Path.Contains("ldap"))
                    {
                        bodyAsText = response.ContentType;
                    }
                    else
                    {
                        bodyAsText = bodyAsText.Replace(Environment.NewLine, " ");
                        bodyAsText = Regex.Replace(bodyAsText, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                    }

                    var logMessage = (
                        $@" {LogLevel.Information} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} RESPONSE BODY: {bodyAsText}"
                    );
                    //Console.WriteLine(logMessage);
                    _logger.LogInformation(logMessage);
                }
                catch (Exception e)
                {
                    var errMessage = (
                        $@" {LogLevel.Error} APPNAME: {logDesc.AppName} IPCON: {logDesc.IpConnection} HOST: {logDesc.Host} THREAD: {logDesc.ThreadName} CLASS: {typeof(RequestLoggingMiddleware).Name} REIP: {logDesc.RemoteIpAddress} BASEURI: {logDesc.BaseApiUrl} UUID: {logDesc.Uuid} METH: {logDesc.Method} PATH: {logDesc.Path} {logDesc.IsFromClient} ERROR READ REQUEST BODY: " +
                        e.ToString());
                    //Console.WriteLine(errMessage);
                    _logger.LogInformation(errMessage);
                }
            }
        }
    }
}
