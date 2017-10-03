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
            ViewBag.featuredProducts = db.Products.ToList().GetRange(db.Products.Count()-11,10);
            return View(db.Category.ToList());
        }
    }
}