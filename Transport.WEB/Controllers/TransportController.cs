using System.Web.Mvc;
using Transport.Business.Interfaces;
using Transport.ViewModels;

namespace Transport.WEB.Controllers
{
    public class TransportController : BaseController
    {
        // GET: Transport
        public ActionResult Index()
        {
            var result = Factory.GetService<ITransportManager>().GetTransports();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = Factory.GetService<ITransportManager>().GetNewTransport(new ViewModels.TransportViewModel());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ViewModels.TransportViewModel model)
        {
           
                //return View(Factory.GetService<ITransportManager>().GetNewTransport(model));            
            //else
            //{
                Factory.GetService<ITransportManager>().AddTransport(model);
            //}
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateTransportType()
        {
            return View(new TransportTypeViewModel());
        }

        [HttpPost]
        public ActionResult CreateTransportType(TransportTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Factory.GetService<ITransportTypeManager>().CreateType(model);
                return RedirectToAction("Index");
            }
        }
    }
}