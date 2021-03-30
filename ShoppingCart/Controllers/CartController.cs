using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            /*if user logged in*/
            if (Session["userid"] != null)
            {
                
                var res = db.Cart.Where(item => item.UserId == Convert.ToInt32(Session["userid"])).ToList();
               
                List<Product> result = new List<Product>();
                foreach (var i in res)
                {
                    Product product = (Product)db.Products.Where(item => item.ProdId == i.ProductId).First();
                    product.Quantity = i.Quantity;
                    result.Add(product);
                }
                return View(result);
            }
            else
            {
                if (Session["cart"] == null)
                {
                    List<Product> empty = new List<Product>();
                    return View(empty);
                }
                List<Cart> items = (List<Cart>)Session["cart"];
               
                var products = db.Products;
                List<Product> result = new List<Product>();
                foreach (var i in items)
                {
                    Product p = (Product)products.Where(item => item.ProdId == i.ProductId).First();
                    p.Quantity = i.Quantity;

                    result.Add(p);
                }
                return View(result);
            }
        }

        public void Func(int? id, int n, int? quantity)
        {
            DataContext db = new DataContext();
            var items = db.Cart.Where(item => item.ProductId == id && item.UserId == n).ToList();
            if (items.Any())
            {
                Cart item = items[0];
                if (quantity != null)
                    item.Quantity = item.Quantity + Convert.ToInt32(quantity);
                else
                    item.Quantity = item.Quantity + 1;
                db.Update(item);
                db.SaveChanges();
            }
            else
            {
                Cart cart = new Cart();
                cart.UserId = n;
                cart.CartId = cart.UserId;
                cart.ProductId = Convert.ToInt32(id);
                if (quantity != null)
                    cart.Quantity = Convert.ToInt32(quantity);
                else
                    cart.Quantity = 1;
                db.Cart.Add(cart);
                db.SaveChanges();
            }

        }

        public ActionResult AddToCart(int? id)
        {
            DataContext db = new DataContext();
            if (Session["userid"] == null && id != null)
            {
                if (Session["cart"] == null)
                {
                    List<Cart> items = new List<Cart>();
                    Cart cart = new Cart();
                    cart.ProductId = Convert.ToInt32(id);
                    cart.Quantity = 1;
                    //cart.SessionId = Session.SessionID;
                    items.Add(cart);
                    Session["cart"] = items;
                }
                else
                {
                    List<Cart> items = (List<Cart>)Session["cart"];
                    var res = from x in items
                              where x.ProductId == id
                              select x;
                    if (res.Any())
                    {
                        res.First().Quantity += 1;
                        Session["cart"] = items;
                    }
                    else
                    {
                        Cart cart = new Cart();
                        cart.ProductId = Convert.ToInt32(id);
                        cart.Quantity = 1;
                        //cart.SessionId = Session.SessionID;
                        items.Add(cart);
                        Session["cart"] = items;
                    }
                }
                return RedirectToAction("Index");
            }

            /*if user is logged in*/
            if (Session["userid"] != null && id != null)
            {
                Func(id, Convert.ToInt32(Session["userid"]), null);
            }
            return RedirectToAction("Index");


        }

        public ActionResult RemoveItem(int id)
        {
            var res = HandleRemoveItem(id);
            return res;
        }

        public ActionResult HandleRemoveItem(int id)
        {
            if (Session["userid"] == null)
            {
                List<Cart> items = (List<Cart>)Session["cart"];
                var res = from x in items
                          where x.ProductId == id
                          select x;
                if (res.Any())
                {
                    items.Remove(res.First());
                    Session["cart"] = items;
                }
                return RedirectToAction("Index");
            }
            else
            {
                DataContext db = new DataContext();
                var res = db.Cart.Where(item => item.ProductId == id && Convert.ToInt32(Session["userid"]) == item.UserId).First();
                db.Cart.Remove(res);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public void incrementItem(int id)
        {
            if (Session["userid"] == null)
            {
                List<Cart> items = (List<Cart>)Session["cart"];
                var res = from x in items
                          where x.ProductId == id
                          select x;
                if (res.Any())
                {
                    res.First().Quantity += 1;
                    Session["cart"] = items;
                }
            }
            else
            {
                DataContext db = new DataContext();
                var result = db.Cart.Where(item => item.ProductId == id && item.UserId == Convert.ToInt32(Session["UserID"])).First();
                result.Quantity = result.Quantity + 1;
                db.Update(result);
                db.SaveChanges();
            }
        }

        public void decrementItem(int id)
        {
            DataContext db = new DataContext();

            if (Session["userid"] == null)
            {
                List<Cart> items = (List<Cart>)Session["cart"];
                var res = from x in items
                          where x.ProductId == id
                          select x;
                if (res.Any())
                {
                    res.First().Quantity -= 1;
                    if (res.First().Quantity <= 0)
                    {
                        items.Remove(res.First());
                    }
                    Session["cart"] = items;
                }
            }
            else
            {
              
                var result = db.Cart.Where(item => item.ProductId == id && item.UserId == Convert.ToInt32(Session["UserID"])).First();
                result.Quantity = result.Quantity - 1;
                if (result.Quantity <= 0)
                {
                    db.Remove(result);
                }
                else
                {
                    db.Update(result);
                }
                db.SaveChanges();
            }
        }
    }
}