using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;


namespace PharmacyApp.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.OrderId).IsRequired();
            builder.Property(oi => oi.ProductId).IsRequired();
            builder.Property(oi => oi.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(oi => oi.Quantity).IsRequired();

            // Value Objects
            builder.OwnsOne(oi => oi.UnitPrice, price =>
            {
                price.Property(p => p.Amount).HasColumnName("UnitPrice").HasColumnType("decimal(18,2)");
                price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3);
            });

            builder.OwnsOne(oi => oi.Discount, discount =>
            {
                discount.Property(d => d.Amount).HasColumnName("DiscountAmount").HasColumnType("decimal(18,2)");
                discount.Property(d => d.Currency).HasColumnName("DiscountCurrency").HasMaxLength(3);
            });

            builder.Ignore(oi => oi.TotalPrice);
            builder.Ignore(oi => oi.DomainEvents);
        }
    }
}
