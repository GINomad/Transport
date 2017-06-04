using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Infrastructure;
using Transport.Business.Interfaces;
using Transport.ViewModels;

namespace Transport.WEB.Controllers
{
    public class HomeController : BaseController
    {        
        public ActionResult Index()
        {
            var orders = Factory.GetService<IOrderManager>().GetOrders();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var order = Factory.GetService<IOrderManager>().GetNewOrder(new ViewModels.OrderViewModel());
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            model.SenderAdress.IsSender = true;
            Factory.GetService<IOrderManager>().AddOrder(model);
            return RedirectToAction("Index");
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
    }
}