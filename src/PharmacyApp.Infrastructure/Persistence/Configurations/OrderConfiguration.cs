using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Domain.OrderManagement.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.OrderManagement.Entities;
using MassTransit;

namespace PharmacyApp.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder.ToTable("Orders");
builder.HasKey(o => o.Id);

builder.Property(o => o.OrderDate).IsRequired();

builder.Property(o => o.State)
                   .HasConversion(
                       v => v.Name,                                   
                        v => OrderStateEnum.List.Single(x => x.Name == v)           
                   )
                   .IsRequired()
                   .HasMaxLength(50);

builder.Property(o => o.CustomerId).IsRequired();
//caculate runtime total amount  builder.Ignore(o => o.TotalAmount);

builder.Ignore(o => o.SubTotal);
builder.Ignore(o => o.TotalAmount);
builder.Property(o => o.CompletedAt).IsRequired(false);
builder.Property(o => o.CancelledAt).IsRequired(false);
builder.Property(o => o.CancellationReason).HasMaxLength(500);
builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);

builder.OwnsOne(o => o.ShippingCost, cost =>
{
    cost.Property(c => c.Amount).HasColumnName("ShippingCostAmount").HasColumnType("decimal(18,2)");
    cost.Property(c => c.Currency).HasColumnName("ShippingCostCurrency").HasMaxLength(3);
});

builder.HasMany(o => o.Items)
       .WithOne()
       .OnDelete(DeleteBehavior.Cascade);

// Ignore Domain Events
builder.Ignore(o => o.DomainEvents);

    }
}
}