using System;
using System.Collections.Generic;
using System.Linq;
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

        public DetailsViewModel GetOrder(int id)
        {
            using (IRepository<Order> orderRepository = Factory.GetService<IRepository<Order>>())
            {
                var order = orderRepository.FirstOrDefault(x => x.OrderId == id);
                if(order != null)
                {
                    double totalDistance = default(double);

                    DetailsViewModel model = new DetailsViewModel
                    {
                        Cities = BuildPath(order, out totalDistance).ToList(),
                        TotalDistance = totalDistance,
                        TransportName = order.Transport.Name,
                        From = order.FromAddress,
                        To = order.ToAddress,
                        EstimatedTime = GetEstimatedTime(order.Transport.CarryingCapacity, order.Weight, order.Transport.MaxSpeed.Value, totalDistance)
                    };

                    return model;
                }

                return null;
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

        private double GetEstimatedTime(double? carWeight, double productWeight, int maxSpeed, double totalDistance)
        {
            if (carWeight.HasValue)
            {
                var percentWeight = productWeight * 100 / carWeight;
                percentWeight = percentWeight > 50 ? 50 : percentWeight;

                var speed = maxSpeed - (maxSpeed * percentWeight / 100) ;
                var time = totalDistance / speed;
                return System.Math.Round(time.Value, 1) * 60;
            }

            return 0;            
        }

        public IEnumerable<string> BuildPath(Order order, out double totalDistance)
        {
            using (IRepository<Order> orderRepository = Factory.GetService<IRepository<Order>>())
            using (IRepository<City> cityRepository = Factory.GetService<IRepository<City>>())
            using (IRepository<Destination> destRepository = Factory.GetService<IRepository<Destination>>())
            {
                if (order != null)
                {
                    try
                    {
                        var cities = cityRepository.GetAll().AsEnumerable();
                        var algorithm = Factory.GetService<IDijkstraAlgorithm>();
                        algorithm.Initialize(order.FromCity, order.ToCity);

                        var cityPath = algorithm.GetPath()?.ToArray();
                        if (cityPath != null)
                        {
                            List<string> cityNames = new List<string>();
                            double sum = 0;
                            for (int i = 0; i < cityPath.Length; i++)
                            {
                                if (i != cityPath.Length - 1)
                                {
                                    sum += this.GetDistance(destRepository, cityPath[i], cityPath[i + 1]);
                                }
                                var city = cities.FirstOrDefault(x => x.Id == cityPath[i]);

                                if (city != null)
                                {
                                    cityNames.Add(city.Name);
                                }
                            }
                            totalDistance = sum;
                            return cityNames;
                        }
                    }
                    catch(Exception ex)
                    {
                        totalDistance = 0;
                        return new List<string>();
                    }                    
                }

                totalDistance = 0;
                return new List<string>();
            }
        }

        private double GetDistance(IRepository<Destination> repo, int from, int to)
        {
            var distance = repo.FirstOrDefault(x => (x.Source == from && x.Target == to) || (x.Target == from && x.Source == to))?.Destination1;
            return distance.HasValue ? distance.Value : 0;
        }      
    }
}
