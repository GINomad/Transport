using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transport.Business.Interfaces;
using Transport.ViewModels;

namespace Transport.WEB.Controllers.WebApi
{
    public class OrderController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult CreateOrder([FromBody] OrderViewModel model)
        {
            Factory.GetService<IOrderManager>().AddOrder(model);
            return Ok(true);
        }

        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            var orders = Factory.GetService<IOrderManager>().GetOrders();
            return Ok(orders);
        }
    }
}
