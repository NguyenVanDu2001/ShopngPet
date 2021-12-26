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
  //  [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
    public class ProductsController : BaseController
    {
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
        // GET: Admin/Products
        //index
        public ActionResult Index()
        {
            //chusng ta cux 
            var list = db.Products.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }




        // GET: Admin/Products/Create

       //create
        public ActionResult Create()
        {

            ViewBag.listCate = db.Categorys.Where(m => m.status != 0).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase file, FormCollection fc)
        {
            string type = fc["typePro"];
            int SizeS = int.Parse(fc["SizeS"]);
            int SizeM = int.Parse(fc["SizeM"]);
            int SizeL = int.Parse(fc["SizeL"]);
            int SizeXL = int.Parse(fc["SizeXL"]);
            int SizeXXL = int.Parse(fc["SizeXXL"]);

            int Size_36 = int.Parse(fc["Size_36"]);
            int Size_37 = int.Parse(fc["Size_37"]);
            int Size_38 = int.Parse(fc["Size_38"]);
            int Size_39 = int.Parse(fc["Size_39"]);
            int Size_40 = int.Parse(fc["Size_40"]);
            int Size_41 = int.Parse(fc["Size_41"]);
            int Size_42 = int.Parse(fc["Size_42"]);
            int Size_43 = int.Parse(fc["Size_43"]);
            int Size_44 = int.Parse(fc["Size_44"]);
            int Size_45 = int.Parse(fc["Size_45"]);

            int numberProduct = 0;
            int typePro = 0;
            if (type.Equals("fashion"))
            {
                typePro = 1;
               
                numberProduct = SizeS + SizeM + SizeL + SizeXL + SizeXXL;
            }
            else if (type.Equals("shoe"))
            {
                typePro = 2;
                
                numberProduct = Size_36 + Size_37 + Size_38 + Size_39 + Size_40 +
                                Size_41 + Size_42 + Size_43 + Size_44 + Size_45;
            }
            else if (type.Equals("accessories"))
            {
                typePro = 3;
                numberProduct = (int)product.number;
            }

            product.slug = "temp";
            product.type = typePro;
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0).ToList();
            if (product != null && ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(product.name.ToString());
                if (db.Categorys.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("product đã tồn tại trong bảng Category", "danger");
                    return View(product);
                }
                if (db.Products.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash(" product đã tồn tại trong bảng Product", "danger");
                    return View(product);
                }
                var namecateDb = db.Categorys.Where(m => m.ID == product.catid).First();
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/product"), namefilenew);
                var folder = Server.MapPath("~/public/images/product/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                HttpPostedFileBase fileimgBehind = Request.Files["imgBehind"];
                string filenameBehind = fileimgBehind.FileName.ToString();
                string namefileBehind = "NoImage";
                if (filenameBehind.Equals(""))
                {
                    //no prosess the image can null
                }
                else
                {
                    string ExtensionFileBehind = Mystring.GetFileExtension(filenameBehind);
                    namefileBehind = namecate + "/" + slug + "Behind." + ExtensionFileBehind;
                    var path1 = Path.Combine(Server.MapPath("~/public/images/product"), namefileBehind);
                    fileimgBehind.SaveAs(path1);
                }
                file.SaveAs(path);
                product.img = namefilenew;
                product.slug = slug;
                product.number = numberProduct;
                product.created_at = DateTime.Now;
                product.sold = 0;
                product.imgBehind = namefileBehind;
                product.created_by = int.Parse(Session["Admin_id"].ToString());
                db.Products.Add(product);
                db.SaveChanges();
                if (type.Equals("fashion"))
                {
                    Size size = new Size();
                    size.SizeS = SizeS;
                    size.SizeS = SizeM;
                    size.SizeS = SizeL;
                    size.SizeS = SizeXL;
                    size.SizeS = SizeXXL;
                    size.ProductId = product.ID;
                    db.Sizes.Add(size);

                }
                else if (type.Equals("shoe"))
                {
                    SizeShoe shoeSize = new SizeShoe();
                    shoeSize.Size_36 = Size_36;
                    shoeSize.Size_37 = Size_37;
                    shoeSize.Size_38 = Size_38;
                    shoeSize.Size_39 = Size_39;
                    shoeSize.Size_40 = Size_40;
                    shoeSize.Size_41 = Size_41;
                    shoeSize.Size_42 = Size_42;
                    shoeSize.Size_43 = Size_43;
                    shoeSize.Size_44 = Size_44;
                    shoeSize.Size_45 = Size_45;
                    shoeSize.ProductId = product.ID;
                    db.SizeShoes.Add(shoeSize);
                }
                else if (type.Equals("accessories"))
                {
                    // no prosess
                }
                db.SaveChanges();
                Message.set_flash("Add success", "success");
                return RedirectToAction("index");
            }
            Message.set_flash("Add failure", "danger");
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0).ToList();

            ViewBag.size = db.Sizes.Where(m => m.ProductId == id).FirstOrDefault();
            ViewBag.ShoeSize = db.SizeShoes.Where(m => m.ProductId == id).FirstOrDefault();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, HttpPostedFileBase file, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                int numberProduct = 0;
               
                if (product.type == 1)
                {
                    int SizeS = int.Parse(fc["SizeS"]);
                    int SizeM = int.Parse(fc["SizeM"]);
                    int SizeL = int.Parse(fc["SizeL"]);
                    int SizeXL = int.Parse(fc["SizeXL"]);
                    int SizeXXL = int.Parse(fc["SizeXXL"]);

                    numberProduct = SizeS + SizeM + SizeL + SizeXL + SizeXXL;
                    product.type = 1;
                }
                else if (product.type == 2)
                {
                    int Size_36 = int.Parse(fc["Size_36"]);
                    int Size_37 = int.Parse(fc["Size_37"]);
                    int Size_38 = int.Parse(fc["Size_38"]);
                    int Size_39 = int.Parse(fc["Size_39"]);
                    int Size_40 = int.Parse(fc["Size_40"]);
                    int Size_41 = int.Parse(fc["Size_41"]);
                    int Size_42 = int.Parse(fc["Size_42"]);
                    int Size_43 = int.Parse(fc["Size_43"]);
                    int Size_44 = int.Parse(fc["Size_44"]);
                    int Size_45 = int.Parse(fc["Size_45"]);
                    numberProduct = Size_36 + Size_37 + Size_38 + Size_39 + Size_40 +
                                    Size_41 + Size_42 + Size_43 + Size_44 + Size_45;
                    product.type = 2;
                }
                else if (product.type == 3)
                {
                    numberProduct = (int)product.number;
                    product.type = 3;
                }

                string slug = Mystring.ToSlug(product.name.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();

                if (!filename.Equals(""))
                {
                    var namecateDb = db.Categorys.Where(m => m.ID == product.catid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images/product"), namefilenew);
                    var folder = Server.MapPath("~/public/images/product/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var folderdeleteImg = Server.MapPath("~/public/images/product/" + product.img);
                    if (Directory.Exists(folderdeleteImg))
                    {
                        Directory.Delete(folderdeleteImg);
                    }
                    file.SaveAs(path);
                    product.img = namefilenew;
                }
                HttpPostedFileBase fileimgBehind = Request.Files["imgBehind"];
                string filenameBehind = fileimgBehind.FileName.ToString();
                if (filenameBehind.Equals(""))
                {
                    //no prosess
                }
                else
                {
                    var namecateDb = db.Categorys.Where(m => m.ID == product.catid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefileBehind = namecate + "/" + slug + "Behind." + ExtensionFile;
                    var path1 = Path.Combine(Server.MapPath("~/public/images/product"), namefileBehind);
                    var folder1 = Server.MapPath("~/public/images/product/" + namecate);
                    if (!Directory.Exists(folder1))
                    {
                        Directory.CreateDirectory(folder1);
                    }
                    var folderdeleteBehind = Server.MapPath("~/public/images/product/" + product.imgBehind);
                    if (Directory.Exists(folderdeleteBehind))
                    {
                        Directory.Delete(folder1);
                    }
                    fileimgBehind.SaveAs(path1);
                    product.imgBehind = namefileBehind;
                }
                product.slug = slug;
                product.number = numberProduct;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                if (product.type == 1)
                {
                    int SizeS = int.Parse(fc["SizeS"]);
                    int SizeM = int.Parse(fc["SizeM"]);
                    int SizeL = int.Parse(fc["SizeL"]);
                    int SizeXL = int.Parse(fc["SizeXL"]);
                    int SizeXXL = int.Parse(fc["SizeXXL"]);

                    var size = db.Sizes.Where(m => m.ProductId == product.ID).FirstOrDefault();
                    size.SizeS = SizeS;
                    size.SizeM = SizeM;
                    size.SizeL = SizeL;
                    size.SizeXL = SizeXL;
                    size.SizeXXL = SizeXXL;
                    db.Entry(size).State = EntityState.Modified;
                }
                else if(product.type == 2)
                {
                    int Size_36 = int.Parse(fc["Size_36"]);
                    int Size_37 = int.Parse(fc["Size_37"]);
                    int Size_38 = int.Parse(fc["Size_38"]);
                    int Size_39 = int.Parse(fc["Size_39"]);
                    int Size_40 = int.Parse(fc["Size_40"]);
                    int Size_41 = int.Parse(fc["Size_41"]);
                    int Size_42 = int.Parse(fc["Size_42"]);
                    int Size_43 = int.Parse(fc["Size_43"]);
                    int Size_44 = int.Parse(fc["Size_44"]);
                    int Size_45 = int.Parse(fc["Size_45"]);
                    var shoeSize = db.SizeShoes.Where(m => m.ProductId == product.ID).FirstOrDefault();
                    shoeSize.Size_36 = Size_36;
                    shoeSize.Size_37 = Size_37;
                    shoeSize.Size_38 = Size_38;
                    shoeSize.Size_39 = Size_39;
                    shoeSize.Size_40 = Size_40;
                    shoeSize.Size_41 = Size_41;
                    shoeSize.Size_42 = Size_42;
                    shoeSize.Size_43 = Size_43;
                    shoeSize.Size_44 = Size_44;
                    shoeSize.Size_45 = Size_45;
                    shoeSize.ProductId = product.ID;
                    db.Entry(shoeSize).State = EntityState.Modified;
                }
                else
                {
                    //no Prosess
                }
                db.SaveChanges();
                ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID > 2).ToList();
                Message.set_flash("Edit success", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Edit failure", "danger");
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID > 2).ToList();
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Status(int id)
        {
            Product product = db.Products.Find(id);
            product.status = (product.status == 1) ? 2 : 1;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult trash()
        {
            var list = db.Products.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Product product = db.Products.Find(id);
            product.status = 0;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete Success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int id)
        {
            Product product = db.Products.Find(id);
            product.status = 2;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Restore Success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Product product = db.Products.Find(id);
            try
            {
                var thisLink = db.Link.Where(m => m.tableId == 1 && m.parentId == product.ID).First();
                link tt_link1 = db.Link.Find(thisLink.ID);
                db.Link.Remove(tt_link1);
            }
            catch (Exception)
            {
                //no runing
            }
            db.Products.Remove(product);
            db.SaveChanges();
            Message.set_flash("Permanently deleted 1 product", "success");
            return RedirectToAction("trash");
        }

    }
}
