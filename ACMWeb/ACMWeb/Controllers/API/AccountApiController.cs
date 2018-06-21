using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalWeb.Filters;

namespace ACMWeb.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        JwtAuthentication _JwtAuthentication;
        public AccountApiController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager, JwtAuthentication JwtAuthentication)
        {
            _userManager = userManager;
            _signInManager = signInManager;           
            _logger = logger;
            _roleManager = roleManager;
            _JwtAuthentication = JwtAuthentication;
        }
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {          
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //return Redirect("UserMangement");
                    var role = _userManager.IsInRoleAsync(_userManager.Users.Where(e => e.Email == model.Email).FirstOrDefault(), "Admin").Result;
                    if (role)
                    {
                        //return RedirectToAction(nameof(UserManagementController.Index), "UserManagement");
                        return Ok(new { Result = "UserManagementIndex" });
                    }
                    else
                    {
                        //return RedirectToAction(nameof(UserManagementController.ManageStore), "UserManagement");
                        return Ok(new { Result = "UserManagementStore" });
                    }
                }
                if (result.RequiresTwoFactor)
                {
                   // return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                    return Ok(new { Result = "LoginWith2fa" });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    //return RedirectToAction(nameof(Lockout));
                    return Ok(new { Result = "Lockout" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(model); 
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(model);
        }
    }
}