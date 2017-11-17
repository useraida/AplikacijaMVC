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
    public class CheckOutsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: CheckOuts
        [AuthorizeRoles("Admin", "gostHotela")]
        public ActionResult Index()
        {
            return View(db.CheckOut.ToList());
        }

        // GET: CheckOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOut.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // GET: CheckOuts/Create
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Create([Bind(Include = "CheckOutID,DatumOdjave,DodatniTrosak,UkupnaCijena,NacinPlacanja")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.CheckOut.Add(checkOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(checkOut);
        }

        // GET: CheckOuts/Edit/5
        [AuthorizeRoles("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOut.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult Edit([Bind(Include = "CheckOutID,DatumOdjave,DodatniTrosak,UkupnaCijena,NacinPlacanja")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkOut);
        }

        // GET: CheckOuts/Delete/5
        [AuthorizeRoles("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOut.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckOut checkOut = db.CheckOut.Find(id);
            db.CheckOut.Remove(checkOut);
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
