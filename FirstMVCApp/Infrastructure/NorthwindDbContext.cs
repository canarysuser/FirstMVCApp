using FirstMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Infrastructure
{
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {
        }


    }
}
