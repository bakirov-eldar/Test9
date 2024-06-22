using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletWebApp.Models;

[PrimaryKey(nameof(Id))]
public class WalletServiceProvider
{
    [ForeignKey(nameof(User))]
    public int Id { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public List<WalletServiceClientAccount> ClientAccounts { get; set; }
}
