using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Entities;


namespace PharmacyApp.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.OwnsOne(p => p.Description, desc => desc.Property(d => d.Value).HasMaxLength(1000));
            builder.Property(p => p.Stock).IsRequired();

            // Value Object
            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(pr => pr.Value).HasColumnName("Price").HasColumnType("decimal(18,2)");
                price.Property(pr => pr.Currency).HasColumnName("Currency").HasMaxLength(3);
            });

            builder.Property(p => p.CategoryId.Value).HasColumnName("CategoryId");

            // Index
            builder.HasIndex(p => p.CategoryId);

            builder.Ignore(p => p.DomainEvents);
        }
    }
}
