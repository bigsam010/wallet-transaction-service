using System;
using API.Data.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Persistence
{
    public class WalletServiceContext : DbContext
    {
        public WalletServiceContext(DbContextOptions options)
            : base(options)
        {

        }
        public virtual DbSet<UpdateTransactionLog> UpdateTransactionLogs { get; set; }
        public virtual DbSet<WalletTransaction> WalletTransactions { get; set; }
    }
}
