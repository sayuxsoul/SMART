using System.Data;
using System.Data.SqlClient;

namespace SmartphoneTechnology.Core
{
    public class ProductRepository
    {
        private readonly SmartphoneContext context;

        public ProductRepository()
        {
            context = new SmartphoneContext();
        }

        public void Insert(Product product)
        {
            using (SqlConnection conn = context.GetConnection())
            {
                conn.Open();
                string query = @"
INSERT INTO Product (Name, Brand, Model, SalePrice, Cost, Stock)
VALUES (@Name, @Brand, @Model, @SalePrice, @Cost, @Stock)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Brand", product.Brand);
                cmd.Parameters.AddWithValue("@Model", product.Model);
                cmd.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                cmd.Parameters.AddWithValue("@Cost", product.Cost);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAll()
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = "SELECT * FROM Product";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
