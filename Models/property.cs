using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class property
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Area { get; set; }
        public string Bed { get; set; }
        public string Bath { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image_01 { get; set; }
    }
}