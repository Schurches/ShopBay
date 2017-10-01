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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult APUser(Products newProduct, DateTime DateInput, HttpPostedFileBase inputFile)
        {
            ViewBag.Categories = db.Category.ToList();
            if (ModelState.IsValid)
            {
                newProduct.ProductID = db.Products.Count() + 1;
                newProduct.Rate = 0;
                newProduct.isAuction = 0;
                newProduct.UserID = 1;
                if (Request.Form["selectType"].Equals("Aution"))
                {
                    Auction newAution = new Auction();
                    newAution.AuctionID = db.Auction.Count() + 1;
                    newAution.CurrentBid = newProduct.Price;
                    newAution.ProductID = newProduct.ProductID;
                    newAution.EndDate = DateInput;
                    db.Auction.Add(newAution);
                    newProduct.isAuction = 1;
                }
                newProduct.ShippingID = 1;
                newProduct.ShippingOptions = db.ShippingOptions.Find(1);
                newProduct.Users = db.Users.Find(1);

                ImageCatalog Images = new ImageCatalog();
                Images.ProductID = newProduct.ProductID;
                Images.ProductImage = new byte[inputFile.ContentLength];
                inputFile.InputStream.Read(Images.ProductImage, 0, inputFile.ContentLength);
                newProduct.ImageCatalog = Images;
                Images.Products = newProduct;

                db.Products.Add(newProduct);

                db.SaveChanges();
                

                ModelState.Clear();
            }
            return View(newProduct);
        }

    }
}
