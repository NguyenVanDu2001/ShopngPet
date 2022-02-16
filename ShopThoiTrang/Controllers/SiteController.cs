using PagedList;
using ShopThoiTrang.Common;
using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    [CustomAuthorize]
    public class SiteController : Controller
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        // GET: Site
        public ActionResult Index(String slug = "")
        {
            int page = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = int.Parse(Request.QueryString["page"].ToString());
            }

            if (slug == "")
            {

                return this.home();
            }
            else
            {
                var rowlink = db.Link.Where(m => m.slug == slug);
                if (rowlink.Count() > 0)
                {
                    var link = rowlink.First();
                    if (link.type == "category" && link.tableId == 2)
                    {

                        return this.productOfCategory(slug, page);
                    }
                    else if (link.type == "Topic" && link.tableId == 3)
                    {
                        return this.PostOfTopic(slug,page);
                    }
                }
                else
                {
                    var post = db.Posts.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
                    if (post != null)
                    {
                        return this.postDetail(slug);
                    }
                    else
                    {
                        var product = db.Products.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
                        if (product != null)
                        {
                            return this.ProductDetail(slug);
                        }
                        else
                        {
                            return this.page404();
                        }
                    }
                }
                return this.page404();
            }
        }
        // Post
        public ActionResult getAllPost(int ? page)
        {
            ViewBag.url = "bai-viet";
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var list = db.Posts.Where(m => m.status == 1).OrderByDescending(m => m.ID).ToList();
            return View("getAllPost", list.ToPagedList(pageNumber, pageSize));
        }
        private ActionResult PostOfTopic(string slug,int? page)
        {
           
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.post = db.Posts.Where(m => m.status == 1).OrderByDescending(m => m.ID).ToList();
            var sigleTopic = db.Topics.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
            ViewBag.url = sigleTopic.slug+"";
            var ListPost = db.Posts.Where(m => m.status == 1 && m.topid == sigleTopic.ID).ToList();
            return View("getAllPost", ListPost.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult postDetail(string slug)
        {
            ViewBag.post = db.Posts.Where(m => m.status == 1).OrderByDescending(m => m.ID).ToList();
            var siglePost = db.Posts.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
            return View("postDetail", siglePost);
        }

        //product
        public ActionResult ProductMen( int? page)
        {
            ViewBag.url = "product";
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1); 
            ViewBag.categoryName = "product nam";
            var ListMen = db.Products.Where(m => m.status == 1 && (m.ProductType == "nam" || m.ProductType == "CaHai") ).ToList();
            ViewBag.totalPage = ListMen.Count();
            return View("productOfCategory", ListMen.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductWomen(int? page)
        {
            ViewBag.url = "product";
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.categoryName = "product nữ";
            var ListWomen = db.Products.Where(m => m.status == 1 && (m.ProductType == "nu" || m.ProductType == "CaHai")).ToList();
            ViewBag.totalPage = ListWomen.Count();
            return View("productOfCategory", ListWomen.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult AllProduct(string slug, int? page)
        {
            ViewBag.url = "product";
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var list = db.Products
                .Where(m => m.status == 1).OrderByDescending(m=>m.ID)
                 .ToList();
            ViewBag.totalPage = list.Count();
            return View("productOfCategory", list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult productOfCategory(string slug, int? page)
        {
           
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            Mcategory single = db.Categorys.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
            ViewBag.url = single.slug+"";
            ViewBag.categoryName = single.name;
            var list = db.Products
                .Where(m => m.status == 1 && m.catid == single.ID)
                 .ToList();
            ViewBag.totalPage = list.Count();
            return View("productOfCategory", list.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Sreach(string keyW,int? page)
        {
          
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.categoryName = "Tìm kiếm : " + keyW;
            ViewBag.url = "tim-kiem-san-pham?keyW="+keyW+"&";
            var list = db.Products.Where(m => m.status == 1 && m.name.Contains(keyW)).ToList();
            return View("productOfCategory", list.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult ProductDetail(string slug)
        {
            var sigleProduct = db.Products.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
            if (sigleProduct == null)
            { return View("page404"); }
            ViewBag.size = db.Sizes.Where(m => m.ProductId == sigleProduct.ID).FirstOrDefault();
            ViewBag.sizeShoe = db.SizeShoes.Where(m => m.ProductId == sigleProduct.ID).FirstOrDefault();
            return View("ProductDetail", sigleProduct);
        }

        public ViewResult contact()
        {
            return View("contact");
        }
        [HttpPost]
        public ActionResult contact(Mcontact contact)
        {
            if (ModelState.IsValid)
            {
                contact.created_at = DateTime.Now;
                contact.updated_by = 1;
                contact.updated_at = DateTime.Now;
                contact.updated_by = 1;
                contact.status = 2;
                db.Contacts.Add(contact);
                db.SaveChanges();
                Message.set_flash("Send contact success", "success");
                return View("contact");
            }
            Message.set_flash("Send contact failure", "danger");
            return View("contact");

        }

        public ActionResult page404()
        {
            return View("page404");
        }

        public ActionResult home()
        {
            Session["Redirect"] = "/thong-tin-khach-hang";
            return View("home");
        }
        //
    }
}