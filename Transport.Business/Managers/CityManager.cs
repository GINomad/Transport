using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Business.Interfaces;
using Transport.Entity;
using Transport.Infrastructure;
using Transport.ViewModels;
using ViewModels;

namespace Transport.Business.Managers
{
    public class CityManager : BaseBusiness, ICityManager
    {
        public void Add(CityModel model)
        {
            using (IRepository<City> cityRepository = Factory.GetService<IRepository<City>>())
            {
                if(model != null && model.Name != null)
                {
                    City city = new City()
                    {
                        Name = model.Name
                    };

                    cityRepository.Add(city);
                    cityRepository.SaveChanges();
                }                
            }
        }

        public IEnumerable<CityModel> GetCities()
        {
            using (IRepository<City> cityRepository = Factory.GetService<IRepository<City>>())
            {
                var cities = cityRepository.GetAll().ToList();
                List<CityModel> models = new List<CityModel>();
                foreach(var city in cities)
                {
                    models.Add(new CityModel() {
                        Id = city.Id,
                        Name = city.Name
                        });
                }
                return models;
            }
        }

        public bool IsExist(CityModel model)
        {
            using (IRepository<City> cityRepository = Factory.GetService<IRepository<City>>())
            {
                if(model != null && model.Name != null)
                {
                    var cities = cityRepository.GetAll().ToList();

                    return cities.Any(x => x.Name.ToLowerInvariant() == model.Name.ToLowerInvariant());
                }

                return false;
            }
        }
        public bool CheckDistance(int from, int to)
        {
            using (IRepository<Destination> destRepository = Factory.GetService<IRepository<Destination>>())
            {
                var dest = destRepository.FirstOrDefault(x => (x.Source == from && x.Target == to) || (x.Source == to && x.Target == from));
                return dest != null;
            }
        }

        public bool SetDistance(CityDistanceViewModel model)
        {
            using (IRepository<Destination> destRepository = Factory.GetService<IRepository<Destination>>())
            {
                try
                {
                    Destination dest = new Destination()
                    {
                        Source = model.From,
                        Target = model.To,
                        Destination1 = model.Distance
                    };
                    destRepository.Add(dest);
                    destRepository.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }                
            }
        }
    }
}
