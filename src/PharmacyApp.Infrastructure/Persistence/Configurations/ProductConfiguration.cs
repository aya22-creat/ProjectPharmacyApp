using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;


namespace PharmacyApp.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(static p => p.Id);

            builder.Property(static p => p.Name).IsRequired().HasMaxLength(200);
            builder.OwnsOne(static p => p.Description, static desc => desc.Property(static d => d.Value).HasMaxLength(1000));
            builder.Property(static p => p.StockQuantity).IsRequired();

            // Value Object
            builder.OwnsOne(static p => p.Price, static price =>
            {
                price.Property(static pr => pr.Value).HasColumnName("Price").HasColumnType("decimal(18,2)");
                price.Property(static pr => pr.Currency).HasColumnName("Currency").HasMaxLength(3);
            });

            builder.Property(static p => p.CategoryId.Value).HasColumnName("CategoryId");

            // Index
            builder.HasIndex(static p => p.CategoryId);

            builder.Ignore(static p => p.DomainEvents);
        }
    }
}
