using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    
    public class CategoryController : Controller
    {
        
        // GET: Category
        public ActionResult Index()
        {
            DataContext db = new DataContext();
            return View(db.Category.ToList());
        }

        public ActionResult Displayproducts(int? id)
        {
            DataContext db = new DataContext();
            var prod = db.Products.Where(item => item.Category.CatId == id).ToList();
            return View(prod);
        }
    }
}