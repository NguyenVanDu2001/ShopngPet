﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShopThoiTrang.Common;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class UserController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();

        // GET: Admin/User
        public ActionResult Index()
        {
            var list = db.Users.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.Users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View(muser);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            ViewBag.role = db.Roles.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Muser muser, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                string password1 = data["password1"];
                string password2 = data["password2"];
                string username = muser.username;
                var Luser = db.Users.Where(m => m.status == 1 && m.username == username);
                if (password1!=password2) {ViewBag.error = "Password does not match"; }
                if (Luser.Count()>0) { ViewBag.error1 = "Username Incorrect"; }
                else
                {
                    string pass = password1;
                    muser.img = "ádasd";
                    muser.password = pass;
                    muser.address = "";
                    muser.created_at = DateTime.Now;
                    muser.updated_at = DateTime.Now;
                    muser.created_by = int.Parse(Session["Admin_id"].ToString());
                    muser.updated_by = int.Parse(Session["Admin_id"].ToString());
                    db.Users.Add(muser);
                    db.SaveChanges();
                    Message.set_flash("Create user success", "success");
                    return RedirectToAction("Index");
                }
            }
            return View(muser);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.Users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            ViewBag.role = db.Roles.ToList();
            return View(muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Muser muser)
        {
            if (ModelState.IsValid)
            {
                    muser.img = "ádasd";               
                    muser.updated_at = DateTime.Now;
                    muser.updated_by = int.Parse(Session["Admin_id"].ToString());
                    db.Entry(muser).State = EntityState.Modified;
                    db.SaveChanges();
                Message.set_flash("Update success", "success");
                return RedirectToAction("Index");
            }
            return View(muser);
        }

        //status
        public ActionResult Status(int id)
        {
            Muser muser = db.Users.Find(id);
            muser.status = (muser.status == 1) ? 2 : 1;
            muser.updated_at = DateTime.Now;
            muser.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(muser).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Users.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Muser muser = db.Users.Find(id);
            muser.status = 0;
            muser.updated_at = DateTime.Now;
            muser.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(muser).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete Success", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Muser muser = db.Users.Find(id);
            muser.status = 2;
            muser.updated_at = DateTime.Now;
            muser.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(muser).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore Success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Muser muser = db.Users.Find(id);
            db.Users.Remove(muser);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 User", "success");
            return RedirectToAction("trash");
        }

    }
}
