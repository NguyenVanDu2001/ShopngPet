using ShopThoiTrang.Common;
using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class CustomerController : BaseCusController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            Muser sessionUser = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            return View("Index", sessionUser);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection fc)
        {
                int  id = int.Parse(fc["id"].ToString());
                if (ModelState.IsValid)
                {
                    Muser muser = db.Users.Find(id);
                    muser.address = fc["address"];
                    muser.fullname = fc["fullname"];
                    muser.phone = fc["phone"];
                    muser.email = fc["email"];
                    muser.updated_at = DateTime.Now;
                    db.Entry(muser).CurrentValues.SetValues(muser);
                    db.SaveChanges();
                    Session[Common.CommonConstants.CUSTOMER_SESSION] = null;
                    Session.Add(CommonConstants.CUSTOMER_SESSION, muser);
                    Message.set_flash("Update succsess", "success");
                    return RedirectToAction("index");
                }
            Message.set_flash("Update failure", "danger");
            return RedirectToAction("index");
        }



    }
}