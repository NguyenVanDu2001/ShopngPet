using ShopThoiTrang.Common;
using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopThoiTrang.Controllers
{
    public class AuthCusController : Controller
    {
        // GET: AuthCus
        public ActionResult LoginOrRegister()
        {
            return View();
        }
        public void logout()
        {
            CommonConstants.CUSTOMER_SESSION = "";
            Session["id"] = "";
            Session["user"] = "";
            Response.Redirect("~/dang-nhap-dang-ky");
            Message.set_flash("logout success", "success");
        }
        [HttpPost]
        public ActionResult LoginOrRegister(FormCollection fc)
        {
            using (ShopThoiTrangDbContext db = new ShopThoiTrangDbContext())
            {
                string formType = Request.Form["typeName"];
                if (formType.Equals("login"))
                {

                    string Username = fc["uname"];
                    string Pass = fc["psw"];

                    var user_account = db.Users.Where(m => (m.username == Username) && (m.access == 1));

                    if (user_account?.Any() != true)
                    {
                        Message.set_flash("Username is not Exist", "danger");
                        return Redirect("~/dang-nhap-dang-ky");
                    }
                    else
                    {
                        var pass_account = db.Users.Where(m => m.status == 1 && (m.password == Pass) && (m.access == 1));

                        if (pass_account?.Any() != true)
                        {
                            Message.set_flash("Password incorrect", "danger");
                            return Redirect("~/dang-nhap-dang-ky");
                        }
                        else
                        {
                            var muser = pass_account.FirstOrDefault();
                            Session.Add(CommonConstants.CUSTOMER_SESSION, muser);

                            var userLogin = new Userlogin
                            {
                                UserID = muser.ID,
                                AccessName = db.Roles.Where(m => m.parentId == muser.access).FirstOrDefault()?.accessName ?? string.Empty,
                                GroupID = db.Roles.Where(m => m.parentId == muser.access).FirstOrDefault()?.GropID ?? string.Empty,
                                UserName = muser.username,
                                Permissions = UserControlService.GetPermission(muser.ID),
                                Roles = UserControlService.GetRole(muser.ID)
                            };
                            Session.Add(CommonConstants.USER_SESSION, userLogin);
                            Message.set_flash("Login success", "success");
                            return Redirect("~/");

                        }
                    }
                }
                else
                {
                    string uname = fc["uname"];
                    string fname = fc["fname"];

                    string Pass = fc["psw"];
                    string Pass2 = fc["repsw"];
                    if (Pass2 != Pass)
                    {
                        Message.set_flash("Password does not match", "danger");
                        return Redirect("~/");
                    }
                    string email = fc["email"];
                    string address = fc["address"];
                    string phone = fc["phone"];
                    if (ModelState.IsValid)
                    {
                        var muserCheck = db.Users.Where(m => m.status == 1 && m.username == uname && m.access == 1);
                        if (muserCheck?.Any() == true)
                        {
                            Message.set_flash("Username available", "danger");

                            return Redirect("~/");
                        }
                        else
                        {
                            Muser muser = new Muser();
                            muser.img = "defalt.png";
                            muser.password = Pass;
                            muser.username = uname;
                            muser.fullname = fname;
                            muser.email = email;
                            muser.address = address;
                            muser.phone = phone;
                            muser.gender = "nam";
                            muser.access = 1;
                            muser.created_at = DateTime.Now;
                            muser.updated_at = DateTime.Now;
                            muser.created_by = 1;
                            muser.updated_by = 1;
                            muser.status = 1;
                            db.Users.Add(muser);
                            db.SaveChanges();
                            Message.set_flash("register success ", "success");
                            return Redirect("~/dang-nhap-dang-ky");
                        }

                    }
                    Message.set_flash("register failure", "danger");
                    return Redirect("~/dang-nhap-dang-ky");
                }
            }
        }
    }
}