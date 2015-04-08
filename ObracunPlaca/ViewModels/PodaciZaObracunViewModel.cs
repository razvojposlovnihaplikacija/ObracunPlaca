using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObracunPlaca.ViewModels
{
    public class PodaciZaObracunViewModel
    {
        public decimal Bruto { get; set; }

        public decimal koef { get; set; }
        public decimal postotak { get; set; }
    }
}