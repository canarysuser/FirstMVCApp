using Microsoft.EntityFrameworkCore;

namespace MyApi.Infrastructure
{
    public class NorthwindDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; } = null!;  

        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {
        }

    }
}
