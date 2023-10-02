using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

        public ObservableCollection<int> GetUser()
        {
            ObservableCollection<int> userIds = new ObservableCollection<int>();

            string query = "SELECT id FROM reviews";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(0); // Assuming the ID is in the first column (index 0)
                        userIds.Add(userId);
                    }
                }

                conn.Close();
            }

            return userIds;
        }



        public DataTable displayData(int id)
        {
            conn.Open();

            string query = "SELECT * FROM reviews WHERE id = @ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id); // Add the @ID parameter

            SqlDataAdapter sba = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Reviews");
            sba.Fill(dt);

            conn.Close();
            return dt;
        }

        public DataTable displayAll()
        {
            conn.Open();

            string query = "SELECT * FROM reviews";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@ID", id); // Add the @ID parameter

            SqlDataAdapter sba = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Reviews");
            sba.Fill(dt);

            conn.Close();
            return dt;
        }

    }
}
