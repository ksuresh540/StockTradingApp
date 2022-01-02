using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockTradingExchange.Domain;
using StockTradingExchange.Infrastructure.Database;

namespace StockTradingExchange.Infrastructure.Domain.StockTrading
{
    internal sealed class CustomerTradingEntityTypeConfiguration : IEntityTypeConfiguration<CustomerTrading>
    {
        internal const string StockTrading = "StockTrading";

        public void Configure(EntityTypeBuilder<CustomerTrading> builder)
        {
            builder.ToTable("CustomerTrading", SchemaNames.StockTrading);
            builder.HasKey(b => b.CustomerId);

            builder.Property("Name").HasColumnName("Name");
            builder.Property("Quantity").HasColumnName("Quantity");
            builder.Property("Price").HasColumnName("Price");
            builder.Property("StockType").HasColumnName("StockType");
            builder.Property("TotalAmount").HasColumnName("TotalAmount");
        }
    }
}