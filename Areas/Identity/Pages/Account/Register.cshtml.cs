using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MVCappCoreWeb.Areas.Identity.Data;
using MVCappCoreWeb.Helpers;
using Microsoft.Extensions.Options;


namespace MVCappCoreWeb.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<WebUser> _signInManager;
        private readonly UserManager<WebUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<WebUser> userManager,
            SignInManager<WebUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string Oib { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            WebUser useradmin = await _userManager.GetUserAsync(HttpContext.User);
            var parenoib = _userManager.GetClaimsAsync(useradmin).Result.SingleOrDefault(r => r.Type == "Oib").Value;
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new WebUser { UserName = Input.Email, Email = Input.Email , FirstName= Input.FirstName, LastName=Input.LastName,Oib= parenoib };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    WebUser usernew = await _userManager.FindByNameAsync(Input.Email);
                    await _userManager.AddToRoleAsync(usernew, "Korisnik");


                    /****************************************************/

                   // WebUser user = await _userManager.GetUserAsync(HttpContext.User);
                    Claim claim = new Claim("Oib", parenoib, ClaimValueTypes.String);
                    IdentityResult result11 = await _userManager.AddClaimAsync(usernew, claim);




                    /****************************************************/
                    //IdentityRole role = await _roleManager.FindByNameAsync("Korisnik");
                    //var identity = await _signInManager.RefreshSignInAsync(usernew);
                    //Claim claim = new Claim("Oib", parenoib);
                    //await _roleManager.AddClaimAsync(role, claim);
                    //var identity = await base.GenerateClaimsAsync(user);
                    //usernew.AddClaim(new Claim("Oib", user.FirstName ?? ""));

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
     
    }
}
