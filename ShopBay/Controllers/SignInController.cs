using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;
using System.Web.Security;

namespace ShopBay.Controllers
{
    public class SignInController : Controller
    {

        ShopBayEntities1 bd = new ShopBayEntities1();
        // GET: SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarLogin(Users usuario)
        {
            if (ModelState.IsValid)
            {
                var user = bd.Users.ToList();
                foreach (var item in user)
                {
                    if(item.Username == usuario.Username)
                    {
                        if (item.Password.Equals(usuario.Password))
                        {
                            FormsAuthentication.SetAuthCookie(usuario.Username, true);
                            Session["UserID"] = item.UserID;
                            Session["Username"] = item.Username;
                            Session["Type"] = item.Type;
                            Session["Telephone"] = item.Telephone;
                            Session["Rate"] = item.Rate;
                            Session["Mail"] = item.Mail;
                            Session["Information"] = item.Information;
                            Session["AccMoney"] = item.AccMoney;
                            Session["Image"] = item.Image;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Wrong username or password");
            }
            return View(usuario);    
        }

        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
            var movementsOfUser = bd.Movements.SqlQuery("SELECT * FROM Movements WHERE UserID = " + Session["UserID"]);
            int currentCash = 0;
            foreach (var movement in movementsOfUser)
            {
                currentCash = currentCash + Convert.ToInt32(movement.Ammount);
            }
            bd.Users.Find(Session["UserID"]).AccMoney = currentCash;
            bd.SaveChanges();
            Session["UserID"] = null;
            Session["Username"] = null;
            Session["Type"] = null;
            Session["Telephone"] = null;
            Session["Rate"] = null;
            Session["Mail"] = null;
            Session["Information"] = null;
            Session["AccMoney"] = null;
            Session["Image"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}