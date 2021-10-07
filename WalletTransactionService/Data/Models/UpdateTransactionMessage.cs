using System;
using API.Data.Enums;

namespace API.Data.Models
{
    public class UpdateTransactionMessage
    {
        public Guid ClientId { set; get; }
        public string HashedRefrence { set; get; }
        public string WalletAddress { set; get; }
        public CryptoCurrency Currency { set; get; }
    }
}
