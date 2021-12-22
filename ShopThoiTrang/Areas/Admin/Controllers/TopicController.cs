using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Common;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class TopicController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            
            var list = db.Topics.Where(m => m.status !=0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        // GET: Admin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mtopic mtopic = db.Topics.Find(id);
            if (mtopic == null)
            {
                return HttpNotFound();
            }
            return View(mtopic);
        }

        // GET: Admin/Topic/Create
        public ActionResult Create()
        {
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View();
        }
        //create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mtopic mtopic)
        {
            if (ModelState.IsValid)
            {
                //category
                string slug = Mystring.ToSlug(mtopic.name.ToString());
                if (db.Categorys.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("The Topic type already exists in the Category table", "danger");
                    return View(mtopic);
                }
                //topic

                if (db.Topics.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("The product type already exists in the Topic table", "danger");
                    return View(mtopic);
                }
                if (db.Products.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("The product type already exists in the Products table", "danger");
                    return View(mtopic);
                }
                mtopic.slug = slug;
                mtopic.created_at = DateTime.Now;
                mtopic.updated_at = DateTime.Now;
                mtopic.created_by = int.Parse(Session["Admin_id"].ToString());
                mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Topics.Add(mtopic);
                db.SaveChanges();
                link tt_link = new link();
                tt_link.slug = slug;
                tt_link.tableId = 3;
                tt_link.type = "Topic";
                tt_link.parentId = mtopic.ID;
                db.Link.Add(tt_link);
                db.SaveChanges();
                Message.set_flash("Add success", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Add failure", "danger");
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(mtopic);
        }

        // GET: Admin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mtopic mtopic = db.Topics.Find(id);
            if (mtopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(mtopic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Mtopic mtopic)
        {
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mtopic.name.ToString());


                mtopic.updated_at = DateTime.Now;
                mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(mtopic).State = EntityState.Modified;
                try
                {
                    var thisLink = db.Link.Where(m => m.tableId == 1 && m.parentId == mtopic.ID).First();
                    link tt_link = db.Link.Find(thisLink.ID);
                    tt_link.slug = slug;
                    tt_link.tableId = 2;
                    tt_link.parentId = mtopic.ID;
                    db.Entry(tt_link).State = EntityState.Modified;
                }
                catch (Exception)
                {
                    //no runing
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listtopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(mtopic);
        }

        public ActionResult Status(int id)
        {
            Mtopic mtopic = db.Topics.Find(id);
            mtopic.status = (mtopic.status == 1) ? 2 : 1;
            mtopic.updated_at = DateTime.Now;
            mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Topics.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Mtopic mtopic = db.Topics.Find(id);
            mtopic.status = 0;
            mtopic.updated_at = DateTime.Now;
            mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete Success", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Mtopic mtopic = db.Topics.Find(id);
            mtopic.status = 2;
            mtopic.updated_at = DateTime.Now;
            mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore Success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Mtopic mtopic = db.Topics.Find(id);
            db.Topics.Remove(mtopic);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 topic", "success");
            return RedirectToAction("trash");
        }
    }
}
