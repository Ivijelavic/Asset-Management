using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using MVCappCoreWeb.Areas.Identity.Data;
namespace MVCappCoreWeb.Controllers
{
    public class ClaimTestController : Controller
    {
        private UserManager<WebUser> userManager;

        //[Authorize(Policy = "AspManager")]
        public ViewResult Project() => View("Index", User?.Claims);

        public ClaimTestController(UserManager<WebUser> userMgr)
        {
            userManager = userMgr;
        }

        public ViewResult Index() => View(User?.Claims);

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
