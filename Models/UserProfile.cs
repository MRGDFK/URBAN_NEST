﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public ICollection<RealEstateListing> Listings { get; set; }
    }
}
