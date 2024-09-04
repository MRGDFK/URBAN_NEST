using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using UrbanNest.Data;
using UrbanNest.Models;

namespace UrbanNest.Controllers
{
    public class AgentsController : Controller
    {
        public ActionResult Agent(string searchString, string sortOrder)
        {
            var agents = GetAgentsFromDatabase();

            // Filter by location
            if (!String.IsNullOrEmpty(searchString))
            {
                //agents = agents.Where(a => a.AgentAddress.Contains(searchString)).ToList();
                var selectedLocations = searchString.Split(',').ToList();
                // Log selected locations for debugging
                System.Diagnostics.Debug.WriteLine("Selected Locations: " + string.Join(", ", selectedLocations));

                agents = agents.Where(a => selectedLocations.Contains(a.AgentAddress)).ToList();
            }

            // Sort by rating
            switch (sortOrder)
            {
                case "rating_desc":
                    agents = agents.OrderByDescending(a => a.AgentStar).ToList();
                    break;
                case "rating_asc":
                    agents = agents.OrderBy(a => a.AgentStar).ToList();
                    break;
                default:
                    agents = agents.OrderByDescending(a => a.AgentStar).ToList();
                    break;
            }

            return View(agents);
        }

        private List<Agent> GetAgentsFromDatabase()

        {
            List<Agent> agents = new List<Agent>();

            string connectionString = "Data Source=MRGDFK\\SQLEXPRESS;Initial Catalog=real_estate_listing_properties;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT *FROM agent"; // Replace with your table name
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agents.Add(new Agent
                            {
                                AgentId = Convert.ToInt32(reader["agent_id"]),
                                AgentName = reader["agent_name"].ToString(),
                                AgentUsername = reader["agent_username"].ToString(),
                                AgentImage = reader["agent_image"].ToString(),
                                AgentAddress = reader["agent_address"].ToString(),
                                AgentContact = reader["agent_contact"].ToString(),
                                AgentEmail = reader["agent_email"].ToString(),
                                AgentReview = reader["agent_review"].ToString(),
                                AgentStar = Convert.ToDouble(reader["agent_star"])
                            });
                        }
                    }
                }
            }

            return agents;
        }
    }
}
