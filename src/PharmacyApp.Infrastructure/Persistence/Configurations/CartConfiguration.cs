using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Enum;



namespace PharmacyApp.Infrastructure.Persistence.Configurations
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(static c => c.Id);

            builder.HasIndex(static c => c.CustomerId)
                .IsUnique();
            builder.Property(static c => c.State)
                .IsRequired();
            builder.Property(static c => c.CreatedAt)
                .IsRequired();
            builder.Property(static c => c.UpdatedAt);
            builder.Property(static c => c.CouponCode)
                .HasMaxLength(50);


            builder.Property(static x => x.State)
                .HasConversion(
                    static x => x.Name,
                    static x => CartStateEnum.FromName(x, true));

            builder.OwnsMany(static x => x.Items, static a =>
            {
                a.ToTable("CartItems");

                a.WithOwner().HasForeignKey("CartId");

                a.HasKey("Id");

                a.Property(static ci => ci.ProductId)
                    .IsRequired();

                a.Property(static ci => ci.ProductName)
                    .HasMaxLength(200)
                    .IsRequired();

                a.Property(static ci => ci.Quantity)
                    .IsRequired();

                a.OwnsOne(static ci => ci.Price, static money =>
                {
                    money.Property(static m => m.Amount)
                        .HasColumnName("PriceAmount")
                        .IsRequired();
                    money.Property(static m => m.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasMaxLength(3)
                        .IsRequired();
                });
            });

            builder.OwnsOne(static c => c.Discount, static money =>
            {
                money.Property(static m => m.Amount)
                    .HasColumnName("DiscountAmount")
                    .IsRequired();
                money.Property(static m => m.Currency)
                    .HasColumnName("DiscountCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        }
    }
}