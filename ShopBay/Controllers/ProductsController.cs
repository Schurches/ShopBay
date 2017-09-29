using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBay.Models;
namespace ShopBay.Controllers
{
    public class ProductsController : Controller
    {
        ShopBayEntities db = new ShopBayEntities();
        // GET: Products
        public ActionResult Products(int id)
        {
            //Hay que guardar en un viewbag para usarla en el menu de categorías de productos
            ViewBag.categories = db.Category.ToList();

            var category = db.Category.Find(id);

            ViewBag.categoryTitle = category.CategoryTitle;
            var productsList = category.Products.ToList();

            return View(productsList);
        }
        [HttpPost]
        public ActionResult Products(string keyWords)
        {
            ViewBag.categories = db.Category.ToList();
            ViewBag.categoryTitle = "Result of " + keyWords;
            if (!String.IsNullOrEmpty(keyWords) && !keyWords.Trim().Equals(""))
            {
                List<Products> Tabla = db.Products.ToList();
                List<Products> Resultado = new List<Products>();
                foreach (Products i in Tabla)
                {
                    string[] elements = keyWords.Replace(' ', ';').Split(';');
                    foreach (string key in elements)
                    {
                        string name = i.Name.ToLower();
                        if (key != "" && name.Contains(key.ToLower()))
                        {
                            Resultado.Add(i);
                        }
                    }
                }
                return View(Resultado.AsQueryable());
            }
            else
            {
                ViewBag.categoryTitle = "Result of all";
                var Query = from product in db.Products select product;
                return View(Query);
            }
        }
    }
}