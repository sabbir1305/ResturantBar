using ResturantBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResturantBar.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.FoodItems.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SubscriberPartial()
        {
            return PartialView();
        }





        [HttpPost]
        public JsonResult Insert(Subscriber subscriber)
        {
            var sub = db.Subscribers.Where(x => x.Email == subscriber.Email).ToList();

            if (subscriber != null && sub.Count==0)
            {
             
                    db.Subscribers.Add(subscriber);
                    db.SaveChanges();
                    return Json(new { success = true });

            }
            else
            {
                return Json(new { success = false });
            }
        }



        [HttpPost]
        public JsonResult SaveOrder(Order order)
        {

            if (order != null )
            {

                db.Orders.Add(order);
                db.SaveChanges();
                return Json(new { success = true });

            }
            else
            {
                return Json(new { success = false });
            }
        }


        public ActionResult Order()
        {
            return PartialView();
        }
    }
}