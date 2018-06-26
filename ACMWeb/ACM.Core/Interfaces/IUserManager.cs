using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACM.Core.Interfaces
{
   public interface IUserManager
    {
        Task<SignInResult> UserLogin(LoginViewModel model, string returnUrl = null);
        Task<bool> IsUserInRole(ApplicationUser user, string role);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel model, ApplicationUser user);
        Task<ApplicationUser> FindUserByEmail(string email);
        Task<string> GeneratePasswordResetToken(ApplicationUser user);
        Task<ApplicationUser> FindUserById(string userId);
        Task<IdentityResult> ConfirmEmail(ApplicationUser user, string code);
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<IdentityResult> StoreUserRegister(RegisterStoreViewModel model,string AdminId);
        Task<string> GenerateEmailConfirmationToken(ApplicationUser user);
    }
}
