using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResturantBar.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Foods { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

    }
}