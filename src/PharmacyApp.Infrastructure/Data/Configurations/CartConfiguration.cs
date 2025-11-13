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
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
       public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.CustomerId)
                .IsUnique();
            builder.Property(c => c.State)
                .IsRequired();
            builder.Property(c => c.CreatedAt)
                .IsRequired();
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.CouponCode)
                .HasMaxLength(50);
            builder.OwnsOne(c => c.Discount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("DiscountAmount")
                    .IsRequired();
                money.Property(m => m.Currency)
                    .HasColumnName("DiscountCurrency")
                    .HasMaxLength(3)
                    .IsRequired();

            

            builder.Ignore(c => c.DomainEvents);
           
            });
        }
    }
}