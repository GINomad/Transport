using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.ViewModels;

namespace Transport.Business.Interfaces
{
    public interface IDriverManager
    {
        bool AddDriver(DriverViewModel model);

        List<DriverViewModel> GetDrivers();
    }
}
