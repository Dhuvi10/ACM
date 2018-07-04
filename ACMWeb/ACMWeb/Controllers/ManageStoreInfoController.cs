﻿using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using ACM.Core.Models.CheckInContractViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ACMWeb.Controllers
{
    public class ManageStoreInfoController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStoreManager _storeManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        string webRootPath = "";

        public ManageStoreInfoController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IStoreManager storeManager,
            IHostingEnvironment hostingEnvironment
)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _storeManager = storeManager;
            _hostingEnvironment = hostingEnvironment;
            webRootPath = _hostingEnvironment.WebRootPath;

        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        public IActionResult Index()
        {
            //var user = GetCurrentUserId().Result;
            var test = _storeManager.StoreLogo(GetCurrentUserId().Result);
            if (test.Status)
            {
                ViewData["Url"] = System.IO.Path.Combine(webRootPath, "StoreLogo") + "\\";
                return View(test.Data);
            }
            else
            {
                var model = new StoreInfoViewModel();
                model.StoreId = GetCurrentUserId().Result;
                return View(model);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StoreInfoViewModel model)
        {
            try
            {
                var PathWithFolderName = System.IO.Path.Combine(webRootPath, "StoreLogo");
                model.Logo = model.Logo.Split(',')[1];
                if (string.IsNullOrWhiteSpace(model.Logo))
                    return Content("file not selected");

                // var listContacts = new List<Contacts>();
                //string fileExtension = Path.GetExtension(ImgName).ToLower();
                //var directoryPath = Path.Combine(
                //            Directory.GetCurrentDirectory(), "Uploads");
                //var fullPath = Path.Combine(
                //            Directory.GetCurrentDirectory(), "Uploads", ImgName);

                //if (!Directory.Exists(directoryPath))
                //{
                //    Directory.CreateDirectory(directoryPath);
                //}
                //string imageName = ImgName;

                //set the image path
                // string imgPath = Path.Combine(fullPath, imageName);
                var result = _storeManager.SaveStoreLogo(model);
                if (result.Status)
                {

                    byte[] imageBytes = Convert.FromBase64String(model.Logo);

                    MemoryStream ms = new MemoryStream(imageBytes);
                    Image image = Image.FromStream(ms);
                    image.Save(PathWithFolderName + "//" + model.LogoName);
                    return RedirectToAction("index");

                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {

                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }


        }
        public IActionResult ManageStoreContracts()
        {

            var result = _storeManager.EditContract(GetCurrentUserId().Result);
            if (result.Status)
            {
                return View(result.Data);
            }
            else
            {
                return View(new ManageContractViewModel());
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ManageStoreContracts(ManageContractViewModel model)
        {
            model.storeId = GetCurrentUserId().Result;
            var result = _storeManager.SaveContract(model);
            return View();
        }
        public IActionResult CheckInForm(int? id)
        {
            if (id.HasValue)
            {
               var result = _storeManager.CheckInContractDetail(id.Value);
              return View(result.Data);
            }
            else
            {
                return View(new CheckInContractsViewModel());
            }
        }
        [HttpPost]
               
        public IActionResult CheckInForm(CheckInContractsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.StoreId = GetCurrentUserId().Result;
                 var result = _storeManager.SaveCheckInForm(model);
                return View(result.Data);
            }

            else
            {
              //  ModelState.
            }
            return View();
            
        }
        public IActionResult CheckInFormList()
        {
            return View();
        }
        public IActionResult PartialList()
        {

            var result = _storeManager.ManageContractList();
            return PartialView("_CheckIn", result.Data);
        }

    }
}