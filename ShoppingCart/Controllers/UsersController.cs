using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
             return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User usr)
        {
            if (ModelState.IsValid)
            {
                //validate the email
                var res = db.Users.Where(item => item.Email == usr.Email);
                if (res.Count() == 0)
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "User Already Exist!";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Userlogin userlogin)
        {
            if (ModelState.IsValid)
            {
                //validate the email and password
                DataContext db = new DataContext();
                var res = db.Users.Where(item => item.Email == userlogin.Email && item.Password == userlogin.Password).ToList();
                if (res.Count() != 0)
                {
                    Session["userid"] = res[0].id;
                    Session["name"] = res[0].Name;
                    if (Session["cart"] != null)
                    {
                        List<Cart> items = (List<Cart>)Session["cart"];
                        CartController obj = new CartController();
                        foreach (var i in items)
                        {
                            obj.Func(i.ProductId, res[0].id, i.Quantity);
                        }
                    }
                    return RedirectToAction("../Category/Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Email Id or Password";
                    return View();
                }
            }

            return View(); 
            
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        

    }
}