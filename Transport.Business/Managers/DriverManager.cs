using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Business.Interfaces;
using Transport.Entity;
using Transport.Infrastructure;
using Transport.ViewModels;

namespace Transport.Business.Managers
{
    public class DriverManager : BaseBusiness, IDriverManager
    {
        public bool AddDriver(DriverViewModel model)
        {
            try
            {
                using (IRepository<Driver> repository = Factory.GetService<IRepository<Driver>>())
                {
                    repository.Add(new Driver() { Name = model.DriverName });
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public List<DriverViewModel> GetDrivers()
        {
            using (IRepository<Driver> repository = Factory.GetService<IRepository<Driver>>())
            {
                var list = new List<DriverViewModel>();
                var drivers = repository.GetAll();

                foreach(var driver in drivers)
                {
                    list.Add(new DriverViewModel() { DriverId = driver.DriverId, DriverName = driver.Name });
                }
                return list;
            }
        }
    }
}
