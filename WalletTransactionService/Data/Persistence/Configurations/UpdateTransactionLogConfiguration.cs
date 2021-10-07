using System;
using API.Data.Enums;
using API.Data.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Persistence.Configurations
{
    public class UpdateTransactionLogConfiguration : IEntityTypeConfiguration<UpdateTransactionLog>
    {
        public void Configure(EntityTypeBuilder<UpdateTransactionLog> builder)
        {
            builder.Property(p => p.Currency)
               .HasConversion(x => (int)x, x => (CryptoCurrency)x);
        }
    }
}