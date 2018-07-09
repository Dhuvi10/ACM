using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACMWeb.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ACMPhotoComment")]
    public class PhotoCommentApiController : ControllerBase
    {
        private readonly IPhotoCommentManager _commentManager;
        public PhotoCommentApiController(IPhotoCommentManager commentManager)
        {
            _commentManager = commentManager;
        }
        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddProfile([FromBody]PhotoCommentModel model)
        {
            var result = _commentManager.AddPhotoComment(model);
            if (result.Status)
                return Ok(result);
            else
                return StatusCode(404, result);
        }
        [HttpGet]
        [Route("PhotoComment/{photoId}")]
        public IActionResult CommentList(int photoId)
        {
            var result = _commentManager.PhotoComments(photoId);
            if (result.Status)
                return Ok(new { PhotoId=photoId,Comments=result.Data.Select(x=>new { Comment = x.Comment, CreatedOn= x.CreatedOn}).ToList()});
            else
                return StatusCode(404, result);
        }
    }
}