﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CityDistanceViewModel
    {
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public double Distance { get; set; }
    }
}
