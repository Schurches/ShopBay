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
        ShopBayEntities1 db = new ShopBayEntities1();
        public ActionResult APUser()
        {
            ViewBag.Categories = db.Category.ToList();
            ViewBag.ShippingOP = db.ShippingOptions.ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult APUser(Products newProduct, DateTime DateInput, HttpPostedFileBase inputFile)
        {
            ViewBag.Categories = db.Category.ToList();
            ViewBag.ShippingOP = db.ShippingOptions.ToList();
            if (ModelState.IsValid)
            {
                newProduct.ProductID = db.Products.ToList().Last().ProductID + 1;
                newProduct.Rate = 0;
                newProduct.isAuction = 0;
                newProduct.UserID = 1;
                if (Request.Form["selectType"].Equals("Aution"))
                {
                    Auction newAution = new Auction();
                    newAution.AuctionID = db.Auction.ToList().Last().AuctionID + 1;
                    newAution.CurrentBid = newProduct.Price;
                    newAution.ProductID = newProduct.ProductID;
                    newAution.EndDate = DateInput;
                    db.Auction.Add(newAution);
                    newProduct.isAuction = 1;
                }
                else
                {
                    OnSaleProducts newSale = new OnSaleProducts();
                    newSale.SaleID = db.OnSaleProducts.ToList().Last().SaleID + 1;
                    newSale.ProductID = newProduct.ProductID;
                }


                foreach (var c in db.ShippingOptions.ToList())
                {
                    if (Request.Form["selectShipping"].Equals(c.ShippingTitle))
                    {
                        newProduct.ShippingID = c.ShippingID;
                    }
                }

                foreach (var c in db.Category.ToList())
                {
                    if (Request.Form["selectCategory"].Equals(c.CategoryTitle))
                    {
                        ProductCategory newProductCategory = new ProductCategory();
                        newProductCategory.ProductCategoryID = db.ProductCategory.ToList().Last().ProductCategoryID + 1;
                        newProductCategory.CategoryID = c.CategoryID;
                        newProductCategory.ProductID = newProduct.ProductID;
                        db.ProductCategory.Add(newProductCategory);
                    }
                }
                if (inputFile != null)
                {
                    newProduct.Image = new byte[inputFile.ContentLength];
                    inputFile.InputStream.Read(newProduct.Image, 0, inputFile.ContentLength);
                }

                db.Products.Add(newProduct);
                db.SaveChanges();
                ModelState.Clear();
            }
            return View();
        }

    }
}