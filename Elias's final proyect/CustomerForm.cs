using SmartphoneTechnology.Core;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CustomerForm : Form
    {
        private int selectedCustomerId = 0;

        public CustomerForm()
        {
            InitializeComponent();

            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ConfigureGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void ConfigureGrid()
        {
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerId", DataPropertyName = "CustomerId", HeaderText = "Customer Id" });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "FirstName", DataPropertyName = "FirstName", HeaderText = "First Name" });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "LastName", DataPropertyName = "LastName", HeaderText = "Last Name" });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Address", DataPropertyName = "Address", HeaderText = "Address" });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Phone", DataPropertyName = "Phone", HeaderText = "Phone" });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", DataPropertyName = "Email", HeaderText = "Email" });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalesCount", DataPropertyName = "SalesCount", HeaderText = "Sales" });
        }

        private void LoadCustomers()
        {
            CustomerRepository repository = new CustomerRepository();
            dgvCustomers.DataSource = repository.GetAll();
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (!customer.ValidateData())
            {
                MessageBox.Show("Required customer data is incomplete");
                return;
            }

            CustomerRepository repository = new CustomerRepository();
            repository.Insert(customer);
            LoadCustomers();
            ClearFields();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dgvCustomers.SelectedRows[0];

            selectedCustomerId = Convert.ToInt32(row.Cells["CustomerId"].Value);

            txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
            txtLastName.Text = row.Cells["LastName"].Value.ToString();
            txtAddress.Text = row.Cells["Address"].Value.ToString();
            txtPhone.Text = row.Cells["Phone"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == 0)
                return;

            Customer customer = new Customer
            {
                CustomerId = selectedCustomerId,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (!customer.ValidateData())
                return;

            CustomerRepository repository = new CustomerRepository();
            repository.Update(customer);
            LoadCustomers();
            ClearFields();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == 0)
                return;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            CustomerRepository repository = new CustomerRepository();
            repository.Delete(selectedCustomerId);
            LoadCustomers();
            ClearFields();
        }

        private void ClearFields()
        {
            selectedCustomerId = 0;

            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();

            txtFirstName.Focus();
        }
    }
}
