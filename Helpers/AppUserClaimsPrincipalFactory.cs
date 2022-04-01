using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVCappCoreWeb.Areas.Identity.Data;

namespace MVCappCoreWeb.Helpers
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<WebUser, IdentityRole>
    {
        public AppUserClaimsPrincipalFactory(UserManager<WebUser> user, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
           : base(user, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(WebUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));
            return identity;
        }
    }
}
