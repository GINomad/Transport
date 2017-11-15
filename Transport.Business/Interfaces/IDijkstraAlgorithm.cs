using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Business.Interfaces
{
    public interface IDijkstraAlgorithm
    {
        void Initialize(int start, int end);
        IEnumerable<int> GetPath();
    }
}
