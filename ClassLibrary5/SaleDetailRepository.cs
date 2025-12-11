using System.Data.SqlClient;

namespace SmartphoneTechnology.Core
{
    public class SaleDetailRepository
    {
        private readonly SmartphoneContext context;

        public SaleDetailRepository()
        {
            context = new SmartphoneContext();
        }

        public void Insert(SaleDetail detail)
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = @"
INSERT INTO SaleDetail
(SaleId, ProductId, Quantity, UnitPrice)
VALUES
(@SaleId, @ProductId, @Quantity, @UnitPrice);
";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@SaleId", detail.SaleId);
                cmd.Parameters.AddWithValue("@ProductId", detail.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
