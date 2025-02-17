﻿using ShopThoiTrang.Models;
using ShopThoiTrang.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        public ActionResult login()
        {
            return View("_login");
        }
        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            String Username = fc["username"];
            string Pass =fc["password"];
            var user_account = db.Users.Where(m => m.access != 1 && m.status == 1 && (m.username.ToLower() == Username.ToLower()));
            var userC = db.Users.Where(m => m.username == Username && m.access == 1);
            if (userC.Count() != 0)
            {
                ViewBag.error = "you do not have permission to login";
            }
            else
            {
                if (user_account.Count() == 0)
                {
                    ViewBag.error = "Username Incorrect";
                }
                else
                {
                    var passWord = fc["password"].ToLower();
                    var pass_account = db.Users.Where(m => m.access != 1 && m.status == 1 && m.password.ToLower() == passWord);
                    if (pass_account.Count() == 0)
                    {
                        ViewBag.error = "Password Incorrect";
                    }
                    else
                    {
                        var user = user_account.First();
                        role role = db.Roles.Where(m => m.parentId == user.access).First();
                        var userSession = new Userlogin();
                        userSession.UserName = user.username;
                        userSession.UserID = user.ID;
                        userSession.GroupID = role.GropID;
                        userSession.AccessName = role.accessName;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        var i = Session["SESSION_CREDENTIALS"];
                        Session["Admin_id"] = user.ID;
                        Session["Admin_user"] = user.username;
                        Session["Admin_fullname"] = user.fullname;
                        Response.Redirect("~/Admin");
                    }
                }
            }
            ViewBag.sess = Session["Admin_id"];
            return View("_login");

        }
        //logout
        public ActionResult logout()
        {
            Session["Admin_id"] = "";
            Session["Admin_user"] = "";
            Response.Redirect("~/Admin");
            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Muser muser)
        {
            if (ModelState.IsValid)
            {
                muser.img = "ádasd";
                muser.access = 0;
                muser.created_at = DateTime.Now;
                muser.updated_at = DateTime.Now;
                muser.created_by = int.Parse(Session["Admin_id"].ToString());
                muser.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(muser).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Update Success", "success");
                ViewBag.role = db.Roles.Where(m => m.parentId == muser.access).First();
                return View("_information", muser);
            }
            Message.set_flash("Update failure", "danger");
            ViewBag.role = db.Roles.Where(m => m.parentId == muser.access).First();
            return View("Edit", muser);
        }

    }
}