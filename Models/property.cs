using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class property
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public string Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Area")]
        public string Area { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Bed")]
        public string Bed { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Bath")]
        public string Bath { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Type")]
        public string type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Status")]
        public string status { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Image")]
        public string Image_01 { get; set; }
    }
}