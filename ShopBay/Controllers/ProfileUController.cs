using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;
namespace ShopBay.Controllers
{
    public class ProfileUController : Controller
    {
        ShopBayEntities db = new ShopBayEntities();
        // GET: Profile
        public ActionResult ProfileU(int id)
        {
            var user = db.Users.Find(id);
            ViewBag.Comentaries = db.ProfileCommentary.Where(pc => pc.UserID == user.UserID).ToList();
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public ActionResult ProfileU(ProfileCommentary comentary)
        {
            var user= db.Users.Find(comentary.UserID);
            var userR= db.Users.Find(comentary.RatedUserID);
            ViewBag.Comentaries = db.ProfileCommentary.Where(pc => pc.UserID == comentary.UserID).ToList();
            ViewBag.User = user;
            if (ModelState.IsValid)
            {
                comentary.ProfileCommentaryID = db.ProfileCommentary.Count() + 1;
                comentary.UserID = comentary.UserID;
                comentary.RatedUserID = comentary.RatedUserID;
                comentary.Rate = Int32.Parse(Request.Form["rateUserC"]);
                //No se si estos manden error del foreing key, el modelo los tiene y los pongo para llenar todo los campos
                comentary.Users = user;
                comentary.Users1 = userR;
                
                db.ProfileCommentary.Add(comentary);
                db.SaveChanges();
                ModelState.Clear();
            }
            
            return View(comentary);
        }
        
    }
}