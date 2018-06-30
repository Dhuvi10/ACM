using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using ACM.Core.Models.UserViewModel;
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
        private readonly IStoreManager _storeManager;

        JwtAuthentication _JwtAuthentication;
        public AccountApiController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager, JwtAuthentication JwtAuthentication, IStoreManager storeManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _JwtAuthentication = JwtAuthentication;
            _storeManager = storeManager;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginAPI([FromBody]LoginViewModel model)
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
                            List<UserViewModel> userList = new List<UserViewModel>();
                            var usr =  _userManager.GetUsersInRoleAsync("Store").Result;
                            var logoList = _storeManager.StoreLogoList();
                            foreach (var item in usr.Where(e => e.AdminId == user.Id))
                            {
                                UserViewModel _model = new UserViewModel();
                                _model.Name = item.Name;
                                _model.Email = item.Email;
                                _model.id = item.Id;
                                _model.suspened = item.LockoutEnabled;
                                _model.Logo = logoList==null?null: logoList.Data.Where(k => k.StoreId == item.Id).FirstOrDefault().Logo;
                                userList.Add(_model);
                            }
                            //return RedirectToAction(nameof(UserManagementController.Index), "UserManagement");
                            return Ok(new { Status = true, id= user.Id,Name=user.Name,role = "Admin", message = "Login Sucessfully", storeUserList= userList });
                        }
                        else
                        {
                            //return RedirectToAction(nameof(UserManagementController.ManageStore), "UserManagement");
                            return Ok(new { Status = true, id = user.Id, role = "Store", message = "Login Sucessfully" });
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
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        [NonAction]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        [HttpPost]
        [Route("StoreLogin")]
        public async Task<IActionResult> LoginStoreUserAPI(string id)
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