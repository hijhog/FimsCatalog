using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FilmsCatalog.Models;
using Microsoft.Extensions.Logging;
using FilmsCatalog.Services.Contract.Interfaces;
using FilmsCatalog.Services.Contract.Models.Account;

namespace FilmsCatalog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IAccountService accountService,
            ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = new UserDto
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Password = model.Password
            };
            
            var result = await _accountService.CreateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("User success signup!");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.ErrorMessages)
            {
                _logger.LogError("User don't signup!");
                ModelState.AddModelError(String.Empty, error);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            return View(new SignInViewModel {ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginDto = new LoginDto
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var result = await _accountService.LoginAsync(loginDto);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

                return View(model);
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}