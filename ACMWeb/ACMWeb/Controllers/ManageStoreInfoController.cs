using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
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
        private readonly IManageStore _manageStore;
        private readonly IHostingEnvironment _hostingEnvironment;
        string webRootPath = "";

        public ManageStoreInfoController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IManageStore manageStore,
            IHostingEnvironment hostingEnvironment
)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _manageStore = manageStore; 
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
            var test = _manageStore.StoreLogo(GetCurrentUserId().Result);
            if (test.Status )
            {
                ViewData["Url"] = System.IO.Path.Combine(webRootPath, "StoreLogo")+"\\";
                return View( test.Data);
            }
            else
            {
                var model = new StoreInfoViewModel();
                model.StoerId = GetCurrentUserId().Result;
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
                var result = _manageStore.SaveStoreLogo(model);
                if (result.Status)
                {
                   
                    byte[] imageBytes = Convert.FromBase64String(model.Logo);

                    MemoryStream ms = new MemoryStream(imageBytes);
                    Image image = Image.FromStream(ms);
                    image.Save(PathWithFolderName+"//"+model.LogoName);
                    return RedirectToAction("index");

                }
                else
                {
                    return View();
                }
               
            }
            catch (Exception ex)
            {

                return View("Error", new ErrorViewModel{ RequestId = ex.Message });
            }


        }
    }
}