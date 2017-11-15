using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transport.Business.Interfaces;
using Transport.ViewModels;
using ViewModels;

namespace Transport.WEB.Controllers.WebApi
{
    [RoutePrefix("api/city")]
    public class CityController : BaseApiController
    {
        [HttpGet]
        [ActionName("Cities")]
        public IHttpActionResult GetCityList()
        {
            return Ok(Factory.GetService<ICityManager>().GetCities());
        }

        [HttpGet]
        [ActionName("CheckDistance")]
        public IHttpActionResult CheckDistance(int from, int to)
        {
            var result = Factory.GetService<ICityManager>().CheckDistance(from, to);
            return result ? Ok(true) : Ok(false);
        }


        [HttpPost]
        [ActionName("SetDistance")]
        public IHttpActionResult SetDistance([FromBody] CityDistanceViewModel model)
        {
            var result = Factory.GetService<ICityManager>().SetDistance(model);
            return result ? Ok(true) : Ok(false);
        }
    }
}
