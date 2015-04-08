using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ObracunPlaca.Models
{
    /// <summary>
    /// za mijenjanje baze postoje dvije mogucnosti:
    /// 1. DropCreateDatabaseAlways koja je dolje napravljena i ujedno je i jednostavan nacin izmjene, glavni nedostatak je 
    ///     sto se uvijek izgube podaci
    /// 2. Enity Framework Code First Migrations - kompleksno, ali vrlo korisno  
    /// </summary>
    public class ObracunPlacaInitializer: DropCreateDatabaseIfModelChanges<ObracunPlacaContext>
    {
        protected override void Seed(ObracunPlacaContext context)
        {
            base.Seed(context);

            var djelatnik = new Djelatnici
            {
                DjelatniciID = 1,
                ImeDjelatnika = "Antun",
                PrezimeDjelatnika = "Magjer",
                OpisRadnogMjesta = "Tester",
                DatumZaposlenja = new DateTime(2013, 11, 11),
                SifraDjelatnika = "001",
                BrutoPlacaDjelatnika = 20000.00m
            };

            context.Djelatnicis.AddOrUpdate(djelatnik);
        }
    }
}