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
    public class LokacijaSobesController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: LokacijaSobes
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            return View(db.LokacijaSobe.ToList());
        }

        // GET: LokacijaSobes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LokacijaSobe lokacijaSobe = db.LokacijaSobe.Find(id);
            if (lokacijaSobe == null)
            {
                return HttpNotFound();
            }
            return View(lokacijaSobe);
        }

        // GET: LokacijaSobes/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LokacijaSobes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create([Bind(Include = "LokacijaSobeID,SpratSobe,BrojSobe,Orijentacija")] LokacijaSobe lokacijaSobe)
        {
            if (ModelState.IsValid)
            {
                db.LokacijaSobe.Add(lokacijaSobe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lokacijaSobe);
        }

        // GET: LokacijaSobes/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LokacijaSobe lokacijaSobe = db.LokacijaSobe.Find(id);
            if (lokacijaSobe == null)
            {
                return HttpNotFound();
            }
            return View(lokacijaSobe);
        }

        // POST: LokacijaSobes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "LokacijaSobeID,SpratSobe,BrojSobe,Orijentacija")] LokacijaSobe lokacijaSobe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lokacijaSobe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lokacijaSobe);
        }

        // GET: LokacijaSobes/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LokacijaSobe lokacijaSobe = db.LokacijaSobe.Find(id);
            if (lokacijaSobe == null)
            {
                return HttpNotFound();
            }
            return View(lokacijaSobe);
        }

        // POST: LokacijaSobes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            LokacijaSobe lokacijaSobe = db.LokacijaSobe.Find(id);
            db.LokacijaSobe.Remove(lokacijaSobe);
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
