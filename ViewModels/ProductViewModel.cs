using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Вес. кг")]
        public double Weight { get; set; }

        [Display(Name = "Ширина. м")]
        public double Width { get; set; }

        [Display(Name = "Высота. м")]
        public double Height { get; set; }

        [Display(Name = "Длинна. м")]
        public double Length { get; set; }

        [Display(Name = "Укажите хрупкость объекта")]
        public int Fragility { get; set; }

        [Display(Name = "Вы перевозите жидкость?")]
        public bool isLiquid { get; set; }

    }
}
