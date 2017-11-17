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
    public class GostsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: Gosts
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            return View(db.Gost.ToList());
        }

        // GET: Gosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gost.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // GET: Gosts/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create([Bind(Include = "GostID,Ime,Prezime,Email,BrojTelefona,Adresa,Grad,Drzava,BrojLicneKarte,BrojPasosa,Username,Password")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                db.Gost.Add(gost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gost);
        }

        // GET: Gosts/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gost.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // POST: Gosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "GostID,Ime,Prezime,Email,BrojTelefona,Adresa,Grad,Drzava,BrojLicneKarte,BrojPasosa,Username,Password")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gost);
        }

        // GET: Gosts/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gost.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // POST: Gosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Gost gost = db.Gost.Find(id);
            db.Gost.Remove(gost);
            
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
