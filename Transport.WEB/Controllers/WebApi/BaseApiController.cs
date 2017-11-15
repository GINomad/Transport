using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Transport.Business.Util;

namespace Transport.WEB.Controllers.WebApi
{
    public class BaseApiController : ApiController
    {
        private Bootstrapper _container;
        public BaseApiController()
        {
            _container = new Bootstrapper();
        }
        protected Bootstrapper Factory
        {
            get { return _container; }
        }
        protected ResponseMessageResult Success(object data)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, data));
        }
        protected ResponseMessageResult Fail(object data)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));
        }
    }
}
