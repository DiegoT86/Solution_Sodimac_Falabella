using Helper;
using Microsoft.Extensions.Configuration;
using System;

namespace DataAccess
{
    public class ConnectionStringHelper
    {
        public static string GetConnectionString(string connectionStringKey)
        {
            IConfigurationRoot configuration = ConfigurationHelper.GetConfiguration();
            string connectionString = configuration.GetConnectionString(connectionStringKey);
            connectionString = string.Format(connectionString, configuration.GetValue("Database-UserId"), configuration.GetValue("Database-Password"));
            return connectionString;
        }
    }
}
