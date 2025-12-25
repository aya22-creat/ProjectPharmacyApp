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
builder.HasKey(oi => oi.Id);

builder.Property(oi => oi.OrderId).IsRequired();
builder.Property(oi => oi.ProductId).IsRequired();
builder.Property(oi => oi.ProductName).IsRequired().HasMaxLength(200);
builder.Property(oi => oi.Quantity).IsRequired();

builder.HasOne<Order>()
       .WithMany(o => o.Items)
       .HasForeignKey(oi => oi.OrderId)
       .OnDelete(DeleteBehavior.Cascade);

builder.OwnsOne(oi => oi.Price, price =>
{
    price.Property(p => p.Amount)
         .HasColumnName("UnitPrice")
         .HasColumnType("decimal(18,2)")
         .IsRequired();

    price.Property(p => p.Currency)
         .HasColumnName("Currency")
         .HasMaxLength(3)
         .IsRequired();
});
         
        }       
    }
}
