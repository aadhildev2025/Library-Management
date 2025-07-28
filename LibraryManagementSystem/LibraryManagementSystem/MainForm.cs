using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AddBookForm ad = new AddBookForm();
            ad.Show();
            this.Hide(); // Hide login form

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Go to LoginForm
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                this.Close(); // Close the current form (logout)
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ViewBooksForm vf = new ViewBooksForm();
            vf.Show();
            this.Hide(); // Hide login form

        }

        private void button3_Click(object sender, EventArgs e)
        {
            IssueBookForm iis = new IssueBookForm();
            iis.Show();
            this.Hide(); // Hide login form
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnBookForm rb = new ReturnBookForm();
            rb.Show();
            this.Hide(); // Hide login form
        }
    }
}
