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
        public ActionResult Product(int id)
        {
            var product = db.Products.Find(id);
            ViewBag.Seller = db.Users.Find(product.UserID);
            ViewBag.isAuction = product.isAuction;
            ViewBag.Category = db.ProductCategory.SqlQuery("SELECT * FROM ProductCategory WHERE ProductID = " + id).Single().Category;
            if (product.isAuction == 1)
            {
                var infoSubasta = db.Auction.SqlQuery("SELECT * FROM Auction WHERE ProductID =" + id).First();
                ViewBag.auctionEnd = infoSubasta.EndDate;
                product.Price = db.BidList.SqlQuery("SELECT * FROM BidList WHERE AuctionID = " + infoSubasta.AuctionID).ToList().Last().Bid;
                ViewBag.preciominimo = product.Price+100;
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult BuyProduct(Products prod)
        {
            if (Session["UserID"] != null)
            {
                if (prod.isAuction == 1)
                {
                    //Si aun queda tiempo de la subasta
                    var subasta = db.Auction.SqlQuery("SELECT * FROM Auction WHERE ProductID =" + prod.ProductID).First();
                    var currentBid = db.BidList.SqlQuery("SELECT * FROM BidList WHERE AuctionID = " + subasta.AuctionID).ToList().Last();
                    int userBid = Convert.ToInt32(Request.Form["auctionBID"]);
                    if (userBid >= currentBid.Bid+100)
                    {
                        BidList newBid = new BidList();
                        newBid.BidID = db.BidList.Count() + 1;
                        newBid.AuctionID = currentBid.AuctionID;
                        newBid.UserID = Convert.ToInt32(Session["UserID"]);
                        newBid.Bid = userBid;
                        db.BidList.Add(newBid);
                        db.Products.Find(prod.ProductID).Price = userBid;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var AccMoney = Convert.ToInt32(Session["AccMoney"]);
                    int quantity;
                    String Squantity = Request.Form["quantity"];
                    if (Squantity != null)
                    {
                        quantity = Convert.ToInt32(Squantity);
                        if (quantity <= prod.Existencies && quantity != 0 && prod.Existencies != 0)
                        {
                            if (AccMoney >= prod.Price * quantity)
                            {
                                //Take money from buyer
                                Session["AccMoney"] = AccMoney - prod.Price * quantity;
                                db.Users.Find(Session["UserID"]).AccMoney = Convert.ToInt32(Session["AccMoney"]);
                                //Give money to the seller
                                db.Users.Find(prod.UserID).AccMoney = db.Users.Find(prod.UserID).AccMoney + quantity * prod.Price;
                                //Movement of money
                                Movements mov = new Movements();
                                mov.UserID = Convert.ToInt32(Session["UserID"]);
                                mov.MovementsID = db.Movements.Count() + 1;
                                mov.Ammount = -1 * prod.Price * quantity; //(Es * -1 porque debe restar el valor de la cuenta original))
                                mov.Type = "Compra";
                                db.Movements.Add(mov);
                                //Adding to list of bougth products
                                ProductsSold newSell = new ProductsSold();
                                newSell.SalesID = db.ProductsSold.Count() + 1;
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
                                db.SaveChanges();
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        quantity = 1;
                    }
                }

            }
            return View();
        }

    }
}