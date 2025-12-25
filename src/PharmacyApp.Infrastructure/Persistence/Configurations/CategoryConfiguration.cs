using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate;

namespace PharmacyApp.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryAggregate>
{
    public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Description)
               .HasMaxLength(1000);

        builder.Property(c => c.DisplayOrder);
        builder.Property(c => c.CreatedAt);
        builder.Property(c => c.ProductCount);

        builder.Ignore(c => c.DomainEvents);
    }
}
