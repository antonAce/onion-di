using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OnionDI.Domain.Models;

namespace OnionDI.Data.EF.Config.Mapping
{
    public class OrdersMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders")
                .HasKey(o => o.Id)
                .HasName("OrdersPK");
            
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(o => o.OrderingDate)
                .IsRequired();
        }
    }
}