using Microsoft.EntityFrameworkCore;
using AutoLot.TPH.Models;

namespace AutoLot.TPH;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
        // to do this in a TPT format, use a model builder
        // modelBuilder.Entity<BaseEntity>().ToTable("BaseEntities");
        // modelBuilder.Entity<Car>().ToTable("Cars"); // two tables with a 1-to-1 mapping
    }
    public DbSet<Car> Cars { get; set; }
}
