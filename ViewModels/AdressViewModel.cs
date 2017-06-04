using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.ViewModels
{
    public class AdressViewModel
    {
        public int AdressId { get; set; }

        public bool IsSender { get; set; }

        [Display(Name = "Адрес")]
        public string AdressName { get; set; }
    }
}
