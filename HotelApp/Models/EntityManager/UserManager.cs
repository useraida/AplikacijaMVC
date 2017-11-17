using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelApp.Models.DB;
using HotelApp.Models.ViewModel;

namespace HotelApp.Models.EntityManager
{
    public class UserManager
    {
        // IsLoginImePostoji() je metoda koja vraca bool. Provjerava da li u bazi postoji vec odredjeno ime za login
        // koristeci LINQ sintaksu.
        public bool IsLoginImePostoji(string LoginIme)
        {
            using (LoginRoleDBEntities db = new LoginRoleDBEntities())
            {
                return db.Korisnik.Where(o => o.LoginIme.Equals(LoginIme)).Any();
            }
        }
        
        public string GetKorisnikPassword(string loginIme)
        {
            using(LoginRoleDBEntities db = new LoginRoleDBEntities())
            {
                var korisnik = db.Korisnik.Where(o => o.LoginIme.ToLower().Equals(loginIme));
                if(korisnik.Any())
                {
                    return korisnik.FirstOrDefault().Password;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        //daje odgovor da li korisnik ima odredjene role
        public bool isKorisnikURoli(string loginIme, string roleIme)
        {
            using (LoginRoleDBEntities db = new LoginRoleDBEntities())
            {
                Korisnik k = db.Korisnik.Where(o => o.LoginIme.ToLower().Equals(loginIme))?.FirstOrDefault();
                if(k != null)
                {
                    var roles = from q in db.KorisnikRole
                                join r in db.RoleVrste on q.RoleVrsteID equals r.RoleVrsteID
                                where r.RoleIme.Equals(roleIme) && q.KorisnikID.Equals(k.KorisnikID)
                                select r.RoleIme;
                    if(roles != null)
                    {
                        return roles.Any();
                    }
                }
                return false;
            }
        }

    }
}