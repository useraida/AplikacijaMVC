using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelApp.Security;

namespace HotelApp.Controllers
{
    public class LogInHomeController : Controller
    {
        // GET: LogInHome
        public ActionResult DobroDosli()
        {
            return View();
        }

    }
}