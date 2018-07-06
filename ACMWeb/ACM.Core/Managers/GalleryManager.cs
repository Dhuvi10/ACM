using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.DrawingCore;
using ACM.Core.Utility;
using System.DrawingCore.Imaging;

namespace ACM.Core.Managers
{
    public class GalleryManager : IGalleryManager
    {
        private ACMContext acmContext;

        public GalleryManager(ACMContext _acmContext)
        {
            acmContext = _acmContext;
        }
        public ResponseModel<string> AddGalleryImage(GalleryViewModel model, string serverPath, string thumbPath)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            try
            {
                Gallery gallery = new Gallery();
                gallery.CreatedOn = DateTime.Now;
                gallery.IsActive = true;
                gallery.IsMain = false;
                gallery.StoreId = model.StoreId;

                string FileName = Guid.NewGuid().ToString() + "." + Convert.ToString(model.FileName.Split('.')[1]);
                var path = Path.Combine(serverPath, FileName.ToString());
                byte[] imageBytes = Convert.FromBase64String(model.Image);
                File.WriteAllBytes(path, imageBytes);

                gallery.Image = FileName;
                Image normalImage;
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    normalImage = Image.FromStream(ms);

                    Bitmap bmp = new Bitmap(normalImage);

                    string base64ImageString = bmp.ToBase64String(ImageFormat.Png);
                    Bitmap bmpFromString = base64ImageString.Base64StringToBitmap();

                    string thumbName = Guid.NewGuid().ToString() + "." + Convert.ToString(ImageFormat.Png);
                    var outpath = Path.Combine(thumbPath, thumbName.ToString());
                    bmpFromString.Save(outpath, ImageFormat.Png);
                    // thumbnail.Save(outpath, ImageFormat.Jpeg);
                    gallery.ThumbnailImage = thumbName;
                    
                }
                gallery.CheckInId = model.CheckInId;
                acmContext.Gallery.Add(gallery);
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

        public ResponseModel<string> AddMultipleImages(List<GalleryViewModel> models, string serverPath, string thumbPath)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            try
            {
                foreach (var model in models)
                {
                    Gallery gallery = new Gallery();
                    gallery.CreatedOn = DateTime.Now;
                    gallery.IsActive = true;
                    gallery.IsMain = model.IsMain;
                    gallery.StoreId = model.StoreId;

                    string FileName = Guid.NewGuid().ToString() + "." + Convert.ToString(model.FileName.Split('.')[1]);
                    var path = Path.Combine(serverPath, FileName.ToString());
                    byte[] imageBytes = Convert.FromBase64String(model.Image);
                    File.WriteAllBytes(path, imageBytes);

                    gallery.Image = FileName;
                    //Image normalImage;
                    //using (MemoryStream ms = new MemoryStream(imageBytes))
                    //{
                    //    normalImage = Image.FromStream(ms);
                    //}
                    //Bitmap bmp = new Bitmap(normalImage);

                    //string base64ImageString = bmp.ToBase64String(ImageFormat.Png);
                    //Bitmap bmpFromString = base64ImageString.Base64StringToBitmap();

                    //string thumbName = Guid.NewGuid().ToString() + "." + Convert.ToString(ImageFormat.Png);
                    //var outpath = Path.Combine(thumbPath, thumbName.ToString());
                    //bmpFromString.Save(outpath, ImageFormat.Png);
                    // thumbnail.Save(outpath, ImageFormat.Jpeg);
                    gallery.ThumbnailImage = null;
                    gallery.CheckInId = model.CheckInId;
                    acmContext.Gallery.Add(gallery);
                    
                }
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

        public ResponseModel<string> DeleteImages(List<long> Ids, string serverPath, string thumbPath)
        {
            ResponseModel<string> response = new ResponseModel<string> { Data = "" };
            try
            {
                var removalList = acmContext.Gallery.Where(x => Ids.Contains(x.Id))
                .ToList();
                foreach (var item in removalList)
                {
                    var thumbnailImage = Path.Combine(thumbPath, item.ThumbnailImage.ToString());                   
                    if (System.IO.File.Exists(thumbnailImage))
                    {
                        System.IO.File.Delete(thumbnailImage);
                    }
                    var galleryImage = Path.Combine(thumbPath, item.Image.ToString());
                    if (System.IO.File.Exists(galleryImage))
                    {
                        System.IO.File.Delete(galleryImage);
                    }
                }
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
            ResponseModel<List<GalleryViewModel>> response = new ResponseModel<List<GalleryViewModel>>() { Data = new List<GalleryViewModel>() };
            try
            {
                var model = acmContext.Gallery.Where(x => x.StoreId == storeId).Select(x => new GalleryViewModel
                {
                    StoreId = x.StoreId,
                    CheckInId=x.CheckInId,
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    Image = x.Image,
                    IsActive = x.IsActive,
                    ThumbnailImage = x.ThumbnailImage
                }).ToList();
                response.Data = model;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public ResponseModel<string> SetFrontImage(int Id, string storeId)
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
        public ResponseModel<List<GalleryViewModel>> GalleryByCheckinForm(int checkInId)
        {
            ResponseModel<List<GalleryViewModel>> response = new ResponseModel<List<GalleryViewModel>>() { Data = new List<GalleryViewModel>() };
            try
            {
                var model = acmContext.Gallery.Where(x => x.CheckInId == checkInId).Select(x => new GalleryViewModel
                {
                    StoreId = x.StoreId,
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    Image = x.Image,
                    IsActive = x.IsActive,
                    CheckInId=x.CheckInId,
                    ThumbnailImage = x.ThumbnailImage
                }).ToList();
                response.Data = model;
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
