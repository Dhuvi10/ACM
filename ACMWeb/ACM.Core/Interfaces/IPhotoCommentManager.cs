using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Interfaces
{
   public interface IPhotoCommentManager
    {
        ResponseModel<string> AddPhotoComment(PhotoCommentModel model);
        ResponseModel<List<PhotoCommentModel>> PhotoComments(long photoId);
    }
}
