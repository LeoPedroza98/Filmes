using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace Filmes.API.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _roles;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;

            if (claims.Count() == 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            var roleProvider = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            var roles = _roles.Split(',');

            for (int i = 0; i < roles.Length; i++)
            {
                roles[i] = roles[i].Trim();
            }

            var hasAuthorization = roles.Contains(roleProvider);

            if (hasAuthorization)
                return;

            context.Result = new ForbidResult();
            return;
        }

        public CustomAuthorizeAttribute(string roles)
        {
            _roles = roles;
        }
    }
}
