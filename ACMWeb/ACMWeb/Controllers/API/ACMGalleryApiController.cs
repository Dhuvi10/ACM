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
            var result = _galleryManager.AddGalleryImage(model, serverPath, thumbPath);
            if (result.Status)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("AddMultipleImages")]
        public IActionResult AddMultipleImages([FromBody]GalleryViewModel[] models)
        {
            var webRoot = _env.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
            string serverPath = System.IO.Path.Combine(webRoot, "Gallery");
            string thumbPath = System.IO.Path.Combine(webRoot, "Thumbnail");
            var result = _galleryManager.AddMultipleImages(models.ToList(), serverPath, thumbPath);
            if (result.Status)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("GalleryByStore/{storeId}")]
        public IActionResult GalleryByStore(string storeId)
        {
           // var webRoot = _env.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
           // string serverPath = System.IO.Path.Combine(webRoot, "Gallery");
           // string thumbPath = System.IO.Path.Combine(webRoot, "Thumbnail");
            var result = _galleryManager.GalleryByStore(storeId);
            if (result.Status)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("DeleteImages")]
        public IActionResult DeleteImages([FromBody]List<long> Ids)
        {
             var webRoot = _env.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
             string serverPath = System.IO.Path.Combine(webRoot, "Gallery");
             string thumbPath = System.IO.Path.Combine(webRoot, "Thumbnail");
            var result = _galleryManager.DeleteImages(Ids, serverPath, thumbPath);
            if (result.Status)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("SetFrontImage/{Id}/{storeId}")]
        public IActionResult SetFrontImage(int Id, string storeId)
        {
            var result = _galleryManager.SetFrontImage(Id,storeId);
            if (result.Status)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("GalleryByCheckIn/{checkInId}")]
        public IActionResult GalleryByCheckIn(int checkInId)
        {

            var result = _galleryManager.GalleryByCheckinForm(checkInId);
            if (result.Status)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }
    }
}