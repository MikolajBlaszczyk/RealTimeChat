

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RealTimeChat.API.DataAccess.KeyDataAccess
{
    public class DatabaseClosureManager
    {
        private IConfiguration Config;

        public DatabaseClosureManager(IConfiguration config)
        {
            Config = config;
        }

        public async Task PerformBackup()
        {
            using (SqlConnection databaseConnection = new SqlConnection(Config.GetConnectionString("AppContextConnection")))
            {
                databaseConnection.Open();

                SqlCommand cmd = new SqlCommand("exec sp_CreateBackup");
                await cmd.ExecuteNonQueryAsync();

                databaseConnection.Close();
            }
        }
    }
}
