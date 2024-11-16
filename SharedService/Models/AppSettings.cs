namespace SharedService.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            databaseSetting = new DatabaseSetting();
        }

        public DatabaseSetting databaseSetting { get; set; }
    }

    public class DatabaseSetting
    {
        public string ServerName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string ConnectionString => $"Server={ServerName};Database={DatabaseName};TrustServerCertificate=True;User ID={UserID};Password={Password};";
    }
}
