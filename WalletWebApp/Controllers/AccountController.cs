using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WalletWebApp.Models;
using WalletWebApp.Services;
using WalletWebApp.ViewModels.Account;

namespace WalletWebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly WalletNumberGenerator _generator;
    private readonly IStringLocalizer _localizer;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, WalletNumberGenerator generator,
        IStringLocalizer<HomeController> localizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _generator = generator;
        _localizer = localizer;
    }

    public IActionResult Login(string? returnUrl)
    {
        if (User?.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(new LoginViewModel() { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(User?.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Login);
            if(user is null && int.TryParse(model.Login, out int walletNumber))
            {
                user = _userManager.Users.FirstOrDefault(e => e.WalletNumber == walletNumber);
            }
            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "ToDoList");
                }
            }
            ModelState.AddModelError("", _localizer["InvalidLogin"]);
        }
        return View(model);
    }
    public IActionResult Register()
    {
        if (User?.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (User?.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        if (ModelState.IsValid)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                user.WalletNumber = _generator.GenerateUniqueWalletIdentifier();
                user.Balance = 100000;
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "ToDoList");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
