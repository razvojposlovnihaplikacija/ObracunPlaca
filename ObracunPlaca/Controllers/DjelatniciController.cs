using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObracunPlaca.Models;

namespace ObracunPlaca.Controllers
{
    public class DjelatniciController : Controller
    {
        private ObracunPlacaContext db = new ObracunPlacaContext();

        //
        // GET: /Djelatnici/
         /*Ovo je searchsting  napravljen za pretraživanje po prezimenu i šifri radnika.
           *Preuzeto je sa neta i ukomponirano da radi po našemu.
           *Prvi if nam daje string textbox za pretraživanje prezimena radnika
           *Drugi if nam daje dropbox da možemo pretraživati po šiframa
           *Sve nam to vraća na Djelatnici/Index 
           *Svi imaju [Authorize] prema zahtjevu da se djelatnici moraju registrirati i logirati kako bi mogli dodati 
          */
        public ActionResult Index(string searchSifra, string searchString)
        {
            var SifraLst = new List<string>();

            var SifraQry = from d in db.Djelatnicis
                           orderby d.SifraDjelatnika
                           select d.SifraDjelatnika;

            SifraLst.AddRange(SifraQry.Distinct());

            ViewBag.searchSifra = new SelectList(SifraLst);

                                                 
            var djelatnici = from m in db.Djelatnicis
                            select m; 
 
             if (!String.IsNullOrEmpty(searchString)) 
                 { 
                djelatnici = djelatnici.Where(s => s.PrezimeDjelatnika.Contains(searchString)); 
                 }
            
            if (string.IsNullOrEmpty(searchSifra))
                 return View(djelatnici);
            else
             {
                 return View(djelatnici.Where(x => x.SifraDjelatnika == searchSifra));
             } 
            

          }

        //
        // GET: /Djelatnici/Details/5

        public ActionResult Details(int id = 0)
        {
            Djelatnici djelatnici = db.Djelatnicis.Find(id);
            if (djelatnici == null)
            {
                return HttpNotFound();
            }
            return View(djelatnici);
        }

        //
        // GET: /Djelatnici/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Djelatnici/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Djelatnici djelatnici)
        {
            if (ModelState.IsValid)
            {
                db.Djelatnicis.Add(djelatnici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(djelatnici);
        }

        //
        // GET: /Djelatnici/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Djelatnici djelatnici = db.Djelatnicis.Find(id);
            if (djelatnici == null)
            {
                return HttpNotFound();
            }
            return View(djelatnici);
        }

        //
        // POST: /Djelatnici/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Djelatnici djelatnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(djelatnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(djelatnici);
        }

        //
        // GET: /Djelatnici/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Djelatnici djelatnici = db.Djelatnicis.Find(id);
            if (djelatnici == null)
            {
                return HttpNotFound();
            }
            return View(djelatnici);
        }

        //
        // POST: /Djelatnici/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Djelatnici djelatnici = db.Djelatnicis.Find(id);
            db.Djelatnicis.Remove(djelatnici);
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