using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OnionDI.Domain.Models;

namespace OnionDI.Data.EF.Config.Mapping
{
    public class OrderProductMapping : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrdersToProducts")
                .HasKey(op => new { op.OrderId, op.ProductGtin })
                .HasName("OrdersToProductsPK");
            
            builder.Property(op => op.OrderId)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(op => op.ProductGtin)
                .IsRequired()
                .ValueGeneratedNever();
            
            builder.Property(o => o.ProductCount)
                .IsRequired();
            
            builder.HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);
            
            builder.HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductGtin);
        }
    }
}