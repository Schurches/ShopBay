using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;
namespace ShopBay.Controllers
{
    public class HomeController : Controller
    {
        ShopBayEntities1 db = new ShopBayEntities1();
        public ActionResult Index()
        {
            ViewBag.featuredProducts = db.Products.ToList().GetRange(db.Products.Count()-10,10);
            return View(db.Category.ToList());
        }

        public ActionResult CanjearPlata()
        {
            if(Session["UserID"] != null)
            {
                int plata = Convert.ToInt32(Request.Form["canjear"]);
                Movements mov = new Movements();
                mov.MovementsID = db.Movements.Count() + 1;
                mov.Type = "Cargar";
                mov.UserID = Convert.ToInt32(Session["UserID"]);
                mov.Ammount = plata;
                db.Users.Find(Session["UserID"]).AccMoney = Convert.ToInt32(Session["AccMoney"]) + plata;
                Session["AccMoney"] = Convert.ToInt32(Session["AccMoney"]) + plata;
                db.Movements.Add(mov);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}