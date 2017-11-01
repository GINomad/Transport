using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.ViewModels;

namespace Transport.Business.Interfaces
{
    public interface IOrderManager
    {
        bool AddOrder(OrderViewModel model);

        List<OrderViewModel> GetOrders();

        IEnumerable<int> BuildPath(int id);
    }
   
}
