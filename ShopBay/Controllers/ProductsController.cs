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
        ShopBayEntities1 db = new ShopBayEntities1();
        // GET: Products
        public ActionResult Products(int id)
        {
            //Hay que guardar en un viewbag para usarla en el menu de categorías de productos
            ViewBag.categories = db.Category.ToList();

            var category = db.Category.Find(id);

            ViewBag.categoryTitle = category.CategoryTitle;
            var productsList = db.ProductCategory.SqlQuery("SELECT * FROM ProductCategory WHERE CategoryID = " + id).ToList();
            List<Products> listaDeProductos = new List<Products>();
            foreach (var item in productsList)
            {
                foreach (var otherItem in db.Products)
                {
                    if(item.ProductID == otherItem.ProductID)
                    {
                        listaDeProductos.Add(otherItem);
                    }
                }
            }
            ViewBag.listaContenedoraDeProductos = listaDeProductos;
            return View();
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
                ViewBag.listaContenedoraDeProductos = Resultado;
                return View();
            }
            else
            {
                ViewBag.categoryTitle = "Result of all";
                var Query = from product in db.Products select product;
                ViewBag.listaContenedoraDeProductos = Query;
                return View();
            }
        }
    }
}