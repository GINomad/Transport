using System.Collections.Generic;
using Transport.Entity;
using Transport.ViewModels;
using ViewModels;

namespace Transport.Business.Interfaces
{
    public interface IOrderManager
    {
        bool AddOrder(OrderViewModel model);

        List<OrderViewModel> GetOrders();

        IEnumerable<string> BuildPath(Order order, out double totalDistance);
        DetailsViewModel GetOrder(int id);
    }
   
}
