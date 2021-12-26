using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Common;
using ShopThoiTrang.Library;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Areas.Admin.Controllers
{

    public class CategoryController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        //index
        public ActionResult Index()
        {
         
            //. chung ta cua hien tai ko con tri enene chay ncbch sdj gdfsa





            //.. chusng ta cux hien tai khoogn cion tre nwha ro 
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0).ToList();
            var list = db.Categorys.Where(m => m.status > 0).ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            
            ViewBag.listCate = db.Categorys.Where(m => m.status !=0 ).ToList();
            return View();
        }
    //create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Mcategory mcategory)
        {       
            if (ModelState.IsValid)
            {
                //category
                string slug = Mystring.ToSlug(mcategory.name.ToString());
                if (db.Categorys.Where(m=>m.slug == slug).Count()>0) {
                    Message.set_flash("The product type already exists in the Category table", "danger");
                    return View(mcategory);
                }
                //topic
                if (db.Products.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("The product type already exists in the Product table", "danger");
                    return View(mcategory);
                }
                mcategory.slug = slug;
                mcategory.created_at = DateTime.Now;
                mcategory.updated_at = DateTime.Now;
                mcategory.created_by = int.Parse(Session["Admin_id"].ToString());
                mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Categorys.Add(mcategory);
                db.SaveChanges();
                //create Link
                link tt_link = new link();
                tt_link.slug = slug;
                tt_link.tableId = 2;
                tt_link.type = "category";
                tt_link.parentId = mcategory.ID;
                db.Link.Add(tt_link);
                db.SaveChanges();
                Message.set_flash("Add success", "success");
                return RedirectToAction("index");
            }
            Message.set_flash("add failure", "danger");
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0).ToList();
            return View(mcategory);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mcategory mcategory = db.Categorys.Find(id);
            if (mcategory == null)
            {
                return HttpNotFound();
            }
            return View(mcategory);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Mcategory mcategory)
        {
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mcategory.name.ToString());
                mcategory.slug = slug;
                mcategory.updated_at = DateTime.Now;
                mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(mcategory).State = EntityState.Modified;
                try
                {
                    var thisLink = db.Link.Where(m => m.tableId == 1 && m.parentId == mcategory.ID).First();
                    link tt_link = db.Link.Find(thisLink.ID);
                    tt_link.slug = slug;
                    tt_link.tableId = 2;
                    tt_link.parentId = mcategory.ID;
                    db.Entry(tt_link).State = EntityState.Modified;
                }
                catch (Exception)
                {
                    //no runing
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Message.set_flash("Edit failure", "success");
            return View(mcategory);
        }

        //status
        public ActionResult Status(int id)
        {
            Mcategory mcategory = db.Categorys.Find(id);
            mcategory.status = (mcategory.status == 1) ? 2 : 1;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change Status success", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Categorys.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Mcategory mcategory = db.Categorys.Find(id);
            mcategory.status =  0;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete success", "success");
            return RedirectToAction("Index");
        }
       
        public ActionResult Retrash(int id)
        {
            Mcategory mcategory = db.Categorys.Find(id);
            mcategory.status = 2;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Mcategory mcategory = db.Categorys.Find(id);
            try
            {
                var thisLink = db.Link.Where(m => m.tableId == 1 && m.parentId == mcategory.ID).First();
                link tt_link1 = db.Link.Find(thisLink.ID);
                db.Link.Remove(tt_link1);
            }
            catch (Exception)
            {
                //no runing
            }
          
            db.Categorys.Remove(mcategory);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 product", "success");
            return RedirectToAction("trash");
        }

    }
}
