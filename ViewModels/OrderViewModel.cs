using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.ViewModels;
using ViewModels;

namespace Transport.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int FromCity { get; set; }
        public int ToCity { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; } 
        public double Width { get; set; }
        public double Length { get; set; }
        public string Name { get;  set; }
        public string TransportName { get; set; }
        public string FromCityName { get; set; }
        public string ToCityName { get; set; }
    }
}
