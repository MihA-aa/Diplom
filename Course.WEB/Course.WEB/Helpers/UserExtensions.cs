using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace Course.WEB.Helpers
{
    public static class UserExtensions
    {
        public static bool HasPermissionToRedact(this IPrincipal user, string creatorId)
        {
            return creatorId == user.Identity.GetUserId() || user.IsInRole("superAdmin");
        }

        public static bool HasPermissionToCreate(this IPrincipal user)
        {
            return user.IsInRole("admin") || user.IsInRole("superAdmin");
        }
    }
}