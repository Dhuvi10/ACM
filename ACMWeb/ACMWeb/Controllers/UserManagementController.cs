using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACMWeb.Models;
using ACMWeb.Models.AccountViewModels;
using ACMWeb.Models.UserViewModel;
using ACMWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACMWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        public UserManagementController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
           RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public async Task<IActionResult> PartialList()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            var user = await _userManager.GetUsersInRoleAsync("Admin");
                
            foreach (var item in user)
            {
                UserViewModel model = new UserViewModel();
                model.Name = item.Name;
                model.Email = item.Email;
                model.id = item.Id;
                model.suspened = item.LockoutEnabled;
                userList.Add(model);
            }
            return PartialView("_index", userList);
        }
        public IActionResult CreateAdminUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdminUser(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                   // var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                   // await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    var role = _roleManager.Roles.Where(e => e.Name == "Admin").FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, role.Name);
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    //return Redirect("/UserMangement/Index");
                    return RedirectToAction(nameof(UserManagementController.Index), "UserManagement");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IActionResult EditAdminUser(string id)
        {
            var storeUser = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
            RegisterViewModel model = new RegisterViewModel();
            model.id = storeUser.Id;
            model.Name = storeUser.Name;
            model.Email = storeUser.Email;
            //  usr.LockoutEnabled = Suspend;
            return PartialView("_EditAdminUser", model);
        }
        public IActionResult ManageStore()
        {

            return View();
        }
        public async Task<IActionResult> ManageStorePartialList()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            var user = await _userManager.GetUsersInRoleAsync("Store");

            foreach (var item in user)
            {
                UserViewModel model = new UserViewModel();
                model.Name = item.Name;
                model.Email = item.Email;
                model.id = item.Id;
                model.suspened = item.LockoutEnabled;
                userList.Add(model);
            }
            return PartialView("_ManageStore", userList);
        }
        public IActionResult CreateStoreUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreUser(RegisterStoreViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                if (string.IsNullOrWhiteSpace(model.id))

                {
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        // var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        // await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                        var role = _roleManager.Roles.Where(e => e.Name == "Store").FirstOrDefault();
                        await _userManager.AddToRoleAsync(user, role.Name);
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created a new account with password.");
                        //return Redirect("/UserMangement/Index");
                        return RedirectToAction(nameof(UserManagementController.ManageStore), "UserManagement");
                    }
                }
                else
                {
                    var storeUser = _userManager.Users.Where(e => e.Id == model.id).FirstOrDefault();
                    storeUser.Name = model.Name;
                    storeUser.Email = model.Email;
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.UpdateAsync(storeUser);
                    await _userManager.ResetPasswordAsync(user, code, model.Password);

                    return RedirectToAction(nameof(UserManagementController.ManageStore), "UserManagement");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IActionResult EditStore(string id)
        {
            var storeUser = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
            RegisterStoreViewModel model = new RegisterStoreViewModel();
            model.id = storeUser.Id;
            model.Name = storeUser.Name;
            model.Email = storeUser.Email;
            //  usr.LockoutEnabled = Suspend;
            return PartialView("_EditStore", model);
        }
        public IActionResult ManageStoreLogo()
        {
            return View();
        }
        public IActionResult ManageStoreContracts()
        {
            return View();
        }
        public IActionResult PassswordView(string id, string status)
        {
            ViewData["id"] = id;
            ViewData["status"] = status;
            return PartialView("_PasswordView");
            
        }
        [HttpPost]
        public async Task<IActionResult> PassswordView(string id, string status, string password)
        {
            var Suspend = status == "Active" ? false : true;
            var usr = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
            var result = await _signInManager.PasswordSignInAsync(usr.Email, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                usr.LockoutEnabled = Suspend;
                await _userManager.UpdateAsync(usr);
                List<UserViewModel> userList = new List<UserViewModel>();
                var user = _userManager.Users.ToList();
                foreach (var item in user)
                {
                    UserViewModel model = new UserViewModel();
                    model.Name = item.Name;
                    model.Email = item.Email;
                    model.id = item.Id;
                    model.suspened = item.LockoutEnabled;
                    userList.Add(model);
                }
                return PartialView("_index", userList);
            }
            else
            {
                ViewData["id"] = id;
                ViewData["status"] = status;
                ViewData["tryagain"] = "tryagain";
                return Json("false");
               // return PartialView("_PasswordView");
            }
            
           
           
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