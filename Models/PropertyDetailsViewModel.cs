using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class PropertyDetailsViewModel
    {
        public property Property { get; set; }
        public Seller Seller { get; set; }
    }
}