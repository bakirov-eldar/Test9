using Microsoft.AspNetCore.Identity;
using WalletWebApp.Models;

namespace WalletWebApp.Services;

public class WalletServiceDataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
        using var context = scope.ServiceProvider.GetService<WalletContext>();
        var generator = scope.ServiceProvider.GetService<WalletNumberGenerator>();
        var p1 = await userManager.FindByEmailAsync("homeline@homeline.kg");
        if (p1 is null)
        {
            var u1 = new User()
            {
                UserName = "homeline@homeline.kg",
                Email = "homeline@homeline.kg",
                WalletNumber = generator.GenerateUniqueWalletIdentifier()
            };
            var r1 = await userManager.CreateAsync(u1, "homeline");
            if(r1.Succeeded)
            {
                var sp1 = new WalletServiceProvider()
                {
                    Id = u1.Id,
                    Name = "HomeLine - интернет",
                };
                context.WalletServiceProvider.Add(sp1);
                await context.SaveChangesAsync();
                var c11 = new WalletServiceClientAccount()
                {
                    AccountNumber = 20272024,
                    WalletServiceProviderId = sp1.Id
                };
                var c12 = new WalletServiceClientAccount()
                {
                    AccountNumber = 20272025,
                    WalletServiceProviderId = sp1.Id
                };
                var c13 = new WalletServiceClientAccount()
                {
                    AccountNumber = 20272026,
                    WalletServiceProviderId = sp1.Id
                };
                context.WalletServiceClientAccounts.AddRange(c11, c12, c13);
                await context.SaveChangesAsync();
            }

        }

        var p2 = await userManager.FindByEmailAsync("bishkekvodokanal@bishkek.kg");
        if (p2 is null)
        {
            var u2 = new User()
            {
                UserName = "bishkekvodokanal@bishkek.kg",
                Email = "bishkekvodokanal@bishkek.kg",
                WalletNumber = generator.GenerateUniqueWalletIdentifier()
            };
            await userManager.CreateAsync(u2, "bishkekvodokanal");
            var sp2 = new WalletServiceProvider()
            {
                Id = u2.Id,
                Name = "Бишкекводоканал",
            };
            context.WalletServiceProvider.Add(sp2);
            await context.SaveChangesAsync();
            var c21 = new WalletServiceClientAccount()
            {
                AccountNumber = 20282027,
                WalletServiceProviderId = sp2.Id
            };
            var c22 = new WalletServiceClientAccount()
            {
                AccountNumber = 20282028,
                WalletServiceProviderId = sp2.Id
            };
            var c23 = new WalletServiceClientAccount()
            {
                AccountNumber = 20282029,
                WalletServiceProviderId = sp2.Id
            };
            context.WalletServiceClientAccounts.AddRange(c21, c22, c23);
            await context.SaveChangesAsync();
        }

        var p3 = await userManager.FindByEmailAsync("severelectro@severelectro.kg");
        if (p3 is null)
        {
            var u3 = new User()
            {
                UserName = "severelectro@severelectro.kg",
                Email = "severelectro@severelectro.kg",
                WalletNumber = generator.GenerateUniqueWalletIdentifier()
            };
            await userManager.CreateAsync(u3, "severelectro");
            var sp3 = new WalletServiceProvider()
            {
                Id = u3.Id,
                Name = "Северэлектро",
            };
            context.WalletServiceProvider.Add(sp3);
            await context.SaveChangesAsync();
            var c31 = new WalletServiceClientAccount()
            {
                AccountNumber = 20292030,
                WalletServiceProviderId = sp3.Id
            };
            var c32 = new WalletServiceClientAccount()
            {
                AccountNumber = 20292031,
                WalletServiceProviderId = sp3.Id
            };
            var c33 = new WalletServiceClientAccount()
            {
                AccountNumber = 20292032,
                WalletServiceProviderId = sp3.Id
            };
            context.WalletServiceClientAccounts.AddRange(c31, c32, c33);
            await context.SaveChangesAsync();
        }

        var p4 = await userManager.FindByEmailAsync("gazprom@gazprom.kg");
        if (p4 is null)
        {
            var u4 = new User()
            {
                UserName = "gazprom@gazprom.kg",
                Email = "gazprom@gazprom.kg",
                WalletNumber = generator.GenerateUniqueWalletIdentifier()
            };
            await userManager.CreateAsync(u4, "gazprom");
            var sp4 = new WalletServiceProvider()
            {
                Id = u4.Id,
                Name = "Газпром",
            };
            context.WalletServiceProvider.Add(sp4);
            await context.SaveChangesAsync();
            var c41 = new WalletServiceClientAccount()
            {
                AccountNumber = 20302033,
                WalletServiceProviderId = sp4.Id
            };
            var c42 = new WalletServiceClientAccount()
            {
                AccountNumber = 20302034,
                WalletServiceProviderId = sp4.Id
            };
            var c43 = new WalletServiceClientAccount()
            {
                AccountNumber = 20302035,
                WalletServiceProviderId = sp4.Id
            };
            context.WalletServiceClientAccounts.AddRange(c41, c42, c43);
            await context.SaveChangesAsync();
        }
    }
}
