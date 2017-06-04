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
    public class OrderManager : BaseBusiness, IOrderManager
    {
        public bool AddOrder(OrderViewModel model)
        {
            using (IRepository<Order> orderRepository = Factory.GetService<IRepository<Order>>())
            {
                var order = new Order()
                {
                    TransportId = model.Transport.TransportId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                orderRepository.Add(order);
                orderRepository.SaveChanges();                
                SaveAdress(model, order);
                return true;
            }
        }

        public OrderViewModel GetNewOrder(OrderViewModel order)
        {
            using (IRepository<Entity.Transport> transRepository = Factory.GetService<IRepository<Entity.Transport>>())
            {
                var transports = transRepository.GetAll();
               
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

                    order.Transports.Add(model);
                }
                return order;
            }
                
        }

        public List<OrderViewModel> GetOrders()
        {
            using (IRepository<Order> orderRepository = Factory.GetService<IRepository<Order>>())
            {
                var orders = orderRepository.GetAll();
                var list = new List<OrderViewModel>();

                foreach(var order in orders)
                {
                    OrderViewModel model = new OrderViewModel();
                    model.OrderId = order.OrderId;
                    model.StartDate = order.StartDate;
                    model.EndDate = order.EndDate;
                    model.SenderAdress = new AdressViewModel()
                    {
                        AdressId = order.Adresses.FirstOrDefault(x => x.IsSender).AdressId,
                        AdressName = order.Adresses.FirstOrDefault(x => x.IsSender).Name,
                        IsSender = true
                    };
                    model.ReceiverAdress = new AdressViewModel()
                    {
                        AdressId = order.Adresses.FirstOrDefault(x => !x.IsSender).AdressId,
                        AdressName = order.Adresses.FirstOrDefault(x => !x.IsSender).Name,
                        IsSender = false
                    };
                    model.Transport.Name = order.Transport.Name;
                    model.Transport.TransportId = order.TransportId;
                    list.Add(model);
                }
                return list;
            }
        }

        private void SaveAdress(OrderViewModel model, Order order)
        {
            using (IRepository<Adress> adressRepository = Factory.GetService<IRepository<Adress>>())
            {             
                
                    adressRepository.Add(new Adress()
                    {
                        Name = model.SenderAdress.AdressName,
                        IsSender = model.SenderAdress.IsSender,
                        OrderId = order.OrderId
                    });
                    adressRepository.Add(new Adress()
                    {
                        Name = model.ReceiverAdress.AdressName,
                        IsSender = model.ReceiverAdress.IsSender,
                        OrderId = order.OrderId
                    });
                adressRepository.SaveChanges();                               
            }
        }
    }
}
