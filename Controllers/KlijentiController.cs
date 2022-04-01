using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.EfDbLayer;
namespace MVCappCoreWeb.Controllers
{
    public class KlijentiController : Controller
    {
        private readonly OrbisDbContext _context;
        //private RoleManager<IdentityRole> _roleManager;
        //private UserManager<WebUser> _userManager;
        public IActionResult Index()
        {
            return View();
        }
    }
}
