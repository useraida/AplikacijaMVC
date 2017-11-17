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
    public class ZaduzenjasController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: Zaduzenjas
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            return View(db.Zaduzenja.ToList());
        }

        // GET: Zaduzenjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaduzenja zaduzenja = db.Zaduzenja.Find(id);
            if (zaduzenja == null)
            {
                return HttpNotFound();
            }
            return View(zaduzenja);
        }

        // GET: Zaduzenjas/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zaduzenjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZaduzenjaID,VrstaZaduzenja,Cijena")] Zaduzenja zaduzenja)
        {
            if (ModelState.IsValid)
            {
                db.Zaduzenja.Add(zaduzenja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zaduzenja);
        }

        // GET: Zaduzenjas/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaduzenja zaduzenja = db.Zaduzenja.Find(id);
            if (zaduzenja == null)
            {
                return HttpNotFound();
            }
            return View(zaduzenja);
        }

        // POST: Zaduzenjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "ZaduzenjaID,VrstaZaduzenja,Cijena")] Zaduzenja zaduzenja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zaduzenja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zaduzenja);
        }

        // GET: Zaduzenjas/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaduzenja zaduzenja = db.Zaduzenja.Find(id);
            if (zaduzenja == null)
            {
                return HttpNotFound();
            }
            return View(zaduzenja);
        }

        // POST: Zaduzenjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Zaduzenja zaduzenja = db.Zaduzenja.Find(id);
            db.Zaduzenja.Remove(zaduzenja);
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
