using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Product
    {
        [Key]
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public int ProdPrice { get; set; }
        public string ImgPro { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
    }
}