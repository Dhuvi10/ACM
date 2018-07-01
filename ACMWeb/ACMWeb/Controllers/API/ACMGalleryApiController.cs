using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ACMWeb.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ACMGallery")]
   // [ApiController]
    public class ACMGalleryApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserManager _manager;
        private readonly IGalleryManager _galleryManager;
        private IHostingEnvironment _env;
        public ACMGalleryApiController(IGalleryManager galleryManager, IHostingEnvironment env)
        {
            _galleryManager = galleryManager;
            _env = env;
        }
        [HttpPost]
        [Route("AddGalleryImage")]
        public IActionResult AddGalleryImage([FromBody]GalleryViewModel model)
        {
            var webRoot = _env.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
            string serverPath = System.IO.Path.Combine(webRoot, "Gallery");
            string thumbPath = System.IO.Path.Combine(webRoot, "Thumbnail");
            _galleryManager.AddGalleryImage(model, serverPath, thumbPath);
            return Ok();
        }
    }
}