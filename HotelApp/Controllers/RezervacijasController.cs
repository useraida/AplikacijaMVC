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
    public class RezervacijasController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: Rezervacijas
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            var rezervacija = db.Rezervacija.Include(r => r.CheckIn).Include(r => r.CheckOut).Include(r => r.Gost).Include(r => r.Soba);
            return View(rezervacija.ToList());
        }

        // GET: Rezervacijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacija.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacijas/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            ViewBag.CheckInID = new SelectList(db.CheckIn, "CheckInID", "CheckInID");
            ViewBag.CheckOutID = new SelectList(db.CheckOut, "CheckOutID", "CheckOutID");
            ViewBag.GostID = new SelectList(db.Gost, "GostID", "Ime");
            ViewBag.SobaID = new SelectList(db.Soba, "SobaID", "SobaID");
            return View();
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create([Bind(Include = "RezervacijaID,GostID,SobaID,CheckInID,CheckOutID,BrojOdraslih,BrojDjece,TipPonude,DodatneInformacije")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Rezervacija.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CheckInID = new SelectList(db.CheckIn, "CheckInID", "CheckInID", rezervacija.CheckInID);
            ViewBag.CheckOutID = new SelectList(db.CheckOut, "CheckOutID", "CheckOutID", rezervacija.CheckOutID);
            ViewBag.GostID = new SelectList(db.Gost, "GostID", "Ime", rezervacija.GostID);
            ViewBag.SobaID = new SelectList(db.Soba, "SobaID", "SobaID", rezervacija.SobaID);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacija.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.CheckInID = new SelectList(db.CheckIn, "CheckInID", "CheckInID", rezervacija.CheckInID);
            ViewBag.CheckOutID = new SelectList(db.CheckOut, "CheckOutID", "CheckOutID", rezervacija.CheckOutID);
            ViewBag.GostID = new SelectList(db.Gost, "GostID", "Ime", rezervacija.GostID);
            ViewBag.SobaID = new SelectList(db.Soba, "SobaID", "SobaID", rezervacija.SobaID);
            return View(rezervacija);
        }

        // POST: Rezervacijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "RezervacijaID,GostID,SobaID,CheckInID,CheckOutID,BrojOdraslih,BrojDjece,TipPonude,DodatneInformacije")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervacija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CheckInID = new SelectList(db.CheckIn, "CheckInID", "CheckInID", rezervacija.CheckInID);
            ViewBag.CheckOutID = new SelectList(db.CheckOut, "CheckOutID", "CheckOutID", rezervacija.CheckOutID);
            ViewBag.GostID = new SelectList(db.Gost, "GostID", "Ime", rezervacija.GostID);
            ViewBag.SobaID = new SelectList(db.Soba, "SobaID", "SobaID", rezervacija.SobaID);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacija.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervacija rezervacija = db.Rezervacija.Find(id);
            db.Rezervacija.Remove(rezervacija);
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
