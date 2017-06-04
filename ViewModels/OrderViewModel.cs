using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.ViewModels;

namespace Transport.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            SenderAdress = new AdressViewModel();
            ReceiverAdress = new AdressViewModel();
            Transport = new TransportViewModel();
            Transports = new List<TransportViewModel>();
        }
        public int OrderId { get; set; }
        
        [Display(Name = "Дата отправки")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата доставки")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Адрес отправителя")]
        public AdressViewModel SenderAdress { get; set; }

        [Display(Name = "Адрес доставки")]
        public AdressViewModel ReceiverAdress { get; set; }

        [Display(Name = "Транспорт для перевозки")]
        public TransportViewModel Transport { get; set; }

        public List<TransportViewModel> Transports { get; set; }
    }
}
