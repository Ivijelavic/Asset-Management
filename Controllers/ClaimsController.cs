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
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCappCoreWeb.Data;
using MVCappCoreWeb.Helpers;
using MVCappCoreWeb.Constants;

namespace MVCappCoreWeb.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<WebUser> userManager;
        List<WebUser> listUsers;
      
        public ClaimsController(UserManager<WebUser> userMgr,RoleManager<IdentityRole> roleMgr)
        {

            roleManager = roleMgr;
            userManager = userMgr;
            listUsers = userMgr.Users.ToList();
        }

        public async Task<IActionResult> Index(string userId, string select=null,string poruka=null)
        {
            IdentityRole role = null;
            WebUser _user = await userManager.FindByIdAsync(userId);
            List <IdentityRole> list = new List<IdentityRole>();            
            var rolesForUser = await userManager.GetRolesAsync(_user);
            foreach (var item in rolesForUser)
            {
                role = await roleManager.FindByNameAsync(item);
                list.Add(role);
            }
            /***********************************************************************/
            if (select == null) select = list[0].Id;
             IdentityRole rola = await roleManager.FindByIdAsync(select);
            List<Claim> claims = new List<Claim>();
            //Claim claim = null;

            //var roleClaims = roleManager.GetClaimsAsync(role).Result;
            //if (roleClaims != null && roleClaims.Count() > 0)
            //{
            //    foreach (var claimitem in roleClaims)
            //    {
            //        claim = claimitem;
            //        claims.Add(claim);
            //    }
            //}
            /***************************************************************************/


            
            @ViewBag.Message = _user.Email;
            TempData["UserId"] = userId;
            @ViewBag.RolaSelect = rola.Name;
            ViewBag.FeedBack = poruka;
            ViewBag.CategoriesList = new SelectList(list, "Id", "Name");
            return View(claims);
        }

        public async Task<ActionResult> Edit(string idRola, string idUser)
        {
            // List<Claim> cleme = getClaimsAsync(idRola, idUser);
            //return Json(idRola+ idUser);
            // return RedirectToAction("Index", new { userId = idUser, select = idRola });
            /*
            IdentityRole rola = await roleManager.FindByIdAsync(idRola);
            List<Claim> claims = new List<Claim>();
            Claim claim = null;

            var roleClaims = roleManager.GetClaimsAsync(rola).Result;
            if (roleClaims != null && roleClaims.Count() > 0)
            {
                foreach (var claimitem in roleClaims)
                {
                    claim = claimitem;
                    claims.Add(claim);
                }
            }*/
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.Asset), idRola);
            var role = await roleManager.FindByIdAsync(idRola);
            model.RoleId = idRola;
            var claims = await roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            @ViewBag.idUser = idUser;
            return PartialView("../Forms/ClaimsForUser", model);
        }
        public async Task<List<Claim>> getClaimsAsync(string idRola, string idUser)
        { 
            List<Claim> claims = new List<Claim>();
            try
            {
                IdentityRole rola = await roleManager.FindByIdAsync(idRola);


            }
            catch (Exception ex)
            {

                throw;
            }
            return claims;
        }
        public ViewResult Create() => View();

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue)
        {
            WebUser user = await userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await userManager.AddClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View();
        }
        [HttpPost]  
        public async Task<IActionResult> Delete(string claimValues)
        {
            WebUser user = await userManager.GetUserAsync(HttpContext.User);

            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];

            Claim claim = User.Claims.Where(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer).FirstOrDefault();

            IdentityResult result = await userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);

            return View("Index");
        }
        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public IActionResult Users()
        {
            


            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
              listUsers = userManager.Users.ToList();
            //   var query = from listUsers in listUsers join user in Users on listUsers.Id equals user.Id where listUsers.ClaimType == "Oib" select user;
            //  IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;
            foreach (WebUser user in listUsers)
            {
               

               // var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            }
           // List<Ugovori> ugovori = GenericsBack.GetUgovoriList(principal.ToString());
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Update(PermissionViewModel model,String Id=null)
        {
            String strMessage = string.Empty;
            try
            {
                var role = await roleManager.FindByIdAsync(model.RoleId);
                var claims = await roleManager.GetClaimsAsync(role);
                foreach (var claim in claims)
                {
                    await roleManager.RemoveClaimAsync(role, claim);
                }
                var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await roleManager.AddPermissionClaim(role, claim.Value);
                }
                strMessage = "Ovlasti uspješno snimljene";
               
                return RedirectToAction("Index", new { userId = Id, select = model.RoleId, poruka = strMessage });
            }
            catch (Exception ex)
            {
                strMessage = "Pogreška prilikom spremanja ovlasti!";

                return RedirectToAction("Index", new { userId = Id, select = model.RoleId,poruka= strMessage });
            }
        }

    }
}
