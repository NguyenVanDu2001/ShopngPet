using API_NganLuong;
using ShopThoiTrang.Models;
using ShopThoiTrang.nganluonAPI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopThoiTrang.Controllers
{
    public class CheckoutController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
            Muser sessionUser = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            if (sessionUser == null)
            {
                Session["Redirect"] = "/thanh-toan";
                RouteValueDictionary route = new RouteValueDictionary(new { Controller = "AuthCus", Action = "LoginOrRegister" });
                Message.set_flash("Plaese login", "danger");
                filterContext.Result = new RedirectToRouteResult(route);
                return;
            }
        }

        private const string SessionCart = "SessionCart";
        private ShopThoiTrangDbContext db = new ShopThoiTrangDbContext();
      //  private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            else if (cart == null)
            {
                ViewBag.statusCart = "Cart empty";
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(Morder order)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1, 100000);
            DateTime time = DateTime.Now;
            string orderCode = Mystring.ToStringNospace(numIterations + "" + time);
            string sumOrder = Request["sumOrder"];
            string payment_method = Request["option_payment"];
            float TotalSumOrder = float.Parse(Request["transport_fee"]);
            float sumOrderF = float.Parse(sumOrder);
            // Neu Ship COde
            if (payment_method.Equals("COD"))
            {
                // cap nhat thong tin sau khi dat hang thanh cong

                saveOrder(order, "COD", 3, orderCode, TotalSumOrder, "Giao Hang tiet kiem", sumOrderF);
                var cart = Session[SessionCart];
                var list = new List<Cart_item>();
                ViewBag.cart = (List<Cart_item>)cart;
                ViewBag.statusCod = 1;
                ViewBag.Methodpayment = "COD";
                Session["SessionCart"] = null;
                var listProductOrder = db.Orders.Where(m => m.ID == order.ID).FirstOrDefault();
                return View("oderComplete", listProductOrder);
            }
            //Neu Thanh toan Ngan Luong
            else if (payment_method.Equals("NL"))
            {
                string str_bankcode = Request["bankcode"];
                RequestInfo info = new RequestInfo();
                info.Merchant_id = nganluongInfo.Merchant_id;
                info.Merchant_password = nganluongInfo.Merchant_password;
                info.Receiver_email = nganluongInfo.Receiver_email;
                info.cur_code = "vnd";
                info.bank_code = str_bankcode;
                info.Order_code = orderCode;
                info.Total_amount = sumOrder;
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Payment Ngan Luong for order";
                info.return_url = nganluongInfo.return_url;
                info.cancel_url = nganluongInfo.cancel_url;
                info.Buyer_fullname = order.deliveryname;
                info.Buyer_email = order.deliveryemail;
                info.Buyer_mobile = order.deliveryphone;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
                // neu khong gap loi gi
                if (result.Error_code == "00")
                {
                    saveOrder(order, "Ngan Luong", 2, orderCode, TotalSumOrder, "Giao Hang tiet kiem", sumOrderF);
                    // chuyen sang trang ngan luong
                    return Redirect(result.Checkout_url);
                }
                else
                {
                    ViewBag.errorPaymentOnline = result.Description;
                    return View("payment");
                }

            }
            //Neu Thanh Toán ATM online
            else if (payment_method.Equals("ATM_ONLINE"))
            {
                string str_bankcode = Request["bankcode"];
                RequestInfo info = new RequestInfo();
                info.Merchant_id = nganluongInfo.Merchant_id;
                info.Merchant_password = nganluongInfo.Merchant_password;
                info.Receiver_email = nganluongInfo.Receiver_email;
                info.cur_code = "vnd";
                info.bank_code = str_bankcode;
                info.Order_code = orderCode;
                info.Total_amount = sumOrder;
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Payment Ngan Luong for order";
                info.return_url = nganluongInfo.return_url;
                info.cancel_url = nganluongInfo.cancel_url;
                info.Buyer_fullname = order.deliveryname;
                info.Buyer_email = order.deliveryemail;
                info.Buyer_mobile = order.deliveryphone;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
                // neu khong gap loi gi
                if (result.Error_code == "00")
                {
                    saveOrder(order, "ATM Online qua ngân lượng", 2, orderCode, TotalSumOrder, "Giao Hang tiet kiem", sumOrderF);
                    return Redirect(result.Checkout_url);
                }
                else
                {
                    ViewBag.errorPaymentOnline = result.Description;
                    return View("payment");
                }
            }
            return View("payment");
        }
 
        public ActionResult cancel_order()
        {

            return View("cancel_order");
        }
        //Khi thanh toán Ngan Luong XOng
        public ActionResult confirm_orderPaymentOnline()
        {

            String Token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = nganluongInfo.Merchant_id;
            info.Merchant_password = nganluongInfo.Merchant_password;
            info.Token = Token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                String codeOrder = Session["OrderId"].ToString();
                var OrderInfo = db.Orders.OrderByDescending(m => m.code == codeOrder).FirstOrDefault();
                OrderInfo.StatusPayment = 1;
                db.Entry(OrderInfo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.paymentStatus = OrderInfo.StatusPayment;
                ViewBag.Methodpayment = OrderInfo.deliveryPaymentMethod;
                return View("oderComplete", OrderInfo);
            }
            else
            {
                ViewBag.status = false;
            }

            return View("confirm_orderPaymentOnline");
        }

        //Khi huy thanh toán MOMO
        public ActionResult cancel_order_momo()
        {

            return View("cancel_order");
        }
        //Khi Thanh toám momo xong
        public ActionResult confirm_orderPaymentOnline_momo()
        {

            String errorCode = Request["errorCode"];
            String orderId = Request["orderId"];
            if (errorCode == "0")
            {
                Session["SessionCart"] = null;
                var OrderInfo = db.Orders.Where(m => m.code == orderId).FirstOrDefault();
                OrderInfo.StatusPayment = 1;
                db.Entry(OrderInfo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.paymentStatus = OrderInfo.StatusPayment;
                ViewBag.Methodpayment = OrderInfo.deliveryPaymentMethod;
                return View("oderComplete", OrderInfo);
            }
            else
            {
                ViewBag.status = false;
            }

            return View("confirm_orderPaymentOnline");
        }
        //function ssave order when order success!
        public void saveOrder(Morder order, string paymentMethod, int StatusPayment, string ordercode, float transpost_fee, string transport_type, float totalSum)
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }

            if (ModelState.IsValid)
            {
                order.code = ordercode;
                order.userid = 1;
                order.transport_fee = transpost_fee;
                order.transport_type = transport_type;
                order.totalSum = totalSum;
                order.deliveryPaymentMethod = paymentMethod;
                order.StatusPayment = StatusPayment;
                order.created_ate = DateTime.Now;
                order.updated_by = 1;
                order.updated_at = DateTime.Now;
                order.updated_by = 1;
                order.status = 2;
                order.exportdate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                Session["OrderId"] = ordercode;
                ViewBag.name = order.deliveryname;
                ViewBag.email = order.deliveryemail;
                ViewBag.address = order.deliveryaddress;
                ViewBag.code = order.code;
                ViewBag.phone = order.deliveryphone;
                Mordersdetail orderdetail = new Mordersdetail();

                foreach (var item in list)
                {


                    int price = 0;
                    int sale = (int)item.product.pricesale;
                    if (sale > 0)
                    {
                        price = ((int)item.product.price - (int)item.product.price / 100 * (int)sale) * ((int)item.quantity);
                    }
                    else
                    {
                        price = ((int)item.product.price) * ((int)item.quantity);
                    }
                    orderdetail.orderid = order.ID;
                    orderdetail.productid = item.product.ID;
                    orderdetail.priceSale = (int)item.product.pricesale;
                    orderdetail.price = item.product.price;
                    orderdetail.Size = item.Size;
                    orderdetail.quantity = item.quantity;
                    orderdetail.amount = price;
                    db.Ordersdetails.Add(orderdetail);
                    db.SaveChanges();
                    var updatedProduct = db.Products.Find(item.product.ID);
                    db.Products.Attach(updatedProduct);
                    db.Entry(updatedProduct).State = EntityState.Modified;
                    db.SaveChanges();                 
                }

            }
        }
        //

        public ActionResult productDetailCheckOut(int orderId)
        {
            var order = db.Orders.Where(m=>m.ID == orderId).FirstOrDefault();
            ViewBag.transport_fee = order.transport_fee;
            var list = db.Ordersdetails.Where(m => m.orderid == orderId).ToList();
            return View("_productDetailCheckOut", list);

        }
        public ActionResult orderDetail(int orderId)
        {
            var single = db.Orders.Find(orderId);
            return View("oderComplete", single);
        }

        public ActionResult subnameProduct(int id)
        {
            var list = db.Products.Find(id);
            return View("_subproductOrdersuccess", list);

        }
        public ActionResult formCheckOut()
        {
            //Muser user = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            return View("_formCheckout"/*, user*/);

        }
    }
}