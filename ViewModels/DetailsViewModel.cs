using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DetailsViewModel
    {
        public double TotalDistance { get; set; }
        public List<string> Cities { get; set; }

        public double EstimatedTime { get; set; }
        public string TransportName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
