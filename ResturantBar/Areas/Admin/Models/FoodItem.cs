using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResturantBar.Areas.Admin.Models
{
    public class FoodItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public Double Price { get; set; }
        [Required]
        public string FoodTypeName { get; set; }

    }
}