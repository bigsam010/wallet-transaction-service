using System;
using API.Data.Enums;

namespace API.Data.Models
{
    public class CryptoWalletTransaction
    {
        public Guid ClientId { set; get; }
        public string WalletAddress { set; get; }
        public WalletTransactionType Type { set; get; }
        public decimal Amount { set; get; }
        public TransactionStatus Status { set; get; }
        public DateTime TransactionDate { set; get; }
        public string Ref { set; get; }

    }
}
