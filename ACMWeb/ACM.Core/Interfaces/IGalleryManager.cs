using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Interfaces
{
   public interface IGalleryManager
    {
        ResponseModel<string> AddGalleryImage(GalleryViewModel model);
        ResponseModel<string> AddMultipleImages(List<GalleryViewModel> model);
        ResponseModel<string> SetFrontImage(int Id, string storeId);
        ResponseModel<List<GalleryViewModel>> GalleryByStore(string storeId);
        ResponseModel<string> DeleteImages(List<long> Ids);
        
    }
}
