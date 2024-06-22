using System.ComponentModel.DataAnnotations;

namespace WalletWebApp.ViewModels.Transaction;

public class TopUpViewModel
{
    [Required(ErrorMessage = "WalletRequired")]
    [Display(Name = "Wallet")]
    public int WalletNumber { get; set; }
    [Required(ErrorMessage = "AmountRequired")]
    [Display(Name = "Amount")]
    [Range(1.0, 10_000_000, ErrorMessage = "AmountRange")]
    public decimal Amount { get; set; }
}
