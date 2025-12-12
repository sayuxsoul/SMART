using SmartphoneTechnology.Core;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();

            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadProducts();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            // Está solo para que el diseñador no dé error.
            // Ya estamos llamando LoadProducts() en el constructor.
        }

        private void LoadProducts()
        {
            try
            {
                ProductRepository repository = new ProductRepository();
                dgvProducts.DataSource = repository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtSalePrice.Text, out decimal salePrice) ||
                !decimal.TryParse(txtCost.Text, out decimal cost) ||
                !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Price, cost or stock are not valid numbers");
                return;
            }

            Product product = new Product
            {
                Name = txtName.Text.Trim(),
                Brand = txtBrand.Text.Trim(),
                Model = txtModel.Text.Trim(),
                SalePrice = salePrice,
                Cost = cost,
                Stock = stock
            };

            if (!product.ValidateData())
            {
                MessageBox.Show("All product fields are required");
                return;
            }

            try
            {
                ProductRepository repository = new ProductRepository();
                repository.Insert(product);
                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtBrand.Clear();
            txtModel.Clear();
            txtSalePrice.Clear();
            txtCost.Clear();
            txtStock.Clear();
            txtName.Focus();
        }
    }
}
