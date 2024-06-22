using System.ComponentModel.DataAnnotations;
using WalletWebApp.Models;

namespace WalletWebApp.ViewModels.Transaction;

public class PaymentViewModel
{
    [Required(ErrorMessage = "WalletServiceWalletNumberRequired")]
    [Display(Name = "WalletServiceWalletNumber")]
    public int WalletServiceWalletNumber { get; set; }
    [Required(ErrorMessage = "AccountRequired")]
    [Display(Name = "Account")]
    public int AccountNumber { get; set; }
    [Required(ErrorMessage = "AmountRequired")]
    [Display(Name = "Amount")]
    [Range(1.0, 10_000_000, ErrorMessage = "AmountRange")]
    public decimal Amount { get; set; }
}
