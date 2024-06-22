using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Reflection;
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
        if (User?.Identity?.IsAuthenticated == true)
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
        if (ModelState.IsValid)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.WalletNumber == model.WalletNumber);
            if (user is not null)
            {
                user.Balance += model.Amount;
                var transaction = new Transaction()
                {
                    Amount = model.Amount,
                    ToUserId = user.Id,
                    Type = TransactionType.Replenishment
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
        return View(model);
    }

    [HttpGet]
    public IActionResult Transfer()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Transfer(TransferViewModel model)
    {
        if (ModelState.IsValid)
        {
            var receiver = await _userManager.Users.FirstOrDefaultAsync(e => e.WalletNumber == model.WalletNumber);
            if (receiver is not null)
            {
                var sender = await _userManager.GetUserAsync(User);
                if (sender.Id != receiver.Id)
                {
                    var newBalance = sender.Balance - model.Amount;
                    if (newBalance >= 0)
                    {
                        sender.Balance = newBalance;
                        receiver.Balance += model.Amount;
                        var transaction = new Transaction()
                        {
                            Amount = model.Amount,
                            FromUserId = sender.Id,
                            ToUserId = receiver.Id,
                            Type = TransactionType.Transfer
                        };
                        _walletContext.Transactions.Add(transaction);
                        await _userManager.UpdateAsync(sender);
                        await _userManager.UpdateAsync(receiver);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Amount", _localizer["InsufficientFunds"]);
                    }
                }
                else
                {
                    ModelState.AddModelError("WalletNumber", _localizer["MatchIds"]);
                }
            }
            else
            {
                ModelState.AddModelError("WalletNumber", _localizer["UnknownWallet"]);
            }
        }
        return View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Journal(DateTimeOffset? committedDateAfter,
        DateTimeOffset? committedDateBefore)
    {
        var user = await _userManager.GetUserAsync(User);
        IQueryable<Transaction> query = _walletContext.Transactions.Include(e => e.FromUser).Include(e => e.ToUser)
            .Where(e => e.FromUserId == user.Id || e.ToUserId == user.Id);
        if (committedDateBefore.HasValue)
        {
            query = query.Where(p => p.CommitedAt <= committedDateBefore.Value);
        }
        if (committedDateAfter.HasValue)
        {
            query = query.Where(p => p.CommitedAt >= committedDateAfter.Value);
        }
        var result = await query
            .Where(e => e.Type != TransactionType.Other)
            .OrderByDescending(e => e.CommitedAt)
            .ToListAsync();
        return View(new JournalViewModel()
        {
            Transactions = result,
            CommittedDateAfter = committedDateAfter,
            CommittedDateBefore = committedDateBefore,
            User = user,
        });
    }

    [HttpGet]
    [Authorize]
    public IActionResult Payment()
    {
        ViewBag.Providers = _walletContext.WalletServiceProvider.Include(e => e.User).ToList();
        return View();

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Payment(PaymentViewModel model)
    {
        if(ModelState.IsValid)
        {
            var provider = _walletContext.WalletServiceProvider.Include(e => e.User).FirstOrDefault(e => e.User.WalletNumber == model.WalletServiceWalletNumber);
            if(provider is not null) 
            {
                var account = _walletContext.WalletServiceClientAccounts.FirstOrDefault(e => e.WalletServiceProviderId == provider.Id && e.AccountNumber == model.AccountNumber);
                if(account is not null)
                {
                    var sender = await _userManager.GetUserAsync(User);
                    var newBalance = sender.Balance - model.Amount;
                    if(newBalance >= 0)
                    {
                        sender.Balance = newBalance;
                        var receiver = provider.User;
                        receiver.Balance += model.Amount;
                        var transaction = new Transaction()
                        {
                            Amount = model.Amount,
                            FromUserId = sender.Id,
                            ToUserId = receiver.Id,
                            WalletServiceClientAccountId = account.Id,
                            Type = TransactionType.Payment
                        };
                        _walletContext.Transactions.Add(transaction);
                        await _userManager.UpdateAsync(sender);
                        await _userManager.UpdateAsync(receiver);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Amount", _localizer["InsufficientFunds"]);
                    }
                }
                else
                {
                    ModelState.AddModelError("AccountNumber", _localizer["UnknownAccount"]);
                }
            }
        }
        ViewBag.Providers = await _walletContext.WalletServiceProvider.Include(e => e.User).ToListAsync();
        return View(model);
    }
}
