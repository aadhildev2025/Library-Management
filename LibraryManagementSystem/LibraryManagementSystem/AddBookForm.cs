using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class AddBookForm : Form
    {
        public AddBookForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string category = cmb.Text.Trim();

            if (title == "" || author == "" || category == "")
            {
                MessageBox.Show("Please fill in title, author, and select a category.");
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\AADHIL\DESKTOP\FINAL\LIBRARYMANAGEMENTSYSTEM\LIBRARYMANAGEMENTSYSTEM\LIBRARYDB.MDF;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // ✅ Include the Category column in the query
                string query = "INSERT INTO Books (Title, Author, Category, Available) VALUES (@Title, @Author, @Category, 1)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Category", category);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book added successfully!");
                    txtTitle.Clear();
                    txtAuthor.Clear();
                    cmb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide(); // Hide login form
        }
    }
}