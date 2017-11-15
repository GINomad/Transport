using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Entity;
using Transport.ViewModels;
using ViewModels;

namespace Transport.Business.Interfaces
{
    public interface ICityManager
    {
        void Add(CityModel model);
        bool IsExist(CityModel model);
        bool CheckDistance(int from, int to);
        bool SetDistance(CityDistanceViewModel model);
        IEnumerable<CityModel> GetCities();
    }
}
