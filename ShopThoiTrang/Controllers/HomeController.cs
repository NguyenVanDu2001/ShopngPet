using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class HomeController : Controller
    {
        // khởi tạo session:
        private const string SessionCart = "SessionCart";
        // GET: Cart
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        public ActionResult Index()
        {

            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            else if (cart == null)
            {
                ViewBag.statusCart = "Cart empty";

            }
            return View(list);
        }
        public ActionResult IndexClient()
        {
            return View();
        }
        public ActionResult CartIndex()
        {
            return View();
        }
        public  ActionResult ProductIndex()
        {
            return View();
        }
        [HttpGet]
        public RedirectResult deleteitem(long productID, string size)
        {
            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;

            Cart_item itemXoa = list.FirstOrDefault(m => m.product.ID == productID && m.Size == size);
            if (itemXoa != null)
            {
                list.Remove(itemXoa);
            }
            Message.set_flash("delete success", "success");
            return Redirect("~/gio-hang");
        }
        public RedirectResult deleteAllCart()
        {
            Session["SessionCart"] = null;
            return Redirect("~/gio-hang");
        }
        public ActionResult cart_header()
        {

            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            //float priceTotol = 0;
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
                //foreach (var item1 in list)
                //{
                //    if (item1.product.pricesale > 0)
                //    {
                //        int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);

                //        priceTotol += temp;
                //    }
                //    else
                //    {
                //        int temp = (int)item1.product.price * (int)item1.quantity;
                //        priceTotol += temp;
                //    }
                //}
            }
            //ViewBag.CartTotal = priceTotol;
            ViewBag.CartCoun = list.Count();
            return View("_cartHeader");
        }
        [HttpGet]
        public JsonResult updateitem(long productID, int quantity, string size, int protype)
        {

            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;
            Cart_item item1 = null;
            if (protype == 1 || protype == 2)
            {
                 item1 = list.FirstOrDefault(m => m.product.ID == productID && m.Size == size);
            }
            else if(protype == 3) {
                 item1 = list.FirstOrDefault(m => m.product.ID == productID);
            }
         
            int status = 0;
            if (item1 != null)
            {
                
                if (protype == 1)
                {
                    switch (size)
                    {
                        case "S":
                            var SizeS = db.Sizes.Where(m => m.ProductId == productID && m.SizeS >= item1.quantity + quantity);
                            if (SizeS.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity = quantity;
                            }
                            break;
                        case "M":
                            var SizeM = db.Sizes.Where(m => m.ProductId == productID && m.SizeM >= item1.quantity + quantity);
                            if (SizeM.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity = quantity;
                            }
                            break;
                        case "L":
                            var SizeL = db.Sizes.Where(m => m.ProductId == productID && m.SizeL >= item1.quantity + quantity);
                            if (SizeL.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity = quantity;
                            }
                            break;
                        case "XL":
                            var SizeXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXL >= item1.quantity + quantity);
                            if (SizeXL.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity = quantity;
                            }
                            break;
                        case "XXL":
                            var SizeXXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXXL >= item1.quantity + quantity);
                            if (SizeXXL.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity = quantity;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (protype == 2)
                {
                    switch (size)
                    {
                        case "36":
                            var Size_36 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_36 >= item1.quantity + quantity);
                            if (Size_36.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "37":
                            var Size_37 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_37 >= item1.quantity + quantity);
                            if (Size_37.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "38":
                            var Size_38 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_38 >= item1.quantity + quantity);
                            if (Size_38.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "39":
                            var Size_39 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_39 >= item1.quantity + quantity);
                            if (Size_39.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "40":
                            var Size_40 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_40 >= item1.quantity + quantity);
                            if (Size_40.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "41":
                            var Size_41 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_41 >= item1.quantity + quantity);
                            if (Size_41.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "42":
                            var Size_42 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_42 >= item1.quantity + quantity);
                            if (Size_42.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "43":
                            var Size_43 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_43 >= item1.quantity + quantity);
                            if (Size_43.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "44":
                            var Size_44 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_44 >= item1.quantity + quantity);
                            if (Size_44.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        case "45":
                            var Size_45 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_45 >= item1.quantity + quantity);
                            if (Size_45.Count() <= 0)
                            {
                                status = 3;
                            }
                            else
                            {
                                item1.quantity += quantity;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return Json(
                       new
                       {
                           statuss = status
                       }
                       , JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult Additem(long productID, int quantity, string size, int protype)
        {
            int statusCartEmpty = 0;
            var item = new Cart_item();
            Product product = db.Products.Find(productID);
            var cart = Session[SessionCart];
            if (cart != null)
            {
                var list = (List<Cart_item>)cart;

                if (protype != 3 && list.Exists(m => m.product.ID == productID && m.Size == size))
                {
                    int status = 0;
                    foreach (var item1 in list)
                    {
                        if (item1.product.ID == productID && item1.Size == size)
                        {
                            if (protype == 1)
                            {
                                switch (size)
                                {
                                    case "S":
                                        var SizeS = db.Sizes.Where(m => m.ProductId == productID && m.SizeS >= item1.quantity + quantity);
                                        if (SizeS.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "M":
                                        var SizeM = db.Sizes.Where(m => m.ProductId == productID && m.SizeM >= item1.quantity + quantity);
                                        if (SizeM.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "L":
                                        var SizeL = db.Sizes.Where(m => m.ProductId == productID && m.SizeL >= item1.quantity + quantity);
                                        if (SizeL.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "XL":
                                        var SizeXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXL >= item1.quantity + quantity);
                                        if (SizeXL.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "XXL":
                                        var SizeXXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXXL >= item1.quantity + quantity);
                                        if (SizeXXL.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if (protype == 2)
                            {
                                switch (size)
                                {
                                    case "36":
                                        var Size_36 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_36 >= item1.quantity + quantity);
                                        if (Size_36.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "37":
                                        var Size_37 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_37 >= item1.quantity + quantity);
                                        if (Size_37.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "38":
                                        var Size_38 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_38 >= item1.quantity + quantity);
                                        if (Size_38.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "39":
                                        var Size_39 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_39 >= item1.quantity + quantity);
                                        if (Size_39.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "40":
                                        var Size_40 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_40 >= item1.quantity + quantity);
                                        if (Size_40.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "41":
                                        var Size_41 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_41 >= item1.quantity + quantity);
                                        if (Size_41.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "42":
                                        var Size_42 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_42 >= item1.quantity + quantity);
                                        if (Size_42.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "43":
                                        var Size_43 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_43 >= item1.quantity + quantity);
                                        if (Size_43.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "44":
                                        var Size_44 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_44 >= item1.quantity + quantity);
                                        if (Size_44.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    case "45":
                                        var Size_45 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_45 >= item1.quantity + quantity);
                                        if (Size_45.Count() <= 0)
                                        {
                                            status = 3;
                                        }
                                        else
                                        {
                                            item1.quantity += quantity;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                    }
                    var cartCount1 = list.Count();
                    return Json(
                        new
                        {
                            statuss = status,
                            cartCount = cartCount1
                        }
                        , JsonRequestBehavior.AllowGet);

                }
                else if (protype == 3 && list.Exists(m => m.product.ID == productID))
                {
                    int status = 0;
                    foreach (var item1 in list)
                    {
                        if (item1.product.ID == productID)
                        {
                            // Prosess
                            item1.quantity += quantity;
                        }
                    }
                    var cartCount1 = list.Count();
                    return Json(
                        new
                        {
                            statuss = status,
                            cartCount = cartCount1
                        }
                        , JsonRequestBehavior.AllowGet);

                }
                else
                {
                    int status = 0;
                    if (protype == 3)
                    {

                        item.Size = "";
                        item.product = product;
                        item.quantity = quantity;
                        if (quantity > product.number)
                        {
                            return Json(
                                 new
                                 {
                                     statuss = 3,
                                     cartCount = 1
                                 }
                                , JsonRequestBehavior.AllowGet);
                        }
                        list.Add(item);
                        item.countCart = list.Count();

                    }
                    else if (protype == 2)
                    {
                        //swich shoes
                        switch (size)
                        {
                            case "36":
                                var Size_36 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_36 >= quantity);
                                if (Size_36.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "37":
                                var Size_37 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_37 >= quantity);
                                if (Size_37.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "38":
                                var Size_38 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_38 >= quantity);
                                if (Size_38.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "39":
                                var Size_39 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_39 >= quantity);
                                if (Size_39.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "40":
                                var Size_40 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_40 >= quantity);
                                if (Size_40.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "41":
                                var Size_41 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_41 >= quantity);
                                if (Size_41.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "42":
                                var Size_42 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_42 >= quantity);
                                if (Size_42.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "43":
                                var Size_43 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_43 >= quantity);
                                if (Size_43.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "44":
                                var Size_44 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_44 >= quantity);
                                if (Size_44.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "45":
                                var Size_45 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_45 >= quantity);
                                if (Size_45.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {  //swich clothes
                        switch (size)
                        {
                            case "S":
                                var SizeS = db.Sizes.Where(m => m.ProductId == productID && m.SizeS >= quantity);
                                if (SizeS.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();

                                }
                                break;
                            case "M":
                                var SizeM = db.Sizes.Where(m => m.ProductId == productID && m.SizeM >= quantity);
                                if (SizeM.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "L":
                                var SizeL = db.Sizes.Where(m => m.ProductId == productID && m.SizeL >= quantity);
                                if (SizeL.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "XL":
                                var SizeXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXL >= quantity);
                                if (SizeXL.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();
                                }
                                break;
                            case "XXL":
                                var SizeXXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXXL >= quantity);
                                if (SizeXXL.Count() <= 0)
                                {
                                    status = 3;
                                }
                                else
                                {
                                    item.Size = size;
                                    item.product = product;
                                    item.quantity = quantity;
                                    list.Add(item);
                                    item.countCart = list.Count();

                                }
                                break;
                            default:
                                break;
                        }
                    }

                    return Json(
                        new
                        {
                            statuss = status,
                            cartCount = list.Count()
                        }
                        , JsonRequestBehavior.AllowGet); ;
                }
            }
            else
            {
                if (protype == 3)
                {
                    if (quantity > product.number)
                    {
                        return Json(
                           new
                           {
                               statuss = 3,
                               cartCount = 1
                           }
                          , JsonRequestBehavior.AllowGet);
                    }
                    item.Size = size;
                    item.product = product;
                    item.quantity = quantity;
                    item.countCart = 1;
                    item.priceTotal = (int)product.price;
                    var list = new List<Cart_item>();
                    list.Add(item);
                    Session[SessionCart] = list;
                }
                else if (protype == 2)
                {
                    switch (size)
                    {
                        case "36":
                            var Size_36 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_36 >= quantity);
                            if (Size_36.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "37":
                            var Size_37 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_37 >= quantity);
                            if (Size_37.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "38":
                            var Size_38 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_38 >= quantity);
                            if (Size_38.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "39":
                            var Size_39 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_39 >= quantity);
                            if (Size_39.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "40":
                            var Size_40 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_40 >= quantity);
                            if (Size_40.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "41":
                            var Size_41 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_41 >= quantity);
                            if (Size_41.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "42":
                            var Size_42 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_42 >= quantity);
                            if (Size_42.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "43":
                            var Size_43 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_43 >= quantity);
                            if (Size_43.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "44":
                            var Size_44 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_44 >= quantity);
                            if (Size_44.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "45":
                            var Size_45 = db.SizeShoes.Where(m => m.ProductId == productID && m.Size_45 >= quantity);
                            if (Size_45.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (protype == 1)
                {
                    switch (size)
                    {
                        case "S":
                            var SizeS = db.Sizes.Where(m => m.ProductId == productID && m.SizeS >= quantity);
                            if (SizeS.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "M":
                            var SizeM = db.Sizes.Where(m => m.ProductId == productID && m.SizeM >= quantity);
                            if (SizeM.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "L":
                            var SizeL = db.Sizes.Where(m => m.ProductId == productID && m.SizeL >= quantity);
                            if (SizeL.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "XL":
                            var SizeXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXL >= quantity);
                            if (SizeXL.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;
                        case "XXL":
                            var SizeXXL = db.Sizes.Where(m => m.ProductId == productID && m.SizeXXL >= quantity);
                            if (SizeXXL.Count() <= 0)
                            {
                                statusCartEmpty = 3;
                            }
                            else
                            {
                                item.Size = size;
                                item.product = product;
                                item.quantity = quantity;
                                item.countCart = 1;
                                item.priceTotal = (int)product.price;
                                var list = new List<Cart_item>();
                                list.Add(item);
                                Session[SessionCart] = list;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            return Json(
                new
                {
                    statuss = statusCartEmpty,
                    cartCount = 1
                }
               , JsonRequestBehavior.AllowGet);
        }
    }
}
//1119 line
