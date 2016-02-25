using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaypalAssignment.BusinessLogic;

namespace PaypalAssignment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DeveloPaypalEntities context = new DeveloPaypalEntities();
            return View(context.IPNs);
        }
        public ActionResult PayPal()
        {
            Paypal_IPN paypalResponse = new Paypal_IPN("test");

            if (paypalResponse.TXN_ID != null)
            {
                DeveloPaypalEntities context = new DeveloPaypalEntities();
                IPN ipn = new IPN();
                ipn.transactionID = paypalResponse.TXN_ID;
                decimal amount = Convert.ToDecimal(paypalResponse.PaymentGross);
                ipn.amount = amount;
                ipn.buyerEmail = paypalResponse.PayerEmail;
                ipn.txTime = DateTime.Now;
                ipn.custom = paypalResponse.Custom;
                ipn.paymentStatus = paypalResponse.PaymentStatus;
                context.IPNs.Add(ipn);
                context.SaveChanges();
            }
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}