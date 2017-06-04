using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.ViewModels
{
    public class TransportViewModel
    {
        public TransportViewModel()
        {
            Types = new List<TransportTypeViewModel>();
            Drivers = new List<DriverViewModel>();
            Type = new TransportTypeViewModel();
            Driver = new DriverViewModel();
        }
        public int TransportId { get; set; }

        [Display(Name = "Название транспорта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [Display(Name = "Номер машины")]
        public string CarNumber { get; set; }

        [Display(Name= "Грузоподъемность. кг")]
        public double CarryingCapacity { get; set; }

        [Display(Name = "Расход топлива. л/100км")]
        public double FuelConsumption { get; set; }

        [Display(Name = "Длинна. м.")]
        [Required(ErrorMessage = "Это поле обязательно")]
        public double Length { get; set; }

        [Display(Name = "Ширина. м.")]
        [Required(ErrorMessage = "Это поле обязательно")]
        public double Width { get; set; }

        [Display(Name = "Высота. м.")]
        [Required(ErrorMessage = "Это поле обязательно")]
        public double Height { get; set; }

        [Display(Name = "Водитель")]
        [Required(ErrorMessage = "Это поле обязательно")]
        public DriverViewModel Driver { get; set; }

        [Display(Name = "Тип транспорта")]
        [Required(ErrorMessage = "Это поле обязательно")]
        public  TransportTypeViewModel  Type { get; set; }

        public List<TransportTypeViewModel> Types { get; set; }

        public List<DriverViewModel> Drivers { get; set; }
    }
}
