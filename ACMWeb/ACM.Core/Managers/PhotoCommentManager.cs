using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ACM.Core.Managers
{
    public class PhotoCommentManager : IPhotoCommentManager
    {
        private ACMContext acmContext;

        public PhotoCommentManager(ACMContext _acmContext)
        {
            acmContext = _acmContext;
        }
        public ResponseModel<string> AddPhotoComment(PhotoCommentModel model)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            try
            {
                PhotoComment _comment = new PhotoComment();
                _comment.PhotoId = model.PhotoId;
                _comment.IsActive = true;
                _comment.CreatedOn = DateTime.Now;
                _comment.Comment = model.Comment;
                acmContext.PhotoComment.Add(_comment);
                acmContext.SaveChanges();
                response.Status = true;
                response.Message = "success";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseModel<List<PhotoCommentModel>> PhotoComments(long photoId)
        {
            ResponseModel<List<PhotoCommentModel>> response = new ResponseModel<List<PhotoCommentModel>>() { Data = new List<PhotoCommentModel>() };
            try
            {
                var list = acmContext.PhotoComment.Where(x => x.PhotoId == photoId).Select(x => new PhotoCommentModel {
                    Id=x.Id,
                    Comment = x.Comment,
                    CreatedOn=x.CreatedOn??DateTime.Now,
                    PhotoId=x.PhotoId??0
                    
                }).ToList();

                response.Data = list;
                response.Status = true;
                response.Message = "success";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
