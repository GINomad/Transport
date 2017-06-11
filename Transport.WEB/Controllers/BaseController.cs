
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Business.Util;

namespace Transport.WEB.Controllers
{
    public abstract class BaseController : Controller
    {
        private Bootstrapper _container;
        public BaseController()
        {
            _container = new Bootstrapper();
        }
        protected Bootstrapper Factory
        {
            get { return _container; }
        }
    }
}