using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelApp.Models.DB;
using HotelApp.Models.EntityManager;

namespace HotelApp.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] korisnikAssignedRoles;
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.korisnikAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using(LoginRoleDBEntities db = new LoginRoleDBEntities())
            {
                UserManager um = new UserManager();
                foreach(var roles in korisnikAssignedRoles)
                {
                    authorize = um.isKorisnikURoli(httpContext.User.Identity.Name, roles);
                    if (authorize)
                    {
                        return authorize;
                    }
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }
    }
}