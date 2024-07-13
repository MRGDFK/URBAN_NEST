using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class Agent
    {
        public int AgentId { get; set; }

        public string AgentName { get; set; }
        public string AgentUsername { get; set; }
        public string AgentImage { get; set; }
        public string AgentAddress { get; set; }
        public string AgentContact { get; set; }
        public string AgentEmail { get; set; }
       
        public string AgentReview { get; set; }
        public double AgentStar { get; set; }
    }
}

