using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WalletWebApp.Models;
using WalletWebApp.ViewModels.Transaction;

namespace WalletWebApp.Controllers;

public class TransactionController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly WalletContext _walletContext;
    private readonly IStringLocalizer _localizer;
    public TransactionController(UserManager<User> userManager, WalletContext walletContext, IStringLocalizer<TransactionController> localizer)
    {
        _userManager = userManager;
        _walletContext = walletContext;
        _localizer = localizer;
    }
    [HttpGet]
    public async Task<IActionResult> TopUpWallet()
    {
        if(User?.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new TopUpViewModel()
            {
                WalletNumber = user.WalletNumber
            });
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> TopUpWallet(TopUpViewModel model)
    {
        if(ModelState.IsValid)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.WalletNumber == model.WalletNumber);
            if(user is not null)
            {
                user.Balance += model.Amount;
                var transaction = new Transaction()
                {
                    Amount = model.Amount,
                    ToUserId = user.Id,
                    Type = TransactionType.Payment
                };
                _walletContext.Transactions.Add(transaction);
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("WalletNumber", _localizer["UnknownWallet"]);
            }
        }
        return View();
    }
}
