using Fina.Core.Enums;

namespace Fina.Core.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ETransactionType Type { get; set; }=ETransactionType.Withdraw;
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrRecievedAt { get; set; }
    public string UserId { get; set; } = string.Empty;
}
