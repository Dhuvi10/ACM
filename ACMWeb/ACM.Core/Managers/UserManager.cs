using ACM.Core.Interfaces;
using ACM.Core.Models;
using ACM.Core.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.Core.Managers
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;       
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> ConfirmEmail(ApplicationUser user, string code)
        {
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<ApplicationUser> FindUserByEmail(string email)
        {
          return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> IsUserInRole(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(_userManager.Users.Where(e => e.Email == user.Email).FirstOrDefault(), role);
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
        public async Task<IdentityResult> StoreUserRegister(RegisterStoreViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModel model, ApplicationUser user)
        {
            return await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        }
        
        public async Task<SignInResult> UserLogin(LoginViewModel model, string returnUrl = null)
        {
          return  await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

    }
}
