using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Business.Interfaces;
using Transport.ViewModels;
using Transport.Infrastructure;
using Transport.Entity;

namespace Transport.Business.Managers
{
    public class TransportManager : BaseBusiness, ITransportManager
    {

        public bool AddTransport(TransportViewModel model)
        {
            using (IRepository<Transport.Entity.Transport> repository = Factory.GetService<IRepository<Transport.Entity.Transport>>())
            {
                Transport.Entity.Transport transport = new Entity.Transport();
                transport.CarNumber = model.CarNumber;
                transport.CarryingCapacity = new Nullable<double>(model.CarryingCapacity);
                transport.Dimensions = string.Format("{0}x{1}x{2}", model.Length, model.Width, model.Height);
                transport.DriverId = model.Driver.DriverId;
                transport.FuelConsumption = new Nullable<double>(model.FuelConsumption);
                transport.TransportTypeId = model.Type.TypeId;
                transport.Name = model.Name;
                transport.MaxSpeed = model.MaxSpeed;

                repository.Add(transport);
                repository.SaveChanges();
                return true;
            }
        }

        public TransportViewModel GetNewTransport(TransportViewModel model)
        {
            using (IRepository<Driver> driverRepo = Factory.GetService<IRepository<Driver>>())
            using (IRepository<TransportType> typeRepo = Factory.GetService<IRepository<TransportType>>())
            {
                var drivers = driverRepo.GetAll().ToList();
                var types = typeRepo.GetAll().ToList();

                var driverList = new List<DriverViewModel>();
                var typeList = new List<TransportTypeViewModel>();

                foreach(var driver in drivers)
                {
                    driverList.Add(new DriverViewModel() { DriverId = driver.DriverId, DriverName = driver.Name });
                }

                foreach(var type in types)
                {
                    typeList.Add(new TransportTypeViewModel() { TypeId = type.TransportTypeId, Name = type.Name });
                }
                model.Types = typeList;
                model.Drivers = driverList;

                return model;
            }
        }

        public List<TransportViewModel> GetTransports()
        {
            using (IRepository<Entity.Transport> transportRepo = Factory.GetService<IRepository<Entity.Transport>>())
            {
                var transports = transportRepo.GetAll().ToList();
                var result = new List<TransportViewModel>();

                foreach (var transport in transports)
                {
                    TransportViewModel model = new TransportViewModel();
                    model.TransportId = transport.TransportId;
                    model.Type.TypeId = transport.TransportType.TransportTypeId;
                    model.Type.Name = transport.TransportType.Name;
                    model.CarNumber = transport.CarNumber;
                    model.CarryingCapacity = transport.CarryingCapacity.HasValue ? transport.CarryingCapacity.Value : 0;
                    model.Driver.DriverId = transport.Driver.DriverId;
                    model.Driver.DriverName = transport.Driver.Name;
                    var dimesions = transport.Dimensions.Split('x');
                    model.Length = Convert.ToDouble(dimesions[0]);
                    model.Width = Convert.ToDouble(dimesions[1]);
                    model.Height = Convert.ToDouble(dimesions[2]);
                    model.FuelConsumption = transport.FuelConsumption.HasValue ? transport.FuelConsumption.Value : 0;
                    model.Name = transport.Name;

                    result.Add(model);
                }
                return result;
            }
               
        }
    }
}
