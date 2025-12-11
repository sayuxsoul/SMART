using System;
using System.Data.SqlClient;

namespace SmartphoneTechnology.Core
{
    public class TestConnection
    {
        public static string Test()
        {
            try
            {
                SmartphoneContext context = new SmartphoneContext();

                using (SqlConnection conn = context.GetConnection())
                {
                    conn.Open();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
