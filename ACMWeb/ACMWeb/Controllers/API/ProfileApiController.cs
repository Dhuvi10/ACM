using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACMWeb.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ACMCheckInProfile")]
    public class ProfileApiController : ControllerBase
    {
        private readonly IProfileManager _profileManager;
        private IHostingEnvironment _env;
        public ProfileApiController(IProfileManager profileManager, IHostingEnvironment env)
        {
            _profileManager = profileManager;
            _env = env;
        }
        [HttpPost]
        [Route("AddCheckInProfile")]
        public IActionResult AddProfile([FromBody]ProfileViewModel model)
        {
            var webRoot = _env.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
            string photoPath = System.IO.Path.Combine(webRoot, "Photo");
            string signPath = System.IO.Path.Combine(webRoot, "Sign");
            var result = _profileManager.AddProfileDetail(model, photoPath, signPath);
            if (result.Status)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("UpdateCheckInProfile")]
        public IActionResult UpdateProfile([FromBody]ProfileViewModel model)
        {
            var webRoot = _env.WebRootPath;
            //var path = System.IO.Path.Combine(webRoot, "TestFiles");
            string photoPath = System.IO.Path.Combine(webRoot, "Photo");
            string signPath = System.IO.Path.Combine(webRoot, "Sign");
            var result = _profileManager.AddProfileDetail(model, photoPath, signPath);
            if (result.Status)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("CheckInProfile/{checkInId}")]
        public IActionResult GetCheckInProfile(long checkInId)
        {
            var result = _profileManager.ProfileDetailById(checkInId);
            if (result.Status)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }
    }
}