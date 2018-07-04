using ACM.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.Core.Interfaces
{
   public interface IProfileManager
    {
        ResponseModel<string> AddProfileDetail(ProfileViewModel model, string imagePath, string signPath);

        ResponseModel<string> UpdateProfileDetail(ProfileViewModel model, string imagePath, string signPath);

        ResponseModel<ProfileViewModel> ProfileDetailById(long checkInId);
    }
}
