using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OnionDI.Domain.Models;

namespace OnionDI.Data.EF.Config.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products")
                .HasKey(p => p.Gtin)
                .HasName("ProductsPK");
            
            builder.Property(p => p.Gtin)
                .ValueGeneratedNever();
            
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(1000)");
            
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("money");
        }
    }
}