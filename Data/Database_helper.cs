using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using UrbanNest.Models;

namespace UrbanNest.Data
{
    public class Database_helper
    {
        private static readonly string connectionString = "Data Source=MRGDFK\\SQLEXPRESS;Initial Catalog=real_estate_listing_properties;Integrated Security=True";

        public static int RegisterUser(string firstName, string lastName, string email, string password, string address, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = @"
            INSERT INTO Users (users_name, users_username, users_email, users_password, users_address, users_contact, users_date) 
            VALUES (@FirstName, @LastName, @Email, @Password, @Address, @Phone, @Time);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Time",DateTime.Now);

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

        public static List<Agent> GetAllAgents()
        {
            List<Agent> agents = new List<Agent>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM agent";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Agent agent = new Agent
                        {
                            AgentId = reader.GetInt32(reader.GetOrdinal("agent_id")),
                            AgentName = reader.GetString(reader.GetOrdinal("agent_name")),
                            AgentUsername = reader.GetString(reader.GetOrdinal("agent_username")),
                            AgentImage = reader.GetString(reader.GetOrdinal("agent_image")),
                            AgentAddress = reader.GetString(reader.GetOrdinal("agent_address")),
                            AgentContact = reader.GetString(reader.GetOrdinal("agent_contact")),
                            AgentEmail = reader.GetString(reader.GetOrdinal("agent_email")),
                            AgentReview = reader.GetString(reader.GetOrdinal("agent_review")),
                            AgentStar = reader.GetDouble(reader.GetOrdinal("agent_star"))
                        };
                        agents.Add(agent);
                    }
                }
                connection.Close();
            }
            return agents;
        }

        public static User GetUserDetails(int userId)
        {
            User user = null;

            // Your database code to get the user details
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Users WHERE users_id = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                FullName = reader["users_name"] != DBNull.Value ? reader["users_name"].ToString() : string.Empty,
                                ProfilePicture = reader["users_image"] != DBNull.Value ? reader["users_image"].ToString() : string.Empty,
                                // Replace other properties with similar checks
                                Email = reader["users_email"] != DBNull.Value ? reader["users_email"].ToString() : string.Empty,
                                Phone = reader["users_contact"] != DBNull.Value ? reader["users_contact"].ToString() : string.Empty,
                                Address = reader["users_address"] != DBNull.Value ? reader["users_address"].ToString() : string.Empty,
                                JoinedDate = reader["users_date"] != DBNull.Value ? (DateTime)reader["users_date"] : default(DateTime)
                            };
                        }
                    }
                }
            }

            return user;
        }


        public static bool UpdateUserProfilePicture(string email, string profilePictureFileName)
        {
            // Your database connection and update logic here
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE users SET users_image = @ProfilePicture WHERE users_email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ProfilePicture", profilePictureFileName);
                    cmd.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }



    }


}