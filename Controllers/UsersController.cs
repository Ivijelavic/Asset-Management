using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCappCoreWeb.Areas.Identity.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<WebUser> _userManager;
        public UsersController(UserManager<WebUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            //return View(allUsersExceptCurrentUser);
            WebUser useradmin = await _userManager.GetUserAsync(HttpContext.User);
            var parenoib = _userManager.GetClaimsAsync(useradmin).Result.SingleOrDefault(r => r.Type == "Oib").Value;
            var users = _userManager.Users.ToList();
            var list = new List<WebUser>();

            var childoib = "";
            foreach (var user in _userManager.Users.ToList())
            {

                int claimCount = _userManager.GetClaimsAsync(user).Result.Count(r => r.Type == "Oib");

                if (claimCount == 1)
                {
                    childoib = _userManager.GetClaimsAsync(user).Result.SingleOrDefault(r => r.Type == "Oib").Value;
                    var rolesForUser = await _userManager.IsInRoleAsync(user, "Admin");
                    if (!rolesForUser)
                    {
                        if (childoib == parenoib)
                        {
                            list.Add(user);
                        }
                    }


                }

            }
            return View(list);
        }
    }
}
