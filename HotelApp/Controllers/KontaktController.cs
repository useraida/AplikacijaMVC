using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HotelApp.Models;
using HotelApp.Security;

namespace HotelApp.Controllers
{
    public class KontaktController : Controller
    {
        private HotelEntities db;

        public KontaktController()
        {
            db = new HotelEntities();
        }

        // GET: Kontakt
        public ActionResult Index()
        {
            return View(db.Kontakt.ToList());
        }

        public ActionResult Create()
        {
            return View(new Kontakt());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Kontakt model)
        {
            if (ModelState.IsValid)
            {
                db.Kontakt.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kontakt kontakt = db.Kontakt.Find(id);
            if (kontakt == null)
            {
                return HttpNotFound();
            }
            return View(kontakt);
        }

        [AuthorizeRoles("Admin")]
        public ActionResult Izbrisi(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kontakt kontakt = db.Kontakt.Find(id);
            if(kontakt == null)
            {
                return HttpNotFound();
            }
            return View(kontakt);
        }

        [HttpPost, ActionName("Izbrisi")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("Admin")]
        public ActionResult PotvrdiBrisanje(int id)
        {
            Kontakt kontakt = db.Kontakt.Find(id);
            db.Kontakt.Remove(kontakt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}