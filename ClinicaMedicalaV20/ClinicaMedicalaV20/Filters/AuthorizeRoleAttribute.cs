using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace ClinicaMedicalaV20.Filters
{
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedRoles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");

            if (userRole == null || !allowedRoles.Contains(userRole))
            {
                context.Result = new RedirectToActionResult("Index","Home", null);
            }
        }
    }
}
