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
        ShopBayEntities1 db = new ShopBayEntities1();
        static int userRID = 0;
        // GET: Profile
        public ActionResult ProfileU(int? id)
        {
            if (id != null)
            {
                var userR = db.Users.Find((int)id);
                ViewBag.Comentaries = db.ProfileCommentary.Where(pc => pc.RatedUserID == userR.UserID).ToList();
                ViewBag.User = userR;
                userRID = (int)id;
            }
            else
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ProfileU(ProfileCommentary comentary)
        {
            if (Session["UserID"] != null)
            {
                var user = db.Users.Find(Int32.Parse(Session["UserID"].ToString()));
                var userR = db.Users.Find(userRID);
                ViewBag.User = userR;
                if (ModelState.IsValid)
                {
                    comentary.ProfileCommentaryID = db.ProfileCommentary.ToList().Last().ProfileCommentaryID + 1;
                    comentary.UserID = user.UserID;
                    comentary.RatedUserID = userR.UserID;
                    comentary.Rate = Int32.Parse(Request.Form["rateUserC"]);
                    //No se si estos manden error del foreing key, el modelo los tiene y los pongo para llenar todo los campos
                    comentary.Users = user;
                    comentary.Users1 = userR;

                    var comentaries = db.ProfileCommentary.Where(p => p.RatedUserID == userR.UserID).ToList();
                    int x = comentaries.Count;
                    double r = (double)db.Users.Find(userR.UserID).Rate;

                    db.Users.Find(userR.UserID).Rate = (double)(x * r + (double)comentary.Rate) / (x + 1);

                    db.ProfileCommentary.Add(comentary);

                    db.SaveChanges();

                    ViewBag.Comentaries = db.ProfileCommentary.Where(p => p.RatedUserID == userR.UserID).ToList();
                    ModelState.Clear();
                }
            }
            else
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            return View();
        }

    }
}