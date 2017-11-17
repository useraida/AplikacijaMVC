using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelApp.Models;
using HotelApp.Security;

namespace HotelApp.Controllers
{
    public class DnevniIzvjestajsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: DnevniIzvjestajs
        [AuthorizeRoles("Admin")]
        public ActionResult Index()
        {
            var dnevniIzvjestaj = db.DnevniIzvjestaj.Include(d => d.Rezervacija).Include(d => d.Recepcioner);
            return View(dnevniIzvjestaj.ToList());
        }

        // GET: DnevniIzvjestajs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DnevniIzvjestaj dnevniIzvjestaj = db.DnevniIzvjestaj.Find(id);
            if (dnevniIzvjestaj == null)
            {
                return HttpNotFound();
            }
            return View(dnevniIzvjestaj);
        }

        // GET: DnevniIzvjestajs/Create
        public ActionResult Create()
        {
            ViewBag.RezervacijaID = new SelectList(db.Rezervacija, "RezervacijaID", "RezervacijaID");
            ViewBag.RecepcionerID = new SelectList(db.Recepcioner, "RecepcionerID", "ImeRecepcionera");
            return View();
        }

        // POST: DnevniIzvjestajs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DnevniIzvjestajID,Datum,RezervacijaID,RecepcionerID")] DnevniIzvjestaj dnevniIzvjestaj)
        {
            if (ModelState.IsValid)
            {
                db.DnevniIzvjestaj.Add(dnevniIzvjestaj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RezervacijaID = new SelectList(db.Rezervacija, "RezervacijaID", "RezervacijaID", dnevniIzvjestaj.RezervacijaID);
            ViewBag.RecepcionerID = new SelectList(db.Recepcioner, "RecepcionerID", "ImeRecepcionera", dnevniIzvjestaj.RecepcionerID);
            return View(dnevniIzvjestaj);
        }

        // GET: DnevniIzvjestajs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DnevniIzvjestaj dnevniIzvjestaj = db.DnevniIzvjestaj.Find(id);
            if (dnevniIzvjestaj == null)
            {
                return HttpNotFound();
            }
            ViewBag.RezervacijaID = new SelectList(db.Rezervacija, "RezervacijaID", "RezervacijaID", dnevniIzvjestaj.RezervacijaID);
            ViewBag.RecepcionerID = new SelectList(db.Recepcioner, "RecepcionerID", "ImeRecepcionera", dnevniIzvjestaj.RecepcionerID);

            return View(dnevniIzvjestaj);
        }

        // POST: DnevniIzvjestajs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DnevniIzvjestajID,Datum,RezervacijaID,RecepcionerID")] DnevniIzvjestaj dnevniIzvjestaj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dnevniIzvjestaj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RezervacijaID = new SelectList(db.Rezervacija, "RezervacijaID", "RezervacijaID", dnevniIzvjestaj.RezervacijaID);
            ViewBag.RecepcionerID = new SelectList(db.Recepcioner, "RecepcionerID", "ImeRecepcionera", dnevniIzvjestaj.RecepcionerID);
            return View(dnevniIzvjestaj);
        }

        // GET: DnevniIzvjestajs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DnevniIzvjestaj dnevniIzvjestaj = db.DnevniIzvjestaj.Find(id);
            if (dnevniIzvjestaj == null)
            {
                return HttpNotFound();
            }
            return View(dnevniIzvjestaj);
        }

        // POST: DnevniIzvjestajs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DnevniIzvjestaj dnevniIzvjestaj = db.DnevniIzvjestaj.Find(id);
            db.DnevniIzvjestaj.Remove(dnevniIzvjestaj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
