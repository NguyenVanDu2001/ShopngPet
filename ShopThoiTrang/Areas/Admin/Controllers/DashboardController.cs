using ShopThoiTrang.Models;
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

    public class DashboardController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        // GET: Admin/Dashboard

        public ActionResult Index()
        {
            ViewBag.product = db.Products.Count();
            ViewBag.Neworder = db.Orders.Where(m => m.status == 2).Count();
            ViewBag.user = db.Users.Where(m => m.status == 1 && m.access == 1).Count();
            ViewBag.contact = db.Contacts.Count();
            return View();
        }
        //edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.Users.Find(id);
            ViewBag.role = db.Roles.Where(m => m.parentId == muser.access).First();
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("_information", muser);
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
                Message.set_flash("Update success", "success");
                ViewBag.role = db.Roles.Where(m => m.parentId == muser.access).First();
                return View("_information", muser);
            }
            Message.set_flash("Update failure", "danger");
            ViewBag.role = db.Roles.Where(m => m.parentId == muser.access).First();
            return View("Edit", muser);
        }
        public ActionResult CallSessionForLayout()
        {
            if (Session["Admin_id"].Equals(""))
            {
                return View("_userNav");
            }
            else
            {
                ViewBag.adminName = Session["Admin_user"];
                ViewBag.adminID = int.Parse(Session["Admin_id"].ToString());
                ViewBag.adminFull = Session["Admin_fullname"];
                return View("_userNav");
            }
        }
        [CustomAuthorizeAttribute(RoleID = "ACCOUNTANT")]
        public ActionResult statistics()
        {
            //order today
            DateTime dateNow = DateTime.Now;
            string we = dateNow.DayOfWeek.ToString();
            string shortDate = dateNow.ToString("yyyy-MM-dd");
            var Order = db.Orders.Where(m=>m.status ==1);
            ViewBag.OrderToday = 0;
            ViewBag.sumOrder = 0;
            foreach (var item in Order)
            {
                DateTime shortItem = Convert.ToDateTime(item.exportdate);
                string shortItem1 = shortItem.ToString("yyyy-MM-dd");
                if (shortItem1 == shortDate)
                {
                    ViewBag.OrderToday += 1;
                    ViewBag.sumOrder += item.totalSum;
                }
            }
            return View("_Statistical");
        }
        public string CallSessionFullname()
        {
            if (Session["Admin_id"].ToString().Equals(""))
            {
                return "";
            }
            else
            {
                string userFullname = Session["Admin_fullname"].ToString();
                return userFullname;
            }

        }

    }
}