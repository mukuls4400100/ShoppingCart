using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Cart
    {
        [Key]
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
       // public string SessionId { get; set; }
        public int UserId { get; set; }

        public Product Products { get; set; } 
    }
}