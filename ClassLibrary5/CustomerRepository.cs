using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SmartphoneTechnology.Core
{
    public class CustomerRepository
    {
        private readonly SmartphoneContext context;

        public CustomerRepository()
        {
            context = new SmartphoneContext();
        }

        
        public void Insert(Customer customer)
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = @"
INSERT INTO Customer
(FirstName, LastName, Phone, Email, Address)
VALUES
(@FirstName, @LastName, @Phone, @Email, @Address);
";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Email",
                        string.IsNullOrEmpty(customer.Email)
                            ? (object)DBNull.Value
                            : customer.Email);
                    cmd.Parameters.AddWithValue("@Address",
                        string.IsNullOrEmpty(customer.Address)
                            ? (object)DBNull.Value
                            : customer.Address);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = context.GetConnection())
            {
                string query = "SELECT * FROM Customer";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerId = (int)reader["CustomerId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                        Address = reader["Address"] == DBNull.Value ? "" : reader["Address"].ToString()
                    });
                }
            }

            return customers;
        }

        
        public void Update(Customer customer)
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = @"
UPDATE Customer
SET FirstName = @FirstName,
    LastName = @LastName,
    Phone = @Phone,
    Email = @Email,
    Address = @Address
WHERE CustomerId = @CustomerId;
";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Email",
                    string.IsNullOrEmpty(customer.Email)
                        ? (object)DBNull.Value
                        : customer.Email);
                cmd.Parameters.AddWithValue("@Address",
                    string.IsNullOrEmpty(customer.Address)
                        ? (object)DBNull.Value
                        : customer.Address);
                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public void Delete(int customerId)
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = "DELETE FROM Customer WHERE CustomerId = @CustomerId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
