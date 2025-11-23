using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CheckoutFunctionality.Entities;



 public class CheckoutConfiguration : IEntityTypeConfiguration<CheckoutAggregate>
    {
        public void Configure(EntityTypeBuilder<CheckoutAggregate> builder)
        {
            builder.ToTable("Checkouts");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CustomerId).IsRequired();
            builder.Property(c => c.CartId).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.CancellationReason).HasMaxLength(500);

            // Value Objects
            builder.OwnsOne(c => c.ShippingAddress, address =>
            {
                address.Property(a => a.Street).HasColumnName("ShippingStreet").HasMaxLength(200);
                address.Property(a => a.City).HasColumnName("ShippingCity").HasMaxLength(100);
                address.Property(a => a.Country).HasColumnName("ShippingCountry").HasMaxLength(100);
                address.Property(a => a.ZipCode).HasColumnName("ShippingZipCode").HasMaxLength(20);
            });

            builder.OwnsOne(c => c.BillingAddress, address =>
            {
                address.Property(a => a.Street).HasColumnName("BillingStreet").HasMaxLength(200);
                address.Property(a => a.City).HasColumnName("BillingCity").HasMaxLength(100);
                address.Property(a => a.Country).HasColumnName("BillingCountry").HasMaxLength(100);
                address.Property(a => a.ZipCode).HasColumnName("BillingZipCode").HasMaxLength(20);
            });

            builder.OwnsOne(c => c.PaymentMethod, payment =>
            {
                payment.Property(p => p.Type).HasColumnName("PaymentType").HasMaxLength(50);
                payment.Property(p => p.Details).HasColumnName("PaymentDetails").HasMaxLength(500);
            });

            builder.OwnsOne(c => c.TotalPrice, price =>
            {
                price.Property(p => p.Amount).HasColumnName("TotalPrice").HasColumnType("decimal(18,2)");
                price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3);
            });

            // Index
            builder.HasIndex(c => c.CustomerId);
            builder.HasIndex(c => c.CartId);

            builder.Ignore(c => c.DomainEvents);
        }
    }
