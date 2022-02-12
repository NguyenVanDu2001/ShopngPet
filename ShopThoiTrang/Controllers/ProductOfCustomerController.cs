using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class ProductOfCustomerController : Controller
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        // GET: ProductOfCustomer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
           var product =  db.Products.FirstOrDefault(x => x.ID == id.Value);
            //var category = 
            ViewBag.viewBagCategoryByProductDeteil = product != null ?  db.Categorys.Where(x => x.ID == product.catid).FirstOrDefault() : null; 
             ViewBag.productByCategory = product != null ? db.Products.Where(x => x.catid == product.catid).ToList() : null;

            return View(product);
        }
            
    }
}