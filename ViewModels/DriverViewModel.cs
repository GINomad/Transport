using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.ViewModels
{
    public class DriverViewModel
    {
        public int DriverId { get; set; }

        [Display(Name = "Имя водителя")]
        [Required(ErrorMessage = "Это поле обязательно")]
        public string DriverName { get; set; }
    }
}
