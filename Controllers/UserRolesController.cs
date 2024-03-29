﻿using MVCappCoreWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.Models;
using System;

namespace MVCappCoreWeb.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly SignInManager<WebUser> _signInManager;
        private readonly UserManager<WebUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<WebUser> userManager, SignInManager<WebUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //private readonly SignInManager<WebUser> _signInManager;
        //private readonly UserManager<WebUser> _userManager;
        //private readonly RoleManager<WebUser> _roleManager;

        //public UserRolesController(UserManager<WebUser> userManager, SignInManager<WebUser> signInManager, RoleManager<WebUser> roleManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _roleManager = roleManager;
        //}
        public async Task<IActionResult> Index(string userId, string poruka = null)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in _roleManager.Roles)
            {
                if(role.Name != "SuperAdmin" && role.Name != "Admin")
                    {
                        var userRolesViewModel = new UserRolesViewModel
                        {
                            RoleName = role.Name
                        };
                        if (await _userManager.IsInRoleAsync(user, role.Name))
                        {
                    
                                userRolesViewModel.Selected = true;
                                 
                        }
                        else
                        {
                            userRolesViewModel.Selected = false;
                        }
                        viewModel.Add(userRolesViewModel);

                }

            }
            var model = new ManageUserRolesViewModel()
            {
                UserId = userId,
                UserRoles = viewModel
            };
            if(poruka != null)
            {
                ViewBag.FeedBack = poruka;
            }
            @ViewBag.User = user.Email;
            return View(model);
        }

    
        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
          String strMessage = string.Empty;
            try
                {

                // var user = await _userManager.FindByIdAsync(id);
                // var roles = await _userManager.GetRolesAsync(user);
                // var result = await _userManager.RemoveFromRolesAsync(user, roles);
                // result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
                //// var currentUser = await _userManager.GetUserAsync(user);
                //// await _signInManager.RefreshSignInAsync(currentUser);
                //// await Seeds.DefaultUsers.SeedSuperAdminAsync(_userManager, _roleManager);
                // return RedirectToAction("Index", new { userId = id });
                // strMessage = "Korisniku uspješno dodane uloge.";
                //return RedirectToAction("Index", new { userId = id, poruka = strMessage });
                // // return View();

                var user = await _userManager.FindByIdAsync(id);
                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
                var currentUser = await _userManager.GetUserAsync(User);
                await _signInManager.RefreshSignInAsync(currentUser);
                strMessage = "Spremljeno !";
                return RedirectToAction("Index", new { userId = id, poruka = strMessage });            
            }
            catch (System.Exception)
            {
                strMessage = "Pogreška !";
                return RedirectToAction("Index", new { userId = id, poruka = strMessage });
            }
           
        }

    }
}
