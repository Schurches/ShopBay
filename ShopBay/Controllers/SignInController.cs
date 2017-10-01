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

        ShopBayEntities bd = new ShopBayEntities();
        // GET: SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarLogin(Users usuario)
        {
            //if (ModelState.IsValid)
            //{
                var user = bd.Users.ToList();
                foreach (var item in user)
                {
                    if (item.Username == usuario.Username)
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
            //}
            return RedirectToAction("SignIn","SignIn");
        }

        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
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