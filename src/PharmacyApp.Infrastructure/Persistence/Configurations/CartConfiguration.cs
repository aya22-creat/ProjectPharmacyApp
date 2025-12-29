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

            builder.HasKey( c => c.Id);

            builder.HasIndex( c => c.CustomerId)
                .IsUnique();
            builder.Property( c => c.State)
                .IsRequired();
            builder.Property( c => c.CreatedAt)
                .IsRequired();
            builder.Property( c => c.UpdatedAt);

            builder.Property(x => x.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.Property( x => x.State)
                .HasConversion(
                     x => x.Name,
                     x => CartStateEnum.FromName(x, true));

            builder.OwnsMany( x => x.Items,  a =>
            {
                a.ToTable("CartItems");

                a.WithOwner().HasForeignKey("CartId");

                a.HasKey("Id");

                a.Property( ci => ci.ProductId)
                    .IsRequired();

                a.Property( ci => ci.ProductName)
                    .HasMaxLength(200)
                    .IsRequired();

                a.Property( ci => ci.Quantity)
                    .IsRequired();

                a.OwnsOne( ci => ci.Price, money =>
                {
                    money.Property( m => m.Amount)
                        .HasColumnName("PriceAmount")
                        .IsRequired();
                    money.Property( m => m.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasMaxLength(3)
                        .IsRequired();
                });
            });


    }
}}
