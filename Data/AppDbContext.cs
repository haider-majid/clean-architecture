using clean_architecture.Entity;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Data;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> products { get; set; }
    
    
    
}