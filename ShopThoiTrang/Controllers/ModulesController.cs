using ShopThoiTrang.Common;
using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class ModulesController : Controller
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        public ActionResult _header()
        {

            return View("_header");
        }
        public ActionResult _mainmenu()
        {
            Muser sessionUser = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            if (sessionUser != null)
            {
                ViewBag.fullname = sessionUser.fullname;
            }
            else
            {

                ViewBag.fullname = null;
            }
            var list = db.Menus.Where(m => m.parentid == 0 && m.status == 1).OrderBy(m => m.orders)
             .Where(m => m.position == "mainmenu").ToList();
            return View("_mainmenu", list);
        }
        public ActionResult SubMainMenu(int parentid)
        {
            ViewBag.menuitem = db.Menus.Find(parentid);
            var list = db.Menus.Where(m => m.parentid == parentid && m.status == 1)
                .Where(m => m.position == "mainmenu");
            if (list.Count() != 0)
            {
                return View("_SubMainMenu2", list.ToList());
            }
            else
            {
                return View("_SubMainMenu1", list);
            }
        }
        public ActionResult _slider()
        {
            var list = db.Sliders.Where(m => m.status == 1 && m.position == "SliderShow").OrderByDescending(m => m.orders).ToList();
            return View("_slider", list);
        }
        public ActionResult _footer()
        {
            return View("_footer");
        }
        //Post
        public ActionResult _postHome()
        {
            var list = db.Posts.Where(m => m.status == 1).OrderByDescending(m=>m.ID).Take(3).ToList();
            return View("_postHome", list);
        }

        //category
        public ActionResult _category(){
            var list = db.Categorys.Where(m => m.status == 1).ToList();
            return View("_category", list);
        }

        //product || get 10 product of home
        public ActionResult _ProductNewHome()
        {
            var listProduct = db.Products.Where(m => m.status == 1).OrderByDescending(m => m.ID).Take(10).ToList();
            return View("_ProductNewHome", listProduct);
        }
        public ActionResult _relatedProducts(int CatId) {
            var listProduct = db.Products.Where(m => m.status == 1 && m.catid==CatId).OrderByDescending(m => m.ID).Take(5).ToList();
            return View("_relatedProducts", listProduct);
        }

    }
}