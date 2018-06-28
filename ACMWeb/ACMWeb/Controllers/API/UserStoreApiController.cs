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
    [Route("api/UserStore")]
    public class UserStoreApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserManager _manager;
        JwtAuthentication _JwtAuthentication;
        public UserStoreApiController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IUserManager manager,
            RoleManager<IdentityRole> roleManager, JwtAuthentication JwtAuthentication)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _JwtAuthentication = JwtAuthentication;
            _manager = manager;
        }
        [HttpGet]
        [Route("AdminList")]
        public async Task<IActionResult> AdminList(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
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
                return Ok(new { Status = true, AdminUser = userList });
            }
            else
            {
                return StatusCode(404, new { Status = false, message = "Invalid  Details" });
            }
            
        }

        [HttpGet]
        [Route("StoreList")]
        public async Task<IActionResult> StoreList(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                List<UserViewModel> userList = new List<UserViewModel>();
                var user = await _userManager.GetUsersInRoleAsync("Store");
               
                foreach (var item in user.Where(e => e.AdminId == id))
                {
                    UserViewModel model = new UserViewModel();
                    model.Name = item.Name;
                    model.Email = item.Email;
                    model.id = item.Id;
                    model.suspened = item.LockoutEnabled;
                    userList.Add(model);
                }
                return Ok(new { Status = true, AdminUser = userList });
            }
            else
            {
                List<UserViewModel> userList = new List<UserViewModel>();
                var user = await _userManager.GetUsersInRoleAsync("Store");

                foreach (var item in user.Where(e => e.AdminId!=null))
                {
                    UserViewModel model = new UserViewModel();
                    model.Name = item.Name;
                    model.Email = item.Email;
                    model.id = item.Id;
                    model.suspened = item.LockoutEnabled;
                    userList.Add(model);
                }
                return Ok(new { Status = true, AdminUser = userList });
            }

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
                    return Ok(new { Status = true, role = "Admin", token = _JwtAuthentication.BuildToken(storeUser.Email, storeUser.Name), message = "Login Sucessfully" });
                }
                else
                {
                    return StatusCode(401, new { Status = true, role = "Store", token = "", message = "Account Locked Please contact Acm Admin" });
                }
            }
            else
            {
                return StatusCode(401, new { Status = true, role = "", token = "", message = "please fill the required details" });
            }
        }
    }
}