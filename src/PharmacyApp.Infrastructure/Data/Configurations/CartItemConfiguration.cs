using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;


namespace PharmacyApp.Infrastructure.Data.Configurations
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.ProductId).IsRequired();
            builder.Property(ci => ci.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(ci => ci.Quantity).IsRequired();
            builder.Property(ci => ci.CartId).IsRequired();

            builder.OwnsOne(ci => ci.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Price").HasColumnType("decimal(18,2)");
                price.Property(p => p.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
            });

            builder.OwnsOne(ci => ci.Discount, discount =>
            {
                discount.Property(d => d.Amount).HasColumnName("Discount").HasColumnType("decimal(18,2)");
                discount.Property(d => d.Currency).HasColumnName("DiscountCurrency").HasMaxLength(3);
            });

            builder.Ignore(ci => ci.DomainEvents);
        }
    }
}
