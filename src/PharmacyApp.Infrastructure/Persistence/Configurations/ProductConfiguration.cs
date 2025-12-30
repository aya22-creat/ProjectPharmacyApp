using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;

namespace PharmacyApp.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductAggregate>
{
    public void Configure(EntityTypeBuilder<ProductAggregate> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductName)
               .IsRequired()
               .HasMaxLength(200);

        builder.OwnsOne(p => p.Description, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(1000);
        });

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(pr => pr!.Amount)
                 .HasColumnName("Price")
                 .HasColumnType("decimal(18,2)");

            price.Property(pr => pr!.Currency)
                 .HasColumnName("Currency")
                 .HasMaxLength(3);
        });

        builder.Property(p => p.StockQuantity).IsRequired();

        builder.Property(p => p.CategoryId)
               .HasConversion(
                   v => v.Value,
                   v => CategoryId.Create(v)
               )
               .HasColumnName("CategoryId")
               .IsRequired();

        builder.HasIndex(p => p.CategoryId);

        builder.Ignore(p => p.DomainEvents);
    }
}
