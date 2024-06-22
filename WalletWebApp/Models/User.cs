using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WalletWebApp.Models;

public class User : IdentityUser<int>
{
    public decimal Balance { get; set; }
    public int WalletNumber { get; set; }
}
