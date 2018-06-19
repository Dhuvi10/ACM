using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACMWeb.Models;
using ACMWeb.Models.RoleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ACMWeb.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        public RolesController(RoleManager<IdentityRole> roleManager, ILogger<RolesController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Add(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.Name, NormalizedName = "",ConcurrencyStamp="" };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New role  created");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                   // _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
       // [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public IActionResult AssignRole(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            RoleViewModel model = new RoleViewModel();
            model.userList = _userManager.Users.Select(e => new SelectListItem { Text = e.Name, Value = e.Id }).ToList();
            model.roleList = _roleManager.Roles.Select(e => new SelectListItem { Text = e.Name, Value = e.Id }).ToList();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleViewModel model,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            //if (ModelState.IsValid)
            //{
                var user = _userManager.Users.Where(e => e.Id == model.User).FirstOrDefault();
            var role = _roleManager.Roles.Where(e => e.Id == model.Role).FirstOrDefault();
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New role  created");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    // _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
               // }
               // AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}