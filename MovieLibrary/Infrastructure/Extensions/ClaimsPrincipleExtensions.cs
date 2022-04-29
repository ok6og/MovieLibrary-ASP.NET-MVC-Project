using System.Security.Claims;
using static MovieLibrary.WebConstants;

namespace MovieLibrary.Infrastructure.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            =>user.IsInRole(AdministratorRoleName);
    }
}
