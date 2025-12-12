using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            new CustomerForm().ShowDialog();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            new ProductForm().ShowDialog();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            new SalesForm().ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
