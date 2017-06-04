using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Business.Util;

namespace Transport.Business
{
    public class BaseBusiness
    {
        private Bootstrapper _factory; 
        public BaseBusiness()
        {
            _factory = new Bootstrapper();
        }
        
        protected Bootstrapper Factory
        {
            get { return _factory; }
        }      
    }
}
