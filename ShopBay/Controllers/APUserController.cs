using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;
namespace ShopBay.Controllers
{
    public class APUserController : Controller
    {
        ShopBayEntities db = new ShopBayEntities();
        public ActionResult APUser()
        {
            ViewBag.Categories = db.Category.ToList();
            return View();
        }

        
    }
}
