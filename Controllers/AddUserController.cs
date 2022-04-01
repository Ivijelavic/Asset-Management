using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCappCoreWeb.Areas.Identity.Data;
using MVCappCoreWeb.Data;
using MVCappCoreWeb.DBLayer;
using MVCappCoreWeb.Models;
using Newtonsoft.Json;
namespace MVCappCoreWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddUserController : Controller
    {
        
        private readonly UserManager<WebUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        DbContextOptions<MVCappCoreWebContext> _options;
        public AddUserController(UserManager<WebUser> userManager,RoleManager<IdentityRole> roleMgr)
        {
            _userManager = userManager;
            roleManager = roleMgr;
        }       
        public async Task<IActionResult> IndexAsync()
        {
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
                        if (childoib== parenoib)
                        {
                            list.Add(user);
                        }
                    }
                   
                   
                }

            }
            Klijent klijent = GenericsBack.GetKlijent(parenoib);

            ViewBag.Tvrtka ="Naziv:" + klijent.ImeTvrtke +" Oib:" + klijent.OIB + " Tip:" + klijent.Tip;
            ViewBag.CategoriesList = new SelectList(list, "Id", "Email");
            return View();
        }

       // [HttpGet]
        public async Task<IActionResult> getClaims(string id)
        {
            WebUser User = await _userManager.FindByIdAsync(id);
            IdentityRole rola = await roleManager.FindByIdAsync(id);
            ClaimsPrincipal clmp = new ClaimsPrincipal();
            var claim = clmp.Identity;

            return View(User.FirstName);
        }
      
        [HttpGet]
        public async Task<ActionResult> getClaimsAsync(string id)
        {
            List<Claim> claims = new List<Claim>();
            try
            {
                WebUser korisnik = await _userManager.FindByIdAsync(id);
                claims = _userManager.GetClaimsAsync(korisnik).Result.ToList();


            }
            catch (Exception ex)
            {

               // log ex
            }
            ViewBag.Poruka = id;
            return PartialView("../Forms/LocalClaims", claims);
        }

        public async Task<IActionResult> Create(string claimType, string claimValue, string id)
        {
            WebUser korisnik = await _userManager.FindByIdAsync(id);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await _userManager.AddClaimAsync(korisnik, claim);
            var values = new[] { "True", "111" };
            var json = JsonConvert.SerializeObject(values);
            return Json(values);
        }
    }
}
