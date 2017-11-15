using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Infrastructure;
using Transport.Business.Interfaces;
using Transport.ViewModels;
using ViewModels;

namespace Transport.WEB.Controllers
{
    public class HomeController : BaseController
    {        
        public ActionResult Index()
        {
            /*var algorithm = Factory.GetService<IDijkstraAlgorithm>();
            algorithm.Initialize(1, 9);
            var result = algorithm.GetPath();
            if(result != null)
            {
                ViewBag.Test = result;
            }*/
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            //model.SenderAdress.IsSender = true;
            Factory.GetService<IOrderManager>().AddOrder(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var details = Factory.GetService<IOrderManager>().GetOrder(id);
            return View(details);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}