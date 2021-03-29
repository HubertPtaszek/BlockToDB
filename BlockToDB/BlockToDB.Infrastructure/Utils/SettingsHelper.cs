using System.Configuration;

namespace BlockToDB.Utils
{
    public class SettingsHelper
    {
        #region appSettings

        public static string PolicyExportServiceUserName => ConfigurationManager.AppSettings[nameof(PolicyExportServiceUserName)];
        public static string PolicyExportServicePassword => ConfigurationManager.AppSettings[nameof(PolicyExportServicePassword)];

        public static string ConnectionString(string connectionName = "MainDatabaseContext") => System.Configuration
            .ConfigurationManager.ConnectionStrings[connectionName]
            .ConnectionString;

        #endregion appSettings
    }
}