using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ACM.Core.Managers
{
    public class GalleryManager : IGalleryManager
    {
        private ACMContext acmContext;

        public GalleryManager(ACMContext _acmContext)
        {
            acmContext = _acmContext;
        }
        public ResponseModel<string> AddGalleryImage(GalleryViewModel model)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            throw new NotImplementedException();
        }

        public ResponseModel<string> AddMultipleImages(List<GalleryViewModel> models)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            foreach (var item in models)
            {
                //FileName = examDetailModel.ExamSesson.Firstname + "-" + examDetailModel.ExamSesson.Lastname + "_" + Guid.NewGuid().ToString() + "." + Convert.ToString(model.FileName.Split('.')[1]);
                //var path = Path.Combine(Serverpath, FileName.ToString());
                ////Convert Base64 Encoded string to Byte Array.
                //byte[] imageBytes = Convert.FromBase64String(model.Filebase64);
                //File.WriteAllBytes(path, imageBytes);

                //Image image = Image.FromFile(fileName);
                //Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                //thumb.Save(Path.ChangeExtension(fileName, "thumb"));
            }
            throw new NotImplementedException();
        }

        public ResponseModel<string> DeleteImages(List<long> Ids)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            try
            {
                var removalList = acmContext.Gallery.Where(x => Ids.Contains(x.Id))
                .ToList();
                acmContext.Gallery.RemoveRange(removalList);
                response.Message = "success";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public ResponseModel<List<GalleryViewModel>> GalleryByStore(string storeId)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<string> SetFrontImage(int Id,string storeId)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            //var allImages= acmContext.Gallery.Where(x => x.StoreId == storeId).ToList();
            try
            {
                acmContext.Gallery.Where(x => x.StoreId == storeId).ToList()
                .ForEach(a => a.IsMain = false);
                acmContext.SaveChanges();

                var detail = acmContext.Gallery.Where(x => x.Id == Id).FirstOrDefault();
                if (detail != null)
                {
                    detail.IsMain = true;
                }
                acmContext.SaveChanges();
                response.Message = "success";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }
    }
}
