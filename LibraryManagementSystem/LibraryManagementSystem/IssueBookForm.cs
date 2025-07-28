using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class IssueBookForm : Form
    {
        public IssueBookForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide(); // Hide current form
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int bookId;
            if (!int.TryParse(txtBookId.Text.Trim(), out bookId))
            {
                MessageBox.Show("Enter a valid Book ID.");
                return;
            }

            string studentName = txtStudent.Text.Trim();
            DateTime issueDate = dtpIssueDate.Value;

            if (studentName == "")
            {
                MessageBox.Show("Enter the student's name.");
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\AADHIL\DESKTOP\FINAL\LIBRARYMANAGEMENTSYSTEM\LIBRARYMANAGEMENTSYSTEM\LIBRARYDB.MDF;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT Available FROM Books WHERE Id = @BookId";
                string insertQuery = "INSERT INTO IssuedBooks (BookId, StudentName, IssueDate) VALUES (@BookId, @StudentName, @IssueDate)";
                string updateBookQuery = "UPDATE Books SET Available = 0 WHERE Id = @BookId";

                try
                {
                    conn.Open();

                    // Check availability
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@BookId", bookId);

                    object result = checkCmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Book not found or availability not set.");
                        return;
                    }

                    bool isAvailable;

                    try
                    {
                        isAvailable = Convert.ToBoolean(result); // works for both int and bit
                    }
                    catch
                    {
                        MessageBox.Show("Invalid availability format in database.");
                        return;
                    }

                    if (!isAvailable)
                    {
                        MessageBox.Show("This book is currently not available.");
                        return;
                    }

                    // Insert into IssuedBooks
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@BookId", bookId);
                    insertCmd.Parameters.AddWithValue("@StudentName", studentName);
                    insertCmd.Parameters.AddWithValue("@IssueDate", issueDate);
                    insertCmd.ExecuteNonQuery();

                    // Update book availability
                    SqlCommand updateCmd = new SqlCommand(updateBookQuery, conn);
                    updateCmd.Parameters.AddWithValue("@BookId", bookId);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Book issued successfully.");
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