using alkitaab.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace alkitaab.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            var model = new OrderModel();
            model.strAdultFee = "$15";
            model.strSixteenOverFee = "$15";
            model.strBetweenTexAndSixteenFee = "$10";
            model.strUnderTenFee = "$0";
            model.Tax = 0;
            model.strSubTotal = "$0.00";
            model.strAdultTotal = "$0.00";
            model.strSixteenOverTotal = "$0.00";
            model.strBetweenTexAndSixteenTotal = "$0.00";
            model.strNetTotal = "$0.00";
            model.strUnderTenTotal = "$0.00";
            model.Customer.IsSubscribe = true;
            return View(model);
        }
        [HttpPost]
        public ActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new Context())
                {
                    var cutomer = new Customer();
                    cutomer.FirstName = model.Customer.FirstName;
                    cutomer.LastName = model.Customer.LastName;
                    cutomer.Email = model.Customer.Email;
                    cutomer.Phone = model.Customer.Phone;
                    cutomer.Address = model.Customer.Address;
                    cutomer.City = model.Customer.City;
                    cutomer.PostalCode = model.Customer.PostalCode;
                    cutomer.IsSubscribe = model.Customer.IsSubscribe;
                    cutomer.ReferedBy = model.Customer.ReferedBy;
                    context.Customer.Add(cutomer);
                    context.SaveChanges();
                    var order = new Order();
                    order.CustomerId = cutomer.Id;
                    order.SubTotal = model.SubTotal;
                    order.Tax = model.Tax;
                    order.NetTotal = model.NetTotal;
                    order.SixteenOverQty = model.AdultQty + model.SixteenOverQty;//order.SixteenOverQty = model.SixteenOverQty;
                    order.SixteenOverFee = model.SixteenOverFee;
                    order.SixteenOverTotal = model.SixteenOverTotal;
                    order.BetweenTexAndSixteenQty = model.BetweenTexAndSixteenQty;
                    order.BetweenTexAndSixteenFee = model.BetweenTexAndSixteenFee;
                    order.BetweenTexAndSixteenTotal = model.BetweenTexAndSixteenTotal;
                    order.UnderTenQty = model.UnderTenQty;
                    order.UnderTenFee = model.UnderTenFee;
                    order.UnderTenTotal = model.UnderTenTotal;
                    context.Order.Add(order);
                    context.SaveChanges();

                    Random rn = new Random();
                    int number = rn.Next(100000, 999999);
                    order.OrderCode = order.Id + "A" + number.ToString();
                    context.SaveChanges();

                    int quantity = model.AdultQty + model.SixteenOverQty;
                    int? maxSerial = context.OrderSerial.Max(u => (int?)u.SerialNumber);
                    for (int i = 1; i <= quantity; i++)
                    {
                        var orderSerial = new OrderSerial();
                        orderSerial.OrderId = order.Id;
                        orderSerial.SerialNumber = (maxSerial != null ? (int)maxSerial : 1001) + i;
                        orderSerial.ChildType = "A";
                        context.OrderSerial.Add(orderSerial);
                        context.SaveChanges();
                    }
                    quantity = model.BetweenTexAndSixteenQty;
                    maxSerial = context.OrderSerial.Max(u => (int?)u.SerialNumber);
                    for (int i = 1; i <= quantity; i++)
                    {
                        var orderSerial = new OrderSerial();
                        orderSerial.OrderId = order.Id;
                        orderSerial.SerialNumber = (maxSerial != null ? (int)maxSerial : 1001) + i;
                        orderSerial.ChildType = "C1";
                        context.OrderSerial.Add(orderSerial);
                        context.SaveChanges();
                    }
                    quantity = model.UnderTenQty;
                    maxSerial = context.OrderSerial.Max(u => (int?)u.SerialNumber);
                    for (int i = 1; i <= quantity; i++)
                    {
                        var orderSerial = new OrderSerial();
                        orderSerial.OrderId = order.Id;
                        orderSerial.SerialNumber = (maxSerial != null ? (int)maxSerial : 1001) + i;
                        orderSerial.ChildType = "T";
                        context.OrderSerial.Add(orderSerial);
                        context.SaveChanges();
                    }
                    quantity = model.AdultQty + model.SixteenOverQty + model.BetweenTexAndSixteenQty + model.UnderTenQty;
                    var orderSerials = context.OrderSerial.Where(x => x.OrderId == order.Id).ToList();
                    RedirectToPayment(model.NetTotal.ToString(), order.OrderCode.ToString(), quantity.ToString());
                    return View(model); //return RedirectToAction("PaymentThanks");
                }
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        //DoSomethingWith(error);
                    }
                }
                ModelState.AddModelError("", "Error in adding order!");
            }
            return View(model);
        }
        public static String GetContextFromHTML(Hashtable paramLst, string Path)
        {
            string context = "";
            using (StreamReader sr = new StreamReader(Path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    context += line;
                }
            }
            if (context.Length > 0)
            {
                foreach (DictionaryEntry key in paramLst)
                {
                    context = context.Replace(key.Key.ToString(), Convert.ToString(key.Value));
                }
            }
            return context;
        }
        public void RedirectToPayment(string amount, string orderCode, string quantity)
        {
            string decAmount = amount;
            string returnUrl = "http://localhost:2147/order/paymentthanks";
            string cancelUrl = "http://localhost:2147/order/checkout";
            string currencyCode = "USD";
            string custom = "Alkitab";
            string BusinessCode = "X87PQWL29QQR6";
            ////.description = "Description about the payment amount.";

            string Paypal_URL = "https://www.paypal.com/cgi-bin/webscr?" +
                "cmd=_xclick" +
                "&business=" + BusinessCode +
                "&item_number=" + orderCode +
                "&amount=" + decAmount +
                "&return=" + returnUrl +
                "&cancel_return=" + cancelUrl +
                "&paypal_currency_code=" + currencyCode +
                "&currency_code=" + currencyCode +
                "&custom=" + custom +
                "&quantity=1";//"&quantity=" + quantity;
            Response.Redirect(Paypal_URL);
        }
        public void PaymentMethod()
        {
            string status = Request.QueryString["st"];
            string paymentDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000");
            string referenceNumber = Request.QueryString["tx"];
            string orderCode = Request.QueryString["item_number"];
            string paymentType = Request.QueryString["cc"];
            string amount = Request.QueryString["amt"];
            int orderId = int.Parse(orderCode.Split('A')[0]);
            Session["ORDERCODE"] = orderCode;

            var context = new Context();
            var paymentTransaction = new PaymentTransaction();
            paymentTransaction.OrderID = orderId;
            paymentTransaction.PaymentDate = paymentDate;
            paymentTransaction.ReferenceNummber = referenceNumber;
            paymentTransaction.Status = status;
            context.PaymentTransaction.Add(paymentTransaction);
            context.SaveChanges();

            var order = context.Order.Where(x => x.Id == orderId).FirstOrDefault();
            var customer = context.Customer.Where(x => x.Id == order.CustomerId).FirstOrDefault();
            var orderSerial = context.OrderSerial.Where(x => x.OrderId == order.Id).ToList();
            OrderEmail(order, customer, orderSerial);
        }
        private void OrderEmail(Order model, Customer cutomer, List<OrderSerial> orderSerials)
        {
            string serialNumbers = "<strong>";
            foreach (var os in orderSerials)
                serialNumbers += os.SerialNumber + " (" + os.ChildType + ")<br />";
            serialNumbers += "</strong>";
            Hashtable ht = new Hashtable();
            ht.Add("FIRSTNAME", cutomer.FirstName);
            ht.Add("LASTNAME", cutomer.LastName);
            ht.Add("EMAIL", cutomer.Email);
            ht.Add("PHONE", cutomer.Phone);
            //ht.Add("ADDRESS", cutomer.Address);
            //ht.Add("CITY", cutomer.City);
            //ht.Add("POSTALCODE", cutomer.PostalCode);
            //ht.Add("ISSUBSCRIBE", cutomer.IsSubscribe ? "YES" : "NO");
            ht.Add("REFERENCE", cutomer.ReferedBy);
            ht.Add("ADULTSCHILD", model.SixteenOverQty);
            ht.Add("CHILD610", model.BetweenTexAndSixteenQty);
            ht.Add("CHILD5", model.UnderTenQty);
            ht.Add("TOTAL", "$" + model.NetTotal);
            ht.Add("SERIALNO", serialNumbers);
            try
            {
                String EmailBody = GetContextFromHTML(ht, Server.MapPath("~/app_themes/vena/emailtemplate/register.html"));
                SendEmail(EmailBody, cutomer.Email, "Alkitab: Successfully registered");
            }
            catch { }
        }
        public ActionResult PaymentThanks()
        {
            try
            {
                PaymentMethod();
            }
            catch { }
            return View();
        }
        private string SendEmail(string body, string to, string subject)
        {
            var from = "";
            MailMessage msg = new MailMessage();
            string[] ToEmailList = to.Split(',');

            subject = subject.Replace('\r', ' ').Replace('\n', ' ');

            msg.From = new MailAddress(from);
            //msg.CC.Add(from);      
            foreach (var toemail in ToEmailList)
            {
                msg.To.Add(new MailAddress(toemail));
            }
            //msg.CC.Add("info@alkitabacademy.com");
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            using (SmtpClient client = new SmtpClient())
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("", "");
                client.Host = "smtp.gmail.com"; //"smtp.gmail.com";
                client.Port = 587;//25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Send(msg);
                return "Success";
            }

        }
    }
}