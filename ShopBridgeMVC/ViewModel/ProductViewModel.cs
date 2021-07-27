using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridgeMVC.ViewModel
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Price field must be greater than 0")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ManufacturedBY { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Stocks must be Greater than 0")]
        public int Stock { get; set; }
    }
}