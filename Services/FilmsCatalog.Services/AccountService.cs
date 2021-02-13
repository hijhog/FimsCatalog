using FilmsCatalog.Data.Contract.Identity;
using FilmsCatalog.Services.Contract;
using FilmsCatalog.Services.Contract.Interfaces;
using FilmsCatalog.Services.Contract.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<OperationResult> CreateAsync(UserDto model)
        {
            var result = new OperationResult();
            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName
            };

            var created = await _userManager.CreateAsync(user, model.Password);
            if (created.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                result.Succeeded = true;
            }
            else
            {
                result.ErrorMessages = created.Errors.Select(x => x.Description);
            }

            return result;
        }

        public async Task<OperationResult> LoginAsync(LoginDto model)
        {
            var result = new OperationResult();

            var signIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (signIn.Succeeded)
            {
                result.Succeeded = true;
            }
            else
            {
                result.ErrorMessage = "Неверный логин или пароль";
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
