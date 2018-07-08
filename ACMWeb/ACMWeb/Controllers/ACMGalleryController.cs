using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using ACM.Core.Models.CheckInContractViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ACMWeb.Controllers
{
    public class ACMGalleryController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStoreManager _storeManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGalleryManager _galleryManager;
        string webRootPath = "";

        public ACMGalleryController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IStoreManager storeManager,
            IHostingEnvironment hostingEnvironment, IGalleryManager galleryManager
)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _storeManager = storeManager;
            _hostingEnvironment = hostingEnvironment;
            webRootPath = _hostingEnvironment.WebRootPath;
            _galleryManager = galleryManager;

        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        public IActionResult Index(int? id)
        {
            ViewData["id"] = id.Value;
            return View();
        }
        public IActionResult GalleryList(int? id)
        {
            var result = _galleryManager.GalleryByCheckinForm(id.Value);
            if (result.Data.Count > 0)
            {
                return PartialView("_index",result.Data);
            }
            else
            {
                return Json(result);
            }


        }
        public IActionResult AddGallery(int? id)
        {

            GalleryViewModel model = new GalleryViewModel();
            model.CheckInId = id.Value;
            return View(model);


        }
        [HttpPost]
        public IActionResult AddGallery(IList<IFormFile> files, long CheckInId)
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
            string serverPath = System.IO.Path.Combine(webRoot, "Gallery");
            string thumbPath = System.IO.Path.Combine(webRoot, "Thumbnail");
            List<GalleryViewModel> models = new List<GalleryViewModel>();
          
            foreach (var file in files)
            {
                GalleryViewModel model = new GalleryViewModel();
                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        model.Image = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                    }
                }
                model.StoreId = GetCurrentUserId().Result;
                model.FileName = this.EnsureCorrectFilename(filename);
                model.CheckInId = CheckInId;
                models.Add(model);
            }
            var result = _galleryManager.WebAddMultipleImages(models, serverPath, thumbPath);
            if (result.Status)
                return Json(new { data = result.Data });
            else
            {

                return Json(new { data = result.Data });
            }
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
    }
}