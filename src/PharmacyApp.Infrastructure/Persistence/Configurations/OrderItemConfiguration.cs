using PharmacyApp.Domain.OrderManagement.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.OrderManagement.Entities;


namespace PharmacyApp.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(static oi => oi.Id);

            builder.Property(static oi => oi.OrderId).IsRequired();
            builder.Property(static oi => oi.ProductId).IsRequired();
            builder.Property(static oi => oi.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(static oi => oi.Quantity).IsRequired();


           builder.HasOne<Order>().WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);

            // Value Objects
            builder.OwnsOne(static oi => oi.Price, static price =>
            {
                price.Property(static p => p.Amount).HasColumnName("UnitPrice").HasColumnType("decimal(18,2)");
                price.Property(static p => p.Currency).HasColumnName("Currency").HasMaxLength(3);
            });
        }
    }
}
