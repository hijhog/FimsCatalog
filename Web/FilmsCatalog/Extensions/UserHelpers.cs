using System;
using System.Security.Claims;
using System.Security.Principal;

namespace FilmsCatalog.Extensions
{
    public static class UserHelpers
    {
        public static Guid? GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? null : Guid.Parse(claim.Value);
        }
    }
}
