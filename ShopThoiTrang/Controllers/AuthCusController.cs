using ShopThoiTrang.Common;
using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class AuthCusController : Controller
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        // GET: AuthCus
        public ActionResult LoginOrRegister()
        {
            return View("login");
        }
        public ActionResult Forget_pwd()
        {
            return View("reset-pwd");
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
            string formType = Request.Form["typeName"];
            if (formType.Equals("login"))
            {
                string Username = fc["uname"];
                //string Pass = Mystring.ToMD5(fc["psw"]);      
                string Pass =fc["psw"];
                //string PassNoMD5 = fc["psw"];
                var user_account = db.Users.Where(m => (m.username == Username) && (m.access == 1));

                if (user_account.Count() == 0)
                {
                    Message.set_flash("Username is not Exist", "danger");
                    return Redirect("~/dang-nhap-dang-ky");
                }
                else
                {
                    var pass_account = db.Users.Where(m => m.status == 1 && (m.password == Pass) && (m.access == 1));

                    if (pass_account.Count() == 0)
                    {
                        Message.set_flash("Password incorrect", "danger");
                        return Redirect("~/dang-nhap-dang-ky");
                    }
                    else
                    {
                        var muser = user_account.First();
                        Session.Add(CommonConstants.CUSTOMER_SESSION, muser);
                        Message.set_flash("Login success", "success");
                        if (!Session["Redirect"].Equals("")) {
                            string Url = Session["Redirect"].ToString();
                            Session["Redirect"] = "";
                            return Redirect("~" + Url);
                        }
                        else
                        {
                            return Redirect("~/thong-tin-khach-hang");
                            
                        }
                      
                    }
                }

            }
            else
            {
                string uname = fc["uname"];
                string fname = fc["fname"];
                //string Pass = Mystring.ToMD5(fc["psw"]);
                //string Pass2 = Mystring.ToMD5(fc["repsw"]);
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
                    var muserCheck = db.Users.Where(m => m.status == 1 && m.username == uname && m.access == 1).FirstOrDefault();
                    if (muserCheck != null)
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



        //
    }
}