using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using UrbanNest.ViewModels;

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

        private List<Property> GetRecentProperties()
        {
            // Fetch recent properties from the database
            using (SqlConnection connection = new SqlConnection(Database_helper.connectionString))
            {
                var properties = new List<Property>();
                string query = "SELECT TOP 5 * FROM property ORDER BY prop_id DESC";

                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    properties.Add(new Property
                    {
                        Title = reader["prop_title"].ToString(),
                        Location = reader["prop_location"].ToString(),
                        Price = reader["prop_price"].ToString(),
                        Area = reader["prop_area"].ToString(),
                        Bed = reader["prop_bed"].ToString(),
                        Bath = reader["prop_bath"].ToString(),
                        Description = reader["prop_description"].ToString(),
                        ImagePath = "~/assets/images/properties/" + reader["prop_image"].ToString()
                    });
                }
                connection.Close();
                return properties;
            }
        }

        private List<Property> SearchProperties(PropertySearchCriteria searchCriteria)
        {
            // Fetch properties based on search criteria from the database
            using (SqlConnection connection = new SqlConnection(Database_helper.connectionString))
            {
                var properties = new List<Property>();
                string query = "SELECT * FROM property WHERE 1=1";

                if (!string.IsNullOrEmpty(searchCriteria.Location))
                    query += " AND prop_location LIKE @Location";
                if (!string.IsNullOrEmpty(searchCriteria.Price))
                    query += " AND prop_price <= @Price";
                if (!string.IsNullOrEmpty(searchCriteria.Bed))
                    query += " AND prop_bed >= @Bed";
                if (!string.IsNullOrEmpty(searchCriteria.Bath))
                    query += " AND prop_bath >= @Bath";

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(searchCriteria.Location))
                    cmd.Parameters.AddWithValue("@Location", "%" + searchCriteria.Location + "%");
                if (!string.IsNullOrEmpty(searchCriteria.Price))
                    cmd.Parameters.AddWithValue("@Price", searchCriteria.Price);
                if (!string.IsNullOrEmpty(searchCriteria.Bed))
                    cmd.Parameters.AddWithValue("@Bed", searchCriteria.Bed);
                if (!string.IsNullOrEmpty(searchCriteria.Bath))
                    cmd.Parameters.AddWithValue("@Bath", searchCriteria.Bath);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    properties.Add(new Property
                    {
                        Title = reader["prop_title"].ToString(),
                        Location = reader["prop_location"].ToString(),
                        Price = reader["prop_price"].ToString(),
                        Area = reader["prop_area"].ToString(),
                        Bed = reader["prop_bed"].ToString(),
                        Bath = reader["prop_bath"].ToString(),
                        Description = reader["prop_description"].ToString(),
                        ImagePath = "~/assets/images/properties/" + reader["prop_image"].ToString()
                    });
                }
                connection.Close();
                return properties;
            }
        }
    }
}