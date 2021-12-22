using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Common;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class PostController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();

        // GET: Admin/Post
        public ActionResult Index()
        {
            var list = db.Posts.Where(m => m.status > 0).OrderByDescending(m=>m.ID).ToList();
            return View(list);
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.Posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            return View(mpost);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.listTopic = db.Topics.Where(m => m.status == 1 ).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Mpost mpost)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                var namecateDb = db.Topics.Where(m => m.ID == mpost.topid).First();
                string slug = Mystring.ToSlug(mpost.title.ToString());
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/Post/"), namefilenew);
                var folder = Server.MapPath("~/public/images/Post/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                mpost.img = namefilenew;
                mpost.slug = slug;
                mpost.type = "Post";
                mpost.created_at = DateTime.Now;
                mpost.updated_at = DateTime.Now;
                mpost.created_by = int.Parse(Session["Admin_id"].ToString());
                mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Posts.Add(mpost);
                db.SaveChanges();
                //create Link
                link tt_link = new link();
                tt_link.slug = slug;
                tt_link.tableId = 3;
                tt_link.type = "PostDetail";
                tt_link.parentId = mpost.ID;
                db.Link.Add(tt_link);
                db.SaveChanges();
                Message.set_flash("Add success", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.Topics.Where(m => m.status != 0).ToList();
            Message.set_flash("Add failure", "danger");
            return View(mpost);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.Posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = db.Topics.Where(m => m.status != 0).ToList();
            return View(mpost);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //edit
        public ActionResult Edit( Mpost mpost)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                string slug = Mystring.ToSlug(mpost.title.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.Topics.Where(m => m.ID == mpost.topid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images/post"), namefilenew);
                    var folder = Server.MapPath("~/public/images/post/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    mpost.img = namefilenew;
                }
                mpost.slug = slug;
                mpost.updated_at = DateTime.Now;
                mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
                try
                {
                    var thisLink = db.Link.Where(m => m.tableId == 1 && m.parentId == mpost.ID).First();
                    link tt_link = db.Link.Find(thisLink.ID);
                    tt_link.slug = slug;
                    tt_link.tableId = 3;
                    tt_link.parentId = mpost.ID;
                    db.Entry(tt_link).State = EntityState.Modified;
                }
                catch (Exception)
                {
                    //no runing
                }
                db.Entry(mpost).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Edit success", "success");
              
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.Topics.Where(m => m.status != 0).ToList();
            Message.set_flash("Edit failure", "danger");
            return View(mpost);
        }
        //delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.Posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            return View(mpost);
        }
        //status
        public ActionResult Status(int id)
        {
            Mpost mpost = db.Posts.Find(id);
            mpost.status = (mpost.status == 1) ? 2 : 1;
            mpost.updated_at = DateTime.Now;
            mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Posts.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Mpost mpost = db.Posts.Find(id);
            mpost.status = 0;
            mpost.updated_at = DateTime.Now;
            mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete Success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int id)
        {
            Mpost mpost = db.Posts.Find(id);
            mpost.status = 2;
            mpost.updated_at = DateTime.Now;
            mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore Success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Mpost mpost = db.Posts.Find(id);
            try
            {
                var thisLink = db.Link.Where(m => m.tableId == 1 && m.parentId == mpost.ID).First();
                link tt_link1 = db.Link.Find(thisLink.ID);
                db.Link.Remove(tt_link1);
            }
            catch (Exception)
            {
                //no runing
            }
            db.Posts.Remove(mpost);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 Post", "success");
            return RedirectToAction("trash");
        }
    }
}
