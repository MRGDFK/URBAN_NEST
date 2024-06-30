using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace UrbanNest.ViewModels
{
    public class PropertyViewModel
    {
        public List<Property> RecentProperties { get; set; }
        public PropertySearchCriteria SearchCriteria { get; set; }
        public List<Property> SearchResults { get; set; }
    }

    public class PropertySearchCriteria
    {
        public string Location { get; set; }
        public string Price { get; set; }
        public string Bed { get; set; }
        public string Bath { get; set; }
    }

    public class Property
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Area { get; set; }
        public string Bed { get; set; }
        public string Bath { get; set; }
        public string ImagePath { get; set; }
    }
}
