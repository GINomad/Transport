using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Transport.Infrastructure;
using Transport.Business.Interfaces;
using Transport.Business.Managers;
using Transport.Entity;

namespace Transport.Business.Util
{
    public class Bootstrapper
    {
        private static IUnityContainer container;

        public Bootstrapper()
        {
            Initialise();
        }
        public void Initialise()
        {
            container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit              

            RegisterTypes(container);
            RegisterServices(container);
            return container;
        }

        public static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ITransportTypeManager, TransportTypeManager>();
            container.RegisterType<IDriverManager, DriverManager>();
            container.RegisterType<ITransportManager, TransportManager>();
            container.RegisterType<IOrderManager, OrderManager>();
            container.RegisterType<IDijkstraAlgorithm, DijkstraAlgorithm>();
            container.RegisterType<ICityManager, CityManager>();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<TransportType>, BaseRepository<TransportType>>();
            container.RegisterType<IRepository<Transport.Entity.Transport>, BaseRepository<Entity.Transport>>();
            container.RegisterType<IRepository<Driver>, BaseRepository<Driver>>();
            container.RegisterType<IRepository<Order>, BaseRepository<Order>>();
            container.RegisterType<IRepository<City>, BaseRepository<City>>();
            container.RegisterType<IRepository<Destination>, BaseRepository<Destination>>();
        }

        public T GetService<T>()
        {
            if (container.IsRegistered<T>())
            {
                return container.Resolve<T>();
            }
            else
            {
                return default(T);
            }

        }
    }
}
