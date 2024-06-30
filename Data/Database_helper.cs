using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;


namespace UrbanNest.Data
{
    public class Database_helper
    {
        private static readonly string connectionString = "Data Source=DESKTOP-AIKR8ED\\SQLEXPRESS01;Initial Catalog=real_state_listing_property;Integrated Security=True";
        public static bool RegisterUser(string FirstName, string LastName, string email, string password, string address, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (users_name, users_username, users_email, users_password, users_address, users_contact) VALUES (@FirstName, @LastName, @Email, @Password, @Address, @Phone)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phone);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                connection.Close();
                return result > 0;
            }
        }

        public static bool ValidUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM users WHERE users_email = @Email AND users_password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    return (int)command.ExecuteScalar() > 0;
                }
            }


        }

    }
}