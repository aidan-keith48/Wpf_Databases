using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf_Databases.Classes;

namespace Wpf_Databases.DataController
{
    //Connection string
    //Code for database
    public class dbController
    {
        SqlConnection conn;
        SqlCommand cmd;
        public dbController() 
        { 
            //Database connection
            conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lab_services_student\\source\\repos\\Wpf_Databases\\Wpf_Databases\\Storage\\reviewDatabase.mdf;Integrated Security=True");
        }

        public void addReview(Reviews review)
        {
            conn.Open();
            string name = review.Name;
            string email = review.Email;
            string product = review.Product;
            string comment = review.Comment;

            string query = "INSERT INTO reviews (name, email, product, comment) " +
                           "VALUES (@Name, @Email, @Product, @Comment)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Product", product);
                cmd.Parameters.AddWithValue("@Comment", comment);

                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public string displayData()
        {
            conn.Open();

            string query = "SELECT * FROM reviews";
            StringBuilder result = new StringBuilder();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Assuming your table has columns named "Name," "Email," "Product," and "Comment"
                        string name = reader["Name"].ToString();
                        string email = reader["Email"].ToString();
                        string product = reader["Product"].ToString();
                        string comment = reader["Comment"].ToString();

                        // Format the data and append it to the result
                        result.AppendLine($"Name: {name}, Email: {email}, Product: {product}, Comment: {comment}");
                    }
                }
            }

            conn.Close();

            // Convert the StringBuilder to a string
            return result.ToString();
        }

    }
}
