using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ObracunPlaca.Models;
using ObracunPlaca.ViewModels;

namespace ObracunPlaca.Controllers
{
    public class ZapisPlaceController : Controller
    {
        public int DjelatnikID { get; set; }
        private ObracunPlacaContext db = new ObracunPlacaContext();

        //
        // GET: /ZapisPlace/

        public ActionResult Index()
        {
            var zapisplaces = db.ZapisPlaces.Include(z => z.Djelatnici);
            return View(zapisplaces.ToList());
        }
    
        //
        // GET: /ZapisPlace/Create

        public ActionResult Create()
        {
            ViewBag.DjelatniciID = new SelectList(db.Djelatnicis, "DjelatniciID", "PrezimeDjelatnika");

            return View();
        }

        //
        // POST: /ZapisPlace/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel createViewModel, string DjelatniciID)
        {
            /* Dakle  createViewModel.PodaciZaObracunViewModel. stavke povlače podatke napisane u Viewu ZapisPlaće
             * u njihovim respektivnim poljima.
             * Ostale decimal stavke se definiraju za obračun plaće.
             * decimal osnovica je rastavljena if funkcijom ukoliko je odbitak veći od osnovice nemože biti negativna vrijednost.
             * decimal porez je rastavljena sa if funkcijom zbog zakonskih normi ( od 2200 do 8800).
             * Stavke Math.Round zaokruzuju sve na dvije decimale
             * ZapisPlaceID i Djelatnici ID se povecava svaki puta za jedan broj.
             * StringBuilder spaja više stringova kako bi ga mogli gurnuti u jedan View.
             * 
             * Hvala Saša na pomoći!
            */
            decimal mir = createViewModel.PodaciZaObracunViewModel.Bruto * 0.2m; 

            decimal odbitak = createViewModel.PodaciZaObracunViewModel.koef * 2200m;

            decimal dohodak = createViewModel.PodaciZaObracunViewModel.Bruto - mir;

            decimal osnovica;
            if (dohodak <= odbitak)
            {
                osnovica = 0;
            }
            else
            {
                osnovica = dohodak - odbitak;
            }

            decimal porez = 0;
            if (osnovica <= 2200.00m)
            {
                porez = osnovica * 0.12m;
            }
            else if (osnovica > 2200.00m && osnovica <= 8800.00m)
            {
                porez = 264.00m + (osnovica - 2200.00m) * 0.25m;
            }
            else if (osnovica > 8800.00m)
            {
                porez = 2464.00m + (osnovica - 8800.00m) * 0.40m;
            }

            decimal prirez = porez * (createViewModel.PodaciZaObracunViewModel.postotak/100);
            
            decimal pip = porez + prirez;

            decimal neto = dohodak - pip;

            if (DjelatniciID != null)
            {
                ZapisPlace zapisPlace = new ZapisPlace
                {
                    DjelatniciID = Convert.ToInt16(DjelatniciID),
                    ZapisPlaceID = db.ZapisPlaces.Count() + 1,
                    Mirovinsko = Math.Round(mir, 2, MidpointRounding.AwayFromZero),
                    NetoPlacaDjelatnika = Math.Round(neto, 2, MidpointRounding.AwayFromZero),
                    Odbitak = Math.Round(odbitak, 2, MidpointRounding.AwayFromZero),
                    Osnovica = Math.Round(osnovica, 2, MidpointRounding.AwayFromZero),
                    Porez = Math.Round(porez, 2, MidpointRounding.AwayFromZero),
                    PorezIPrirez = Math.Round(pip, 2, MidpointRounding.AwayFromZero),
                    Prirez = Math.Round(prirez, 2, MidpointRounding.AwayFromZero)
                };
                // string builder spaja vise stringova u jedan i vraca taj sadrzaj View 
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Format("<b>Mirovinsko:</b> {0} <br/>", zapisPlace.Mirovinsko));
                stringBuilder.Append(string.Format("<b>Odbitak:</b> {0} <br/>", zapisPlace.Odbitak));
                stringBuilder.Append(string.Format("<b>Porezna osnovica:</b> {0} <br/>", zapisPlace.Osnovica));
                stringBuilder.Append(string.Format("<b>Porez:</b> {0} <br/>", zapisPlace.Porez));
                stringBuilder.Append(string.Format("<b>Prirez:</b> {0} <br/>", zapisPlace.Prirez));
                stringBuilder.Append(string.Format("<b>Porez i prirez:</b> {0} <br/>", zapisPlace.PorezIPrirez));
                stringBuilder.Append(string.Format("<b>Netto:</b> {0} <br/>", zapisPlace.NetoPlacaDjelatnika));


                if (ModelState.IsValid)
                {
                    db.ZapisPlaces.Add(zapisPlace);
                    db.SaveChanges();
                    // ovdje se vracaju ti stringovi spojeni StringBulider-om, prekomplicirano bi bilo vracati jedan po jedan string - zato spajanje 
                    return Content(stringBuilder.ToString());
                }

                ViewBag.DjelatniciID = new SelectList(db.Djelatnicis, "DjelatniciID", "PrezimeDjelatnika", zapisPlace.DjelatniciID);
            }

            return View(createViewModel);
        }

       
        //
        // GET: /ZapisPlace/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ZapisPlace zapisplace = db.ZapisPlaces.Find(id);
            if (zapisplace == null)
            {
                return HttpNotFound();
            }
            return View(zapisplace);
        }

        //
        // POST: /ZapisPlace/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZapisPlace zapisplace = db.ZapisPlaces.Find(id);
            db.ZapisPlaces.Remove(zapisplace);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}