using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.Models;
using MVCappCoreWeb.Helpers;
using MVCappCoreWeb.Constants;
namespace MVCappCoreWeb.Controllers
{
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<ActionResult> Index(string roleId,string idUser, string poruka = null)
        {
            IdentityRole Irole = await _roleManager.FindByIdAsync(roleId);
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.Asset), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
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
            ViewBag.Message = "Uloga: " + Irole.Name;
            ViewBag.FeedBack = poruka;
            return View(model);
        }
        //public async Task<ActionResult> Index(string roleId,string poruka=null)
        //{
        //    IdentityRole Irole = await _roleManager.FindByIdAsync(roleId);
        //    //if (poruka != null)
        //    //{
        //    //    poruka = "Uloga: " + Irole.Name;
        //    //}
        //    //else
        //    //{
        //    //    poruka = "Uloga: " + Irole.Name + "    " + poruka;
        //    //}
        //    var model = new PermissionViewModel();
        //    var allPermissions = new List<RoleClaimsViewModel>();
        //    allPermissions.GetPermissions(typeof(Permissions.Asset), roleId);
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    model.RoleId = roleId;
        //    var claims = await _roleManager.GetClaimsAsync(role);
        //    var allClaimValues = allPermissions.Select(a => a.Value).ToList();
        //    var roleClaimValues = claims.Select(a => a.Value).ToList();
        //    var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
        //    foreach (var permission in allPermissions)
        //    {
        //        if (authorizedClaims.Any(a => a == permission.Value))
        //        {
        //            permission.Selected = true;
        //        }
        //    }

        //    model.RoleClaims = allPermissions;
        //    ViewBag.Message = "Uloga: " + Irole.Name; 
        //    ViewBag.FeedBack = poruka;
        //    return View(model);
        //}
        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            String strMessage = string.Empty;
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var claims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in claims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }
                var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await _roleManager.AddPermissionClaim(role, claim.Value);
                }
                strMessage = "Ovlasti uspješno snimljene";
                return RedirectToAction("Index", new { roleId = model.RoleId, poruka = strMessage });
            }
            catch (Exception ex)
            {
                 strMessage = "Pogreška prilikom spremanja ovlasti!";
                return RedirectToAction("Index", new { roleId = model.RoleId, poruka= strMessage});
                //log ex
            }
        }
    }
}


