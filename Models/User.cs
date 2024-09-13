using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}