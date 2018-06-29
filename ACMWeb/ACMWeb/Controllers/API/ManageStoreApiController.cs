using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using ACM.Core.Models.CheckInContractViewModels;
using ACM.Core.Models.UserViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalWeb.Filters;

namespace ACMWeb.Controllers.API
{
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Store")]
    public class ManageStoreApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserManager _manager;
        private readonly IStoreManager _storeManager;
        JwtAuthentication _JwtAuthentication;
        public ManageStoreApiController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IUserManager manager,
            RoleManager<IdentityRole> roleManager, JwtAuthentication JwtAuthentication,IStoreManager storeManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _JwtAuthentication = JwtAuthentication;
            _manager = manager;
            _storeManager = storeManager;
        }
        [HttpGet]
        [Route("AdminList")]
        public async Task<IActionResult> AdminList()
        {
            try
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
            catch(Exception ex)
            {
                return StatusCode(404, new { Status = false, message = ex.Message });
            }
            //if (!string.IsNullOrWhiteSpace(id))
            //{
            //    List<UserViewModel> userList = new List<UserViewModel>();
            //    var user = await _userManager.GetUsersInRoleAsync("Admin");

            //    foreach (var item in user)
            //    {
            //        UserViewModel model = new UserViewModel();
            //        model.Name = item.Name;
            //        model.Email = item.Email;
            //        model.id = item.Id;
            //        model.suspened = item.LockoutEnabled;
            //        userList.Add(model);
            //    }
            //    return Ok(new { Status = true, AdminUser = userList });
            //}
            //else
            //{
            //    return StatusCode(404, new { Status = false, message = "Invalid  Details" });
            //}

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
        [Route("SaveCheckInForm")]
        public IActionResult SaveCheckInForm([FromBody]CheckInContractsViewModel model)
        {
            return Ok();
            //var result = _storeManager.SaveCheckInContract(model);
            //if (result.Status)
            //{ return Ok(result.Message); }
            //else
            //{
            //   return BadRequest(result.Message);
            //}
        }
       
    }
}