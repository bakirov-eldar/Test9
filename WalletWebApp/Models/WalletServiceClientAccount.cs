namespace WalletWebApp.Models;

public class WalletServiceClientAccount
{
    public int Id { get; set; }
    public int AccountNumber { get; set; }
    public int WalletServiceProviderId { get; set; }
    public WalletServiceProvider WalletServiceProvider { get; set; }
}
