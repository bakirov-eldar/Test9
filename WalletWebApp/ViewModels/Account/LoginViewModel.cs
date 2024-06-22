using System.ComponentModel.DataAnnotations;

namespace WalletWebApp.ViewModels.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "LoginRequired")]
    [Display(Name = "Login")]
    public string Login { get; set; }
    [Required(ErrorMessage = "PasswordRequired")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "RememberMe")]
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}