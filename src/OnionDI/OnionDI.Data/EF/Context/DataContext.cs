using Microsoft.EntityFrameworkCore;

using OnionDI.Domain.Models;

using OnionDI.Data.EF.Config.Mapping;

namespace OnionDI.Data.EF.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrdersMapping());
            modelBuilder.ApplyConfiguration(new OrderProductMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
        }
    }
}