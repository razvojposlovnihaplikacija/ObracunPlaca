using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ObracunPlaca.Models
{
    public class ZapisPlace
    {
        public int ZapisPlaceID { get; set; }

        [Required(ErrorMessage = "Molimo odaberite djelatnika")]
        public int DjelatniciID { get; set; }


        public decimal Mirovinsko { get; set; }
        public decimal Odbitak { get; set; }
        public decimal Osnovica { get; set; }
        public decimal Porez { get; set; }
        public decimal Prirez { get; set; }
        public decimal PorezIPrirez { get; set; }
        public decimal NetoPlacaDjelatnika { get; set; }

        public virtual Djelatnici Djelatnici { get; set; }
    }
}