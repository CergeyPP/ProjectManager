using Npgsql;
using System.Configuration;

namespace ProjectManager.Core.Modules.Database
{
    public class DatabaseProvider
    {
        private string _connectionString;
        private NpgsqlDataSource _dataSource;
        public DatabaseProvider()
        {
            string address = ConfigurationManager.AppSettings["DbConnectionUrl"];
            string port = ConfigurationManager.AppSettings["DbConnectionPort"];
            string username = ConfigurationManager.AppSettings["DbConnectionUsername"];
            string password = ConfigurationManager.AppSettings["DbConnectionPassword"];
            string databaseName = ConfigurationManager.AppSettings["DbConnectionDatabase"];

            _connectionString = $"Host={address}:{port};Username={username};Password={password};Database={databaseName}";
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);

            _dataSource = dataSourceBuilder.Build();
        }

        public NpgsqlConnection CreateConnection()
        {
            var connection =  _dataSource.CreateConnection();
            connection.Open();
            return connection;
        }
    }
}
