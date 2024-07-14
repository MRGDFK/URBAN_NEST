using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class User
    {
        [Key]
        public int users_id { get; set; }

        [Required]
        public string users_name { get; set; }

        [Required]
        public string users_username { get; set; }

        public string users_image { get; set; }

        [Required]
        public string users_address { get; set; }

        [Required]
        public string users_contact { get; set; }

        [Required]
        [EmailAddress]
        public string users_email { get; set; }

        [Required]
        [MinLength(8)]
        public string users_password { get; set; }
    }
}
