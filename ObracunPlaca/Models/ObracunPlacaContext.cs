using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ObracunPlaca.Models
{
   public class ObracunPlacaContext: DbContext
    {
        
        public ObracunPlacaContext()
           : base("DefaultConnection")
        {

        }

        public DbSet<Djelatnici> Djelatnicis { get; set; }
        public DbSet<ZapisPlace> ZapisPlaces { get; set; }

    }

}