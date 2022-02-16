using Microsoft.EntityFrameworkCore;
using PCLabs.EntityDesign.Api.DataAccess.Configuration;
using PCLabs.EntityDesign.Domain.Orders;

namespace PCLabs.EntityDesign.Api.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        }
    }
}
