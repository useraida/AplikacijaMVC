using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HotelApp.Models.DB;
using HotelApp.Models.ViewModel;
using HotelApp.Models.EntityManager;

namespace HotelApp.Controllers
{
    public class AccountController : Controller
    { 
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(KorisnikLoginView klv, string returnUr1)
        {
            // provjerava da li su potrebna polja popunjena
            if (ModelState.IsValid)
            {
                // Pravi instancu klase UserManager 
                UserManager um = new UserManager();
                // uzima sifru korisnkika
                string password = um.GetKorisnikPassword(klv.LoginIme);
                // ako password vraca prazan string, javlja gresku
                if (string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Korisnicki login ili password nije tacan");
                }
                else
                {
                    // ako je password tacan, korisnik se preusmjerava na DobroDosli
                    if (klv.Password.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(klv.LoginIme, false);
                        return RedirectToAction("DobroDosli", "LogInHome");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password nije tacan");
                    }
                }
            }
            // ako se sve zavrsi, a nesto ne uspije prikazi obrazac ponovo
            return View(klv);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return View();
            //return RedirectToAction("Index", "Home");
        }
    }
}