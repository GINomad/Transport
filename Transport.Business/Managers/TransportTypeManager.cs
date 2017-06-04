using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Business.Interfaces;
using Transport.Infrastructure;
using Transport.Entity;
using Transport.ViewModels;

namespace Transport.Business.Managers
{
    public class TransportTypeManager : BaseBusiness, ITransportTypeManager
    {
        public TransportTypeManager(): base()
        {

        }
        public bool CreateType(TransportTypeViewModel model)
        {
            using (IRepository<TransportType> repository = Factory.GetService<IRepository<TransportType>>())
            {
                repository.Add(new TransportType() { Name = model.Name });
                repository.SaveChanges();
            }
            return true;
        }
    }
}
