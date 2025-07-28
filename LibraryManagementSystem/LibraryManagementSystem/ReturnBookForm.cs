using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class ReturnBookForm : Form
    {
        public ReturnBookForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide(); // Hide login form
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            {
                int bookId;
                if (!int.TryParse(txtBookId.Text.Trim(), out bookId))
                {
                    MessageBox.Show("Enter a valid Book ID.");
                    return;
                }

                string studentName = txtStudent.Text.Trim();

                if (string.IsNullOrEmpty(studentName))
                {
                    MessageBox.Show("Enter the student's name.");
                    return;
                }

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\AADHIL\DESKTOP\FINAL\LIBRARYMANAGEMENTSYSTEM\LIBRARYMANAGEMENTSYSTEM\LIBRARYDB.MDF;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string checkQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE BookId = @BookId AND StudentName = @StudentName";
                    string deleteQuery = "DELETE FROM IssuedBooks WHERE BookId = @BookId AND StudentName = @StudentName";
                    string updateBookQuery = "UPDATE Books SET Available = 1 WHERE Id = @BookId";

                    try
                    {
                        conn.Open();

                        // Check if the record exists
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@BookId", bookId);
                        checkCmd.Parameters.AddWithValue("@StudentName", studentName);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("No matching issued book record found.");
                            return;
                        }

                        // Delete the issue record
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                        deleteCmd.Parameters.AddWithValue("@BookId", bookId);
                        deleteCmd.Parameters.AddWithValue("@StudentName", studentName);
                        deleteCmd.ExecuteNonQuery();

                        // Update book availability
                        SqlCommand updateCmd = new SqlCommand(updateBookQuery, conn);
                        updateCmd.Parameters.AddWithValue("@BookId", bookId);
                        updateCmd.ExecuteNonQuery();

                        MessageBox.Show("Book returned successfully!");
                        txtBookId.Clear();
                        txtStudent.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
