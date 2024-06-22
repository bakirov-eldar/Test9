using System.ComponentModel.DataAnnotations;

namespace WalletWebApp.Models
{
    public enum TransactionType
    {
        [Display(Name = "Payment")]
        Payment,
        [Display(Name = "Transfer")]
        Transfer,
        [Display(Name = "Replenishment")]
        Replenishment,
        [Display(Name = "Other")]
        Other
    }
}
