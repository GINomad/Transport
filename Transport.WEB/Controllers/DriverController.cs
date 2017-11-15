using System.Web.Mvc;
using Transport.Business.Interfaces;
using Transport.ViewModels;

namespace Transport.WEB.Controllers
{
    public class DriverController : BaseController
    {
        // GET: Driver
        public ActionResult Index()
        {
            var drivers = Factory.GetService<IDriverManager>().GetDrivers();
            return View(drivers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new DriverViewModel());
        }

        [HttpPost]
        public ActionResult Create(DriverViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = Factory.GetService<IDriverManager>().AddDriver(model);

                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Не удалось добавить водителя";
                    return View(model);
                }
            }
        }
    }
}