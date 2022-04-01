using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVCappCoreWeb.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MVCappCoreWeb.Areas.Identity.Data;

namespace MVCappCoreWeb
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<WebUser, IdentityRole>
    {
        private List<WebUser> listUsers;
        private RoleManager<IdentityRole> roleManager;
        private object p;

        public AdditionalUserClaimsPrincipalFactory(
             UserManager<WebUser> userManager,
             RoleManager<IdentityRole> roleManager,
             IOptions<IdentityOptions> optionsAccessor)
             : base(userManager, roleManager, optionsAccessor)
        {
        }

      
    }
}
