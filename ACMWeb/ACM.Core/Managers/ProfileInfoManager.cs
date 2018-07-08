using ACM.Core.Context;
using ACM.Core.Interfaces;
using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using System.Linq;

namespace ACM.Core.Managers
{
    public class ProfileInfoManager : IProfileManager
    {
        private ACMContext acmContext;
        public ProfileInfoManager(ACMContext _acmContext)
        {
            acmContext = _acmContext;
        }
        public ResponseModel<ProfileViewModel> AddProfileDetail(ProfileViewModel model, string imagePath, string signPath)
        {
            ResponseModel<ProfileViewModel> response = new ResponseModel<ProfileViewModel> { Data = new ProfileViewModel() };
            try
            {
                ProfileInfo info = new ProfileInfo();
                string photoName = null;
                string signName = null;
                info.CreatedOn = DateTime.Now;
                info.IsActive = true;
                info.CheckInId = model.CheckInId;
                if (!string.IsNullOrEmpty(model.Photo))
                {
                    photoName = Guid.NewGuid().ToString() + "." + Convert.ToString(ImageFormat.Jpeg);
                    var path = Path.Combine(imagePath, photoName.ToString());
                    string image64Base = model.Photo.Replace("\r", "").Replace("\n", "");
                    byte[] imageBytes = Convert.FromBase64String(image64Base);
                    File.WriteAllBytes(path, imageBytes);
                }
                info.Photo = photoName;
                if (!string.IsNullOrEmpty(model.Signature))
                {
                    signName = Guid.NewGuid().ToString() + "." + Convert.ToString(ImageFormat.Png);
                    var _signPath = Path.Combine(signPath, signName.ToString());
                    string sign64Base = model.Signature.Replace("\r", "").Replace("\n", "");
                    byte[] _imageBytes = Convert.FromBase64String(sign64Base);
                    File.WriteAllBytes(_signPath, _imageBytes);
                }
                info.Signature = signName;

                acmContext.ProfileInfo.Add(info);
                acmContext.SaveChanges();
                ProfileViewModel _profile = new ProfileViewModel();
                _profile.CheckInId = info.CheckInId;
                _profile.CreatedOn = info.CreatedOn;
                _profile.IsActive = info.IsActive;
                _profile.Photo = info.Photo;
                _profile.Signature = info.Signature;
                response.Data = _profile;
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

        public ResponseModel<ProfileViewModel> ProfileDetailById(long checkInId)
        {
            ResponseModel<ProfileViewModel> response = new ResponseModel<ProfileViewModel> { Data=new ProfileViewModel()};
            try
            {
                var details = acmContext.ProfileInfo.Where(x => x.CheckInId == checkInId).Select(x => new ProfileViewModel {
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    Photo = x.Photo,
                    Signature = x.Signature,
                    CheckInId = x.CheckInId
                }).FirstOrDefault();
                response.Data = details;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseModel<ProfileViewModel> UpdateProfileDetail(ProfileViewModel model, string imagePath, string signPath)
        {
            ResponseModel<ProfileViewModel> response = new ResponseModel<ProfileViewModel> { Data = new ProfileViewModel() };
            try
            {
                var details = acmContext.ProfileInfo.Where(x => x.CheckInId == model.CheckInId).FirstOrDefault();
                if (details != null)
                {
                    details.IsActive = model.IsActive;

                    if (!string.IsNullOrEmpty(model.Photo))
                    {
                        var photoImage = Path.Combine(imagePath, details.Photo.ToString());
                        if (System.IO.File.Exists(photoImage))
                        {
                            System.IO.File.Delete(photoImage);
                        }

                        string photoName = Guid.NewGuid().ToString() + "." + Convert.ToString(ImageFormat.Jpeg);
                        var path = Path.Combine(imagePath, photoName.ToString());
                        string Photo64Base = model.Photo.Replace("\r", "").Replace("\n", "");
                        byte[] imageBytes = Convert.FromBase64String(Photo64Base);
                        File.WriteAllBytes(path, imageBytes);
                        details.Photo = photoName;
                       
                    }
                    if (!string.IsNullOrEmpty(model.Signature))
                    {
                        var signImage = Path.Combine(signPath, details.Signature.ToString());
                        if (System.IO.File.Exists(signImage))
                        {
                            System.IO.File.Delete(signImage);
                        }

                        string signName = Guid.NewGuid().ToString() + "." + Convert.ToString(ImageFormat.Png);
                        var _signPath = Path.Combine(signPath, signName.ToString());
                        string Signature64Base = model.Signature.Replace("\r", "").Replace("\n", "");
                        byte[] _imageBytes = Convert.FromBase64String(Signature64Base);
                        File.WriteAllBytes(_signPath, _imageBytes);
                        details.Photo = signName;
                    }
                    acmContext.SaveChanges();
                    ProfileViewModel _profile = new ProfileViewModel();
                    _profile.CheckInId = details.CheckInId;
                    _profile.CreatedOn = details.CreatedOn;
                    _profile.IsActive = details.IsActive;
                    _profile.Photo = details.Photo;
                    _profile.Signature = details.Signature;
                    response.Data = _profile;
                    response.Message = "Success";
                    response.Status = true;
                }
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
