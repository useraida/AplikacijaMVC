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
    public class HotelTsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: HotelTs
        [AuthorizeRoles("Admin")]
        public ActionResult Index()
        {
            var hotelT = db.HotelT.Include(h => h.Recepcija);
            return View(hotelT.ToList());
        }

        // GET: HotelTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelT hotelT = db.HotelT.Find(id);
            if (hotelT == null)
            {
                return HttpNotFound();
            }
            return View(hotelT);
        }

        // GET: HotelTs/Create
        public ActionResult Create()
        {
            ViewBag.RecepcijaID = new SelectList(db.Recepcija, "RecepcijaID", "RecepcijaID");
            return View();
        }

        // POST: HotelTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelTID,NazivHotela,Vlasnik,RecepcijaID")] HotelT hotelT)
        {
            if (ModelState.IsValid)
            {
                db.HotelT.Add(hotelT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecepcijaID = new SelectList(db.Recepcija, "RecepcijaID", "RecepcijaID", hotelT.RecepcijaID);
            return View(hotelT);
        }

        // GET: HotelTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelT hotelT = db.HotelT.Find(id);
            if (hotelT == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecepcijaID = new SelectList(db.Recepcija, "RecepcijaID", "RecepcijaID", hotelT.RecepcijaID);
            return View(hotelT);
        }

        // POST: HotelTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelTID,NazivHotela,Vlasnik,RecepcijaID")] HotelT hotelT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecepcijaID = new SelectList(db.Recepcija, "RecepcijaID", "RecepcijaID", hotelT.RecepcijaID);
            return View(hotelT);
        }

        // GET: HotelTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelT hotelT = db.HotelT.Find(id);
            if (hotelT == null)
            {
                return HttpNotFound();
            }
            return View(hotelT);
        }

        // POST: HotelTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelT hotelT = db.HotelT.Find(id);
            db.HotelT.Remove(hotelT);
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
