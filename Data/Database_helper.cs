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
        private static readonly string connectionString = "Data Source=DESKTOP-AIKR8ED\\SQLEXPRESS01;Initial Catalog=real_estate_listing_properties;Integrated Security=True";

        public static int RegisterUser(string firstName, string lastName, string email, string password, string address, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = @"
            INSERT INTO Users (users_name, users_username, users_email, users_password, users_address, users_contact) 
            VALUES (@FirstName, @LastName, @Email, @Password, @Address, @Phone);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phone);

                connection.Open();
                // Since SCOPE_IDENTITY() returns a decimal, we need to cast it properly
                int newUsersId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

                return newUsersId;
            }
        }





        public static string GetSellerId(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT seller_id FROM generate WHERE users_id = @userId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId); 
                connection.Open();
                string sellerId = (string)cmd.ExecuteScalar();
                connection.Close();
                return sellerId;
            }
        }


        public static int ValidUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM users WHERE users_email = @Email AND users_password = @Password", connection))
                {   
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int newUsersId = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                    return newUsersId;

                   
                    
                }
            }
        }

        public static int GetUserId(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT users_id FROM users WHERE users_email = @Email AND users_password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        /*
        public static bool RegisterProperty(string sellerID, string Title, string Location, string Price, string Area, string Bed, string Bath, string type, string status, string Property_Image)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {   
                
                string query = "INSERT INTO property (seller_id, prop_description, prop_location, prop_price, prop_size, prop_bed, prop_bath, prop_type, prop_status, prop_image ) VALUES (@SellerId, @Title, @Location, @Price, @Area, @Bed, @Bath, @type, @status, @Image_01)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@SellerId", sellerID);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Area", Area);
                cmd.Parameters.AddWithValue("@Bed", Bed);
                cmd.Parameters.AddWithValue("@Bath", Bath);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@Image_01", Property_Image);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                connection.Close();
                return result > 0;
            }
        }
        */
        public static bool RegisterProperty(string sellerId, string Title, string Location, string Price, string Area, string Bed, string Bath, string type, string status, string Property_Image)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO property (seller_id, prop_description, prop_location, prop_price, prop_size, prop_bed, prop_bath, prop_type, prop_status, prop_image ) VALUES (@SellerId, @Title, @Location, @Price, @Area, @Bed, @Bath, @type, @status, @Image_01)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@SellerId", sellerId);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Area", Area);
                cmd.Parameters.AddWithValue("@Bed", Bed);
                cmd.Parameters.AddWithValue("@Bath", Bath);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@Image_01", Property_Image);

                connection.Open();
                int result = cmd.ExecuteNonQuery();
                connection.Close();
                return result > 0;
            }
        }

    }
}