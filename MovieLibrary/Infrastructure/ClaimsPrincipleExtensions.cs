using System.Security.Claims;

namespace MovieLibrary.Infrastructure
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
