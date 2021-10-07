using System;
using System.ComponentModel.DataAnnotations;
using API.Data.Enums;

namespace API.Data.Persistence.Entities
{
    public class WalletTransaction
    {
        [Key]
        public Guid Id { set; get; } = Guid.NewGuid();
        public string WalletAddress { set; get; }
        public Guid ClientId { set; get; }
        public WalletTransactionType Type;
        public decimal Amount { set; get; }
        public TransactionStatus Status { set; get; }
        public DateTime TransDate { set; get; }
        public string Ref { set; get; }
        public DateTime DateLogged { set; get; } = DateTime.UtcNow;

    }
}
