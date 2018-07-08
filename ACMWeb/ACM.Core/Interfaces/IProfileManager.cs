using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Interfaces
{
   public interface IProfileManager
    {
        ResponseModel<ProfileViewModel> AddProfileDetail(ProfileViewModel model, string imagePath, string signPath);

        ResponseModel<ProfileViewModel> UpdateProfileDetail(ProfileViewModel model, string imagePath, string signPath);

        ResponseModel<ProfileViewModel> ProfileDetailById(long checkInId);
    }
}
