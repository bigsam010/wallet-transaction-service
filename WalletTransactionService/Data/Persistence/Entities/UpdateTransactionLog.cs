using System;
using System.ComponentModel.DataAnnotations;
using API.Data.Enums;

namespace API.Data.Persistence.Entities
{
    public class UpdateTransactionLog
    {
        [Key]
        public Guid Id { set; get; }
        public Guid ClientId { set; get; }
        public string HashedRefrence { set; get; }
        public string WalletAddress { set; get; }
        public CryptoCurrency Currency { set; get; }
        public DateTime RequestDate { set; get; }
        public bool IsSuccessful { set; get; }

    }
}
