using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate;


namespace PharmacyApp.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.ProductCount).IsRequired();

            builder.HasIndex(c => c.Name);

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
