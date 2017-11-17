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
    public class CheckInsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: CheckIns
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            var checkIn = db.CheckIn.Include(c => c.Zaduzenja);
            return View(checkIn.ToList());
        }

        // GET: CheckIns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.CheckIn.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // GET: CheckIns/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            ViewBag.ZaduzenjaID = new SelectList(db.Zaduzenja, "ZaduzenjaID", "VrstaZaduzenja");
            return View();
        }

        // POST: CheckIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create([Bind(Include = "CheckInID,ZaduzenjaID,DatumPrijave")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.CheckIn.Add(checkIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZaduzenjaID = new SelectList(db.Zaduzenja, "ZaduzenjaID", "VrstaZaduzenja", checkIn.ZaduzenjaID);
            return View(checkIn);
        }

        // GET: CheckIns/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.CheckIn.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZaduzenjaID = new SelectList(db.Zaduzenja, "ZaduzenjaID", "VrstaZaduzenja", checkIn.ZaduzenjaID);
            return View(checkIn);
        }

        // POST: CheckIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "CheckInID,ZaduzenjaID,DatumPrijave")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZaduzenjaID = new SelectList(db.Zaduzenja, "ZaduzenjaID", "VrstaZaduzenja", checkIn.ZaduzenjaID);
            return View(checkIn);
        }

        // GET: CheckIns/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.CheckIn.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckIn checkIn = db.CheckIn.Find(id);
            db.CheckIn.Remove(checkIn);
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
