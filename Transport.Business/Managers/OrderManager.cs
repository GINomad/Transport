using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Business.Interfaces;
using Transport.Entity;
using Transport.Infrastructure;
using Transport.Infrastructure.Enums;
using Transport.ViewModels;
using ViewModels;

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
                    TransportId = ChoiceCar(model),
                    StartDate = model.FromDate,
                    EndDate = model.ToDate,
                    FromCity = model.FromCity,
                    ToCity = model.ToCity,
                    FromAddress = model.FromAddress,
                    ToAddress = model.ToAddress,
                    Weight = model.Weight,
                    Length = model.Length,
                    Height = model.Height,
                    Width = model.Width,
                    Name = model.Name
                    
                };

                orderRepository.Add(order);
                orderRepository.SaveChanges();                
                return true;
            }
        }       

        public List<OrderViewModel> GetOrders()
        {
            using (IRepository<Order> orderRepository = Factory.GetService<IRepository<Order>>())
            using (IRepository<City> cityRepository = Factory.GetService<IRepository<City>>())
            {
                var orders = orderRepository.GetAll();
                var list = new List<OrderViewModel>();
                var cities = cityRepository.GetAll().ToList();

                foreach (var order in orders)
                {
                    OrderViewModel model = new OrderViewModel();
                    model.OrderId = order.OrderId;
                    model.FromDate = order.StartDate;
                    model.ToDate = order.EndDate;
                    model.FromCity = order.FromCity;
                    model.ToCity = order.ToCity;
                    model.FromAddress = order.FromAddress;
                    model.ToAddress = order.ToAddress;
                    model.Name = order.Name;
                    model.TransportName = order.Transport.Name;
                    model.FromCityName = cities.FirstOrDefault(x => x.Id == order.FromCity)?.Name;
                    model.ToCityName = cities.FirstOrDefault(x => x.Id == order.ToCity)?.Name;
                    list.Add(model);
                }
                return list;
            }
        }       

        private int ChoiceCar(OrderViewModel model)
        {
            using (IRepository<Transport.Entity.Transport> repository = Factory.GetService<IRepository<Transport.Entity.Transport>>())
            {
                if (model.Weight <= 200)
                {
                    if (model.Height <= 0.75 && model.Length <= 1 && model.Width <= 0.5)
                    {
                        var transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.PassengerCar);
                        if (transport != null)
                        {
                            return transport.TransportId;
                        }
                        else
                        {
                            transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Caravan);
                            if (transport != null)
                            {
                                return transport.TransportId;
                            }
                            else
                            {
                                transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Van);
                                if (transport != null)
                                {
                                    return transport.TransportId;
                                }
                            }
                        }
                        return 0;
                    }
                    else
                    {
                        if (model.Height < 1.5 && model.Length < 4 && model.Width < 3)
                        {
                            var transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Caravan);
                            if (transport != null)
                            {
                                return transport.TransportId;
                            }
                            return 0;
                        }
                        else
                        {
                            var transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Van);
                            if (transport != null)
                            {
                                return transport.TransportId;
                            }
                            return 0;
                        }
                    }
                }
                if (model.Weight > 200 && model.Weight <= 3000)
                {
                    if (model.Height < 1.5 && model.Length < 4 && model.Width < 3)
                    {
                        var transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Caravan);
                        if (transport != null)
                        {
                            return transport.TransportId;
                        }
                        return 0;
                    }
                    else
                    {
                        var transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Van);
                        if (transport != null)
                        {
                            return transport.TransportId;
                        }
                        return 0;
                    }
                }

                if (model.Weight > 3000)
                {
                    var transport = repository.FirstOrDefault(x => x.TransportTypeId == (int)TransportTypes.Van);
                    if (transport != null)
                    {
                        return transport.TransportId;
                    }
                    return 0;
                }
                return 0;
            }
                  
        }

        public IEnumerable<int> BuildPath(int id)
        {
            using (IRepository<Order> orderRepository = Factory.GetService<IRepository<Order>>())
            {
                var order = orderRepository.FirstOrDefault(x => x.OrderId == id);
                if(order != null)
                {
                    var algorithm = Factory.GetService<IDijkstraAlgorithm>();
                    algorithm.Initialize(order.FromCity, order.ToCity);

                    return algorithm.GetPath();
                }

                return null;
            }
        }
    }
}
