using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;

namespace ShopBay.Controllers
{
    public class AdminController : Controller
    {
        ShopBayEntities1 db = new ShopBayEntities1();
        // GET: Admin
        public ActionResult Admin()
        {
            ViewBag.Usuarios = db.Users.ToList();
            ViewBag.Productos = db.Products.ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Admin(Category newCategory, HttpPostedFileBase uploadFile)
        {
            ViewBag.Usuarios = db.Users.ToList();
            ViewBag.Productos = db.Products.ToList();
            if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    newCategory.CategoryImage = new byte[uploadFile.ContentLength];
                    uploadFile.InputStream.Read(newCategory.CategoryImage, 0, uploadFile.ContentLength);
                }
                newCategory.CategoryID = db.Category.Count() + 1;
                db.Category.Add(newCategory);
                db.SaveChanges();
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            Users user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            ViewBag.Usuarios = db.Users.ToList();
            ViewBag.Productos = db.Products.ToList();
            return RedirectToAction("Admin", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            Products product = db.Products.Find(id);
            foreach (var onSale in db.OnSaleProducts.ToList())
            {
                if (product.ProductID == onSale.ProductID)
                {
                    db.OnSaleProducts.Remove(onSale);
                }
            }

            db.Products.Remove(product);
            db.SaveChanges();
            ViewBag.Usuarios = db.Users.ToList();
            ViewBag.Productos = db.Products.ToList();
            return RedirectToAction("Admin", "Admin");
        }


    }
}