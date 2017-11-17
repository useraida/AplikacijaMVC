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
    public class RecepcionersController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: Recepcioners
        [AuthorizeRoles("Admin")]
        public ActionResult Index()
        {
            return View(db.Recepcioner.ToList());
        }

        // GET: Recepcioners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcioner recepcioner = db.Recepcioner.Find(id);
            if (recepcioner == null)
            {
                return HttpNotFound();
            }
            return View(recepcioner);
        }

        // GET: Recepcioners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recepcioners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecepcionerID,ImeRecepcionera,PrezimeRecepcionera,Username,Password")] Recepcioner recepcioner)
        {
            if (ModelState.IsValid)
            {
                db.Recepcioner.Add(recepcioner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recepcioner);
        }

        // GET: Recepcioners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcioner recepcioner = db.Recepcioner.Find(id);
            if (recepcioner == null)
            {
                return HttpNotFound();
            }
            return View(recepcioner);
        }

        // POST: Recepcioners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecepcionerID,ImeRecepcionera,PrezimeRecepcionera,Username,Password")] Recepcioner recepcioner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepcioner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recepcioner);
        }

        // GET: Recepcioners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcioner recepcioner = db.Recepcioner.Find(id);
            if (recepcioner == null)
            {
                return HttpNotFound();
            }
            return View(recepcioner);
        }

        // POST: Recepcioners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recepcioner recepcioner = db.Recepcioner.Find(id);
            db.Recepcioner.Remove(recepcioner);
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
