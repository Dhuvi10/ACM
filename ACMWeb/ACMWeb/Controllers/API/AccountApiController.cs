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
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    var user = _userManager.Users.Where(e => e.Email == model.Email).FirstOrDefault();
                    if (user.LockoutEnabled)
                    {
                        _logger.LogWarning("User account locked out.");
                        //return RedirectToAction(nameof(Lockout));



                        _logger.LogInformation("User logged in.");
                        //return Redirect("UserMangement");
                        var role = _userManager.IsInRoleAsync(_userManager.Users.Where(e => e.Email == model.Email).FirstOrDefault(), "Admin").Result;
                        if (role)
                        {
                            //return RedirectToAction(nameof(UserManagementController.Index), "UserManagement");
                            return Ok(new { Status = true, id= user.Id,role = "Admin", token = _JwtAuthentication.BuildToken(model.Email, user.Name), message = "Login Sucessfully" });
                        }
                        else
                        {
                            //return RedirectToAction(nameof(UserManagementController.ManageStore), "UserManagement");
                            return Ok(new { Status = true, id = user.Id, role = "Store", token = _JwtAuthentication.BuildToken(model.Email, user.Name), message = "Login Sucessfully" });
                        }

                    }
                    else
                    {

                        return StatusCode(401, new { Status = false, role = "Store", token = "", message = "Invalid Login Details" });
                    }
                }
                else
                {
                    return StatusCode(401, new { Status = false, role = "Store", token = "", message = "Account Locked Please contact Acm Admin" });
                }
            }

            // If we got this far, something failed, redisplay form
            return StatusCode(401, new { Status = false, role = "", token = "", message = "please fill the required details" });
        }

        [HttpPost]
        [Route("StoreLogin")]
        public async Task<IActionResult> LoginStoreUser(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var storeUser = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
                if (storeUser.LockoutEnabled)
                {
                    //  await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(storeUser, isPersistent: false);
                    return Ok(new { Status = true,id= storeUser.Id, role = "Admin", token = _JwtAuthentication.BuildToken(storeUser.Email, storeUser.Name), message = "Login Sucessfully" });
                }
                else
                {
                    return StatusCode(401, new { Status = false, id = storeUser.Id, role = "Store", token = "", message = "Account Locked Please contact Acm Admin" });
                }
            }
            else
            {
                return StatusCode(401, new { Status = false, role = "", token = "", message = "please fill the required details" });
            }
        }
    }
}