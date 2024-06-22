using System.ComponentModel.DataAnnotations;

namespace WalletWebApp.ViewModels.Transaction;

public class JournalViewModel
{
    public Models.User User { get; set; }
    public List<Models.Transaction> Transactions { get; set; }
    [Display(Name = "CommittedDateAfter")]
    public DateTimeOffset? CommittedDateAfter { get; set; }
    [Display(Name = "CommittedDateBefore")]
    public DateTimeOffset? CommittedDateBefore { get; set; }
}
