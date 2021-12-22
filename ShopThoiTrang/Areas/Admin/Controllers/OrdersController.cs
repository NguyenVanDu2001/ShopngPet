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
    public class OrdersController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var list = db.Orders.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sigleOrder = db.Orders.Where(m => m.ID == id).First();
            return View("Orderdetail", sigleOrder);
        }
        //status
        public ActionResult Status(int id)
        {
            Morder morder = db.Orders.Find(id);
            morder.status = (morder.status == 1) ? 2 : 1;
            morder.updated_at = DateTime.Now;
            morder.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Orders.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Morder morder = db.Orders.Find(id);
            morder.status = 0;
            morder.updated_at = DateTime.Now;
            morder.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete Success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult productDetailCheckOut(int orderId)
        {
            var list = db.Ordersdetails.Where(m => m.orderid == orderId).ToList();
            return View("_productDetailCheckOut", list);

        }

        public ActionResult subnameProduct(int id)
        {
            var list = db.Products.Find(id);
            return View("_subproductOrdersuccess", list);

        }
        public ActionResult Retrash(int id)
        {
            Morder morder = db.Orders.Find(id);
            morder.status = 2;
            morder.updated_at = DateTime.Now;
            morder.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore Success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Morder morder = db.Orders.Find(id);
            db.Orders.Remove(morder);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 order", "success");
            return RedirectToAction("trash");
        }
    }
}
