using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Business.Interfaces;
using Transport.ViewModels;
using ViewModels;

namespace Transport.WEB.Controllers
{
    public class CityController : BaseController
    {
        // GET: City
        [HttpGet]
        public ActionResult Index()
        {
            return View(new CityModel());
        }

        [HttpPost]
        public ActionResult Index(CityModel city)
        {
            if (Factory.GetService<ICityManager>().IsExist(city))
            {
                ViewBag.Error = "Такой город уже существует!";
                return View(city);
            }
            else
            {
                Factory.GetService<ICityManager>().Add(city);
                return RedirectToAction("Index");
            }

        }
    }
}