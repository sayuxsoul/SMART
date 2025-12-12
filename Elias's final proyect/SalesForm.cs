using SmartphoneTechnology.Core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SalesForm : Form
    {
        private readonly SmartphoneContext context = new SmartphoneContext();

        public SalesForm()
        {
            InitializeComponent();
            context = new SmartphoneContext();
            LoadCustomers();
            LoadProducts();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadCustomers()
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = "SELECT CustomerId, FirstName FROM Customer";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbCustomers.DataSource = dt;
                cbCustomers.DisplayMember = "FirstName";
                cbCustomers.ValueMember = "CustomerId";
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = context.GetConnection())
            {
                string query = "SELECT ProductId, Name, SalePrice FROM Product";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbProducts.DataSource = dt;
                cbProducts.DisplayMember = "Name";
                cbProducts.ValueMember = "ProductId";
            }
        }

        private void cbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProducts.SelectedItem == null) return;

            DataRowView row = (DataRowView)cbProducts.SelectedItem;
            txtPrice.Text = row["SalePrice"].ToString();
            CalculateTotal();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            if (!decimal.TryParse(txtPrice.Text, out decimal price)) return;
            if (!int.TryParse(txtQuantity.Text, out int qty)) return;

            txtTotalAmount.Text = (price * qty).ToString("0.00");
        }

        private void btnSaveSale_Click(object sender, EventArgs e)
        {
            if (cbCustomers.SelectedIndex == -1 ||
                cbProducts.SelectedIndex == -1 ||
                cbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Complete all fields");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) ||
                !decimal.TryParse(txtTotalAmount.Text, out decimal total))
            {
                MessageBox.Show("Invalid quantity or total");
                return;
            }

            using (SqlConnection conn = context.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
INSERT INTO Sale (CustomerId, ProductId, Quantity, TotalAmount, PaymentMethod)
VALUES (@CustomerId, @ProductId, @Quantity, @TotalAmount, @PaymentMethod)", conn);

                cmd.Parameters.AddWithValue("@CustomerId", cbCustomers.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductId", cbProducts.SelectedValue);
                cmd.Parameters.AddWithValue("@Quantity", qty);
                cmd.Parameters.AddWithValue("@TotalAmount", total);
                cmd.Parameters.AddWithValue("@PaymentMethod", cbPaymentMethod.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Sale registered successfully");

            txtPrice.Clear();
            txtQuantity.Clear();
            txtTotalAmount.Clear();
            cbPaymentMethod.SelectedIndex = -1;
        }
    }
}
