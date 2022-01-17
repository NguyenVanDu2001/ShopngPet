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
    public class ContactController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();

        public ActionResult Index()
        {
            var list = db.Contacts.Where(m => m.status > 0).ToList();
            return View(list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mcontact mcontact = db.Contacts.Find(id);
            if (mcontact == null)
            {
                return HttpNotFound();
            }
            return View(mcontact);
        }

  
        public ActionResult Status(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            mcontact.status = (mcontact.status == 1) ? 2 : 1;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Contacts.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        //deltrash
        public ActionResult Deltrash(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            mcontact.status = 0;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete success", "success");
            return RedirectToAction("Index");
        }
        //retrash
        public ActionResult Retrash(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            mcontact.status = 2;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore success", "success");
            return RedirectToAction("trash");
        }
        //deleteTrash
        public ActionResult deleteTrash(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            db.Contacts.Remove(mcontact);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 contact", "success");
            return RedirectToAction("trash");
        }

    }
}
