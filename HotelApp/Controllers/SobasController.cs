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
    public class SobasController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: Sobas
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            var soba = db.Soba.Include(s => s.LokacijaSobe);
            return View(soba.ToList());
        }

        // GET: Sobas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Soba.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // GET: Sobas/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            ViewBag.LokacijaSobeID = new SelectList(db.LokacijaSobe, "LokacijaSobeID", "Orijentacija");
            return View();
        }

        // POST: Sobas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create([Bind(Include = "SobaID,TipSobe,StatusSobe,LokacijaSobeID,DodatneInformacije")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Soba.Add(soba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LokacijaSobeID = new SelectList(db.LokacijaSobe, "LokacijaSobeID", "Orijentacija", soba.LokacijaSobeID);
            return View(soba);
        }

        // GET: Sobas/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Soba.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            ViewBag.LokacijaSobeID = new SelectList(db.LokacijaSobe, "LokacijaSobeID", "Orijentacija", soba.LokacijaSobeID);
            return View(soba);
        }

        // POST: Sobas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "SobaID,TipSobe,StatusSobe,LokacijaSobeID,DodatneInformacije")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LokacijaSobeID = new SelectList(db.LokacijaSobe, "LokacijaSobeID", "Orijentacija", soba.LokacijaSobeID);
            return View(soba);
        }

        // GET: Sobas/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Soba.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // POST: Sobas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Soba soba = db.Soba.Find(id);
            db.Soba.Remove(soba);
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
