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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
            builder.Property(o => o.CustomerId).HasColumnName("CustomerId");
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(o => o.CompletedAt);
            builder.Property(o => o.CancelledAt);
            builder.Property(o => o.CancellationReason).HasMaxLength(500);
            builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ignore Domain Events
            builder.Ignore(o => o.DomainEvents);
        }
    }
}
