using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WalletWebApp.ViewModels.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "EmailRequired")]
    [DisplayName("Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "PasswordRequired")]
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required(ErrorMessage = "ConfirmPasswordRequired")]
    [DisplayName("ConfirmPassword")]
    [Compare(nameof(Password), ErrorMessage = "ConfirmPasswordCompare")]
    public string RepeatPassword { get; set; }
}
