using Microsoft.AspNetCore.Identity;
using WalletWebApp.Models;

namespace WalletWebApp.Services;

public class WalletNumberGenerator
{
    private readonly UserManager<User> _userManager;

    public WalletNumberGenerator(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public int GenerateUniqueWalletIdentifier()
    {
        int number;
        bool isUnique = false;

        do
        {
            number = GenerateRandom6DigitNumber();
            isUnique = IsIdentifierUnique(number);
        }
        while (!isUnique);

        return number;
    }

    private int GenerateRandom6DigitNumber()
    {
        Random random = new Random();
        int number = random.Next(100000, 999999);
        return number;
    }

    private bool IsIdentifierUnique(int number)
    {
        return !(_userManager.Users.Any(u => u.WalletNumber == number));
    }
}
