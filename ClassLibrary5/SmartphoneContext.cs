using System.Configuration;
using System.Data.SqlClient;

namespace SmartphoneTechnology.Core
{
    public class SmartphoneContext
    {
        private readonly string connectionString;

        public SmartphoneContext()
        {
            connectionString = ConfigurationManager
                .ConnectionStrings["SmartphoneDB"]
                .ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
