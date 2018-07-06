using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
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
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment _env;
        public ManageStoreApiController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IUserManager manager, IHostingEnvironment env,
        RoleManager<IdentityRole> roleManager, JwtAuthentication JwtAuthentication, IStoreManager storeManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _JwtAuthentication = JwtAuthentication;
            _manager = manager;
            _storeManager = storeManager;
            _env = env;
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
            catch (Exception ex)
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

                foreach (var item in user.Where(e => e.AdminId != null))
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
        [HttpGet]
        [Route("StoreLogo/{storeId}")]
        public IActionResult GetStoreLogo(string storeId)
        {
            var result = _storeManager.StoreLogo(storeId);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpPost]
        [Route("StoreLogo")]
        public IActionResult StoreLogo([FromBody]StoreInfoViewModel model)
        {
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = System.IO.Path.Combine(webRoot, "StoreLogo");
            //model.Logo = model.Logo.Split(',')[1];
            if (string.IsNullOrWhiteSpace(model.Logo))
                return Content("file not selected");


            var result = _storeManager.SaveStoreLogo(model);
            if (result.Status)
            {

                byte[] imageBytes = Convert.FromBase64String(model.Logo);

                MemoryStream ms = new MemoryStream(imageBytes);
                Image image = Image.FromStream(ms);
                image.Save(PathWithFolderName + "//" + model.LogoName);
                return Ok(result);
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpPost]
        [Route("SaveCheckInForm")]
        public IActionResult SaveCheckInForm([FromBody]CheckInContractsViewModel model)
        {
            var result = _storeManager.SaveCheckInForm(model);
            // CheckInContractsViewModel obj= result.Data
            if (result.Status)
            {
                return Ok(new
                {
                    status = result.Status,
                    Data = new { StoreId = result.Data.StoreId, Id = result.Data.Id }
                });
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpGet]
        [Route("CheckInContractListing")]
        public IActionResult CheckInContractListing()
        {
            var result = _storeManager.ManageContractList(false);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpGet]
        [Route("History")]
        public IActionResult CheckOutHistory()
        {
            var result = _storeManager.ManageContractList(true);
            if (result.Status)
            {
                return Ok(new { status = result.Status, Data = result.Data.OrderByDescending(x => x.CheckOutDate) });
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpGet]
        [Route("Checkout/{checkInId}")]
        public IActionResult CheckOutForm(int checkInId)
        {
            var result = _storeManager.CheckOutForm(checkInId);
            if (result.Status)
            {
                return Ok(new { Status = result.Status, Message = result.Message, Data = new { Id = result.Data.Id, CheckOutDate = result.Data.CheckOutDate } });
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpGet]
        [Route("CheckInContractDetail/{contractId}")]
        public IActionResult CheckInContractDetail(int contractId)
        {
            var result = _storeManager.CheckInContractDetail(contractId);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(404, result);
            }
        }
        [HttpGet]
        [Route("CheckInContractListByStore")]
        public IActionResult CheckInContractListByStore(string storeId)
        {
            var result = _storeManager.ContractListByStore(storeId);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(404, result);
            }
        }

    }
}