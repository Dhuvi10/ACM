using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
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
        public ManageStoreInfoController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IManageStore manageStore)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _manageStore = manageStore; ;
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
                return View( test.Data);
            }
            else
            {
                return View();
            }
        }
        public ActionResult SaveImage(IFormFile file, int cid, string ImgName, string ImgStr)
        {
            try
            {
                ImgStr = ImgStr.Split(',')[1];
                if (file == null)
                    return Content("file not selected");

                // var listContacts = new List<Contacts>();
                string fileExtension = Path.GetExtension(ImgName).ToLower();
                var directoryPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads");
                var fullPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", ImgName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string imageName = ImgName;

                //set the image path
                string imgPath = Path.Combine(fullPath, imageName);

                byte[] imageBytes = Convert.FromBase64String(ImgStr);

                MemoryStream ms = new MemoryStream(imageBytes);
                Image image = Image.FromStream(ms);
                image.Save(@"E:\2018\Alisa\Lean\Web\Uploads\" + ImgName);
                //System.IO.Stream fs = FileUpload1.PostedFile.InputStream;
                //System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);


                return Json(new { isSuccess = true, data = "", message = "file upload successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, data = new List<object>(), message = ex.Message });
                throw ex;
            }


        }
    }
}