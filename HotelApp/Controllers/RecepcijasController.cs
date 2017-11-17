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
    public class RecepcijasController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: Recepcijas
        [AuthorizeRoles("Admin")]
        public ActionResult Index()
        {
            var recepcija = db.Recepcija.Include(r => r.DnevniIzvjestaj);
            return View(recepcija.ToList());
        }

        // GET: Recepcijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcija recepcija = db.Recepcija.Find(id);
            if (recepcija == null)
            {
                return HttpNotFound();
            }
            return View(recepcija);
        }

        // GET: Recepcijas/Create
        public ActionResult Create()
        {
            ViewBag.DnevniIzvjestajID = new SelectList(db.DnevniIzvjestaj, "DnevniIzvjestajID", "DnevniIzvjestajID");
            return View();
        }

        // POST: Recepcijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecepcijaID,DnevniIzvjestajID,BrojRezervacija,UkupnaZarada")] Recepcija recepcija)
        {
            if (ModelState.IsValid)
            {
                db.Recepcija.Add(recepcija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DnevniIzvjestajID = new SelectList(db.DnevniIzvjestaj, "DnevniIzvjestajID", "DnevniIzvjestajID", recepcija.DnevniIzvjestajID);
            return View(recepcija);
        }

        // GET: Recepcijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcija recepcija = db.Recepcija.Find(id);
            if (recepcija == null)
            {
                return HttpNotFound();
            }
            ViewBag.DnevniIzvjestajID = new SelectList(db.DnevniIzvjestaj, "DnevniIzvjestajID", "DnevniIzvjestajID", recepcija.DnevniIzvjestajID);
            return View(recepcija);
        }

        // POST: Recepcijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecepcijaID,DnevniIzvjestajID,BrojRezervacija,UkupnaZarada")] Recepcija recepcija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepcija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DnevniIzvjestajID = new SelectList(db.DnevniIzvjestaj, "DnevniIzvjestajID", "DnevniIzvjestajID", recepcija.DnevniIzvjestajID);
            return View(recepcija);
        }

        // GET: Recepcijas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcija recepcija = db.Recepcija.Find(id);
            if (recepcija == null)
            {
                return HttpNotFound();
            }
            return View(recepcija);
        }

        // POST: Recepcijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recepcija recepcija = db.Recepcija.Find(id);
            db.Recepcija.Remove(recepcija);
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
