using System.Security.Cryptography.X509Certificates;

namespace OnlineShop.Connection
{
    public class ConnectionDB
    {
        private string _connectionString = string.Empty;
        public ConnectionDB()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _connectionString = builder.GetSection("ConnectionStrings:masterConnection").Value;
        }
        public string ConnectionString()
        {
            return _connectionString;
        }
    }
}
