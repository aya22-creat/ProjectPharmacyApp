using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate;


namespace PharmacyApp.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(static c => c.Id);

            builder.Property(static c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(static c => c.Description).HasMaxLength(500);
            builder.Property(static c => c.ProductCount).IsRequired();

            builder.HasIndex(static c => c.Name);

            builder.Ignore(static c => c.DomainEvents);
        }
    }
}
