using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;

namespace ShopBay.Controllers

{
    public class ProductController : Controller
    {
        ShopBayEntities1 db = new ShopBayEntities1();
        // GET: Product


        public ActionResult Product()
        {
            return View();
        }


        public ActionResult Product(int id)
        {
            var product = db.Products.Find(id);
            ViewBag.Seller = db.Users.Find(product.UserID);
            ViewBag.isAuction = product.isAuction;
            var executedQuery = db.ProductCategory.SqlQuery("SELECT * FROM ProductCategory WHERE ProductID = " + id).Single();
            ViewBag.productInstance = product;
            foreach (var item in db.Category)
            {
                if(item.CategoryID == executedQuery.CategoryID)
                {
                    ViewBag.Category = item;
                    return View(product);
                }
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult BuyProduct(Products prod)
        {
            if(Session["UserID"] != null)
            {
                var AccMoney = Convert.ToInt32(Session["AccMoney"]);
                int quantity;
                String Squantity = Request.Form["quantity"];
                if (Squantity != null)
                {
                    quantity = Convert.ToInt32(Squantity);
                    if (quantity <= prod.Existencies)
                    {
                        if (AccMoney >= prod.Price * quantity)
                        {
                            //Take money from buyer
                            Session["AccMoney"] = AccMoney - prod.Price*quantity;
                            db.Users.Find(Session["UserID"]).AccMoney = Convert.ToInt32(Session["AccMoney"]);
                            //Give money to the seller
                            /////////////Code goes here///////////

                            //Movement of money
                            Movements mov = new Movements();
                            mov.UserID = Convert.ToInt32(Session["UserID"]);
                            mov.MovementsID = db.Movements.Count()+1;
                            mov.Ammount = -1 * prod.Price * quantity; //(Es * -1 porque debe restar el valor de la cuenta original))
                            mov.Type = "Compra";
                            db.Movements.Add(mov);
                            //Adding to list of bougth products
                            ProductsSold newSell = new ProductsSold();
                            newSell.SalesID = db.ProductsSold.Count()+1;
                            newSell.BuyerID = Convert.ToInt32(Session["UserID"]);
                            newSell.SellerID = prod.UserID;
                            newSell.ProductID = prod.ProductID;
                            newSell.BuyDate = DateTime.Today;
                            newSell.Price = prod.Price;
                            newSell.Quantity = quantity;
                            newSell.ShippingID = prod.ShippingID;
                            db.ProductsSold.Add(newSell);
                            //Actualizar las existencias de un producto
                            var productList = db.Products.ToList();
                            foreach (var product in productList)
                            {
                                if (product.ProductID == prod.ProductID)
                                {
                                    product.Existencies = product.Existencies - quantity;
                                    prod.Existencies = product.Existencies;
                                    break;
                                }
                            }
                            if (prod.Existencies == 0)
                            {
                                var productosEnVenta = db.OnSaleProducts.ToList();
                                foreach (var sellProduct in productosEnVenta)
                                {
                                    if(sellProduct.ProductID == prod.ProductID)
                                    {
                                        db.OnSaleProducts.Remove(sellProduct);
                                        break;
                                    }
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                }else
                {
                    quantity = 1;
                }
            }
            return View(prod);
        }

    }
}