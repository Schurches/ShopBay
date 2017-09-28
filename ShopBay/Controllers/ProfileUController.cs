using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;
namespace Intento.Controllers
{
    public class ProfileUController : Controller
    {
        ShopBayEntities db = new ShopBayEntities();
        // GET: Profile
        public ActionResult ProfileU(int id)
        {
            return View(db.Users.Find(id));
        }

        public PartialViewResult History()
        {
            return PartialView();
        }
    }
}