using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ObracunPlaca.Models
{
    public class Djelatnici
    {
        public int DjelatniciID { get; set; }

        [Required(ErrorMessage = "Obavezno ime djelatnika")]
        [DisplayName("Prezime djelatnika")]
        public string PrezimeDjelatnika { get; set; }

        [Required(ErrorMessage = "Obavezno prezime djelatnika")]
        [DisplayName("Ime djelatnika")]
        public string ImeDjelatnika { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Opis radnog mjesta")]
        public string OpisRadnogMjesta { get; set; }

        [Required(ErrorMessage = "Napisati datum u obliku DD/MM/YY")]
        [DataType(DataType.DateTime)]
        [DisplayName("Datum zaposlenja")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime DatumZaposlenja { get; set; }

        [Required(ErrorMessage = "Sifra djelatnika je obavezna")]
        [DisplayName("Sifra djelatnika")]
        public string SifraDjelatnika { get; set; }

        [Required(ErrorMessage = "Obavezan unos bruto place djelatnika")]
        [DisplayName("Bruto placa djelatnika")]
        public decimal BrutoPlacaDjelatnika { get; set; }

        public virtual ICollection<ZapisPlace> ZapisPlaces { get; set; }
    }
}