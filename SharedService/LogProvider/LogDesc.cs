namespace SharedService.LogProvider
{
    public class LogDesc
    {
        public string Uuid { get; set; } = string.Empty;
        public string? RemoteIpAddress { get; set; } = string.Empty;
        public string? BaseApiUrl { get; set; } = string.Empty;
        public string? IpConnection { get; set; } = string.Empty;
        public string Pid { get; set; } = string.Empty;
        public string? AppName { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string RequestTime { get; set; } = string.Empty;
        public string ResponseTime { get; set; } = string.Empty;
        public string? ThreadName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string Jti { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string IsFromClient { get; set; } = string.Empty;
    }
}