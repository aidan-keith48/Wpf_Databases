using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
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

        public void addReview(Reviews review, List<User> userList)
        {
            conn.Open();
            string name = review.Name;
            string email = review.Email;
            string product = review.Product;
            string comment = review.Comment;
            string currentUser = getUserFromList(userList);


            string query = "INSERT INTO reviews1 (username,name, email, product, comment) " +
                           "VALUES (@User, @Name, @Email, @Product, @Comment)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", currentUser);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Product", product);
                cmd.Parameters.AddWithValue("@Comment", comment);

                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public void doesUserExist(string user)
        {
            int count = 0;


            try
            {
                conn.Open();

                // Query to check if the user exists
                string query = "SELECT COUNT(*) FROM [User] WHERE username = @Username";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Username", user);
                    count = (int)command.ExecuteScalar(); // ExecuteScalar returns the number of matching rows
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                // Handle the exception (e.g., log it, show an error message)
            }


            if (count > 0)
            {
                MessageBox.Show("User exists. Redirect to the next screen.");
                // Perform the redirection to the next screen here

            }
            else
            {
                MessageBox.Show("User does not exist. Display an error message or take appropriate action.");
                // Display an error message or take appropriate action

            }
        }

        public string getUserFromList(List<User> userList)
        {
            string username = "";
            foreach (var item in userList)
            {
                username = item.Username.ToString();
            }

            return username;
        }

        public ObservableCollection<string> GetUser()
        {
            ObservableCollection<string> products = new ObservableCollection<string>();

           
                conn.Open();

                string query = "SELECT product FROM reviews1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string product = reader.GetString(0); // Assuming the 'product' column is in the first column (index 0)
                            products.Add(product);
                        }
                    }
                }
            return products;
        }


        public void addUserInfo(string user)
        {

            conn.Open();

            // Create an SQL command to insert user information into the database
            string insertQuery = "INSERT INTO [User] (username) VALUES (@Username)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                // Add parameters
                cmd.Parameters.AddWithValue("@Username", user);

                // Execute the SQL command to insert the user information
                cmd.ExecuteNonQuery();
            }

            // Close the database connection
            conn.Close();

        }



        public DataTable displayData(List<User> userList)
        {
            conn.Open();
            string user = getUserFromList(userList);

            string query = "SELECT * FROM reviews1 WHERE username = @User";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@User", user); // Add the @ID parameter

            SqlDataAdapter sba = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Reviews");
            sba.Fill(dt);

            conn.Close();
            return dt;
        }

        public DataTable displayAll()
        {
            conn.Open();

            string query = "SELECT * FROM reviews1";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@ID", id); // Add the @ID parameter

            SqlDataAdapter sba = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Reviews1");
            sba.Fill(dt);

            conn.Close();
            return dt;
        }

    }
}
