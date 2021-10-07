using System;
using API.Data.Enums;
using API.Data.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Persistence.Configurations
{
    public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder.Property(p => p.Status)
               .HasConversion(x => (int)x, x => (TransactionStatus)x);

            builder.Property(p => p.Type)
             .HasConversion(x => (int)x, x => (WalletTransactionType)x);
        }
    }
}
