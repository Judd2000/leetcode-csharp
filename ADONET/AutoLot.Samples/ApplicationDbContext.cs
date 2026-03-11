using AutoLot.Samples.ViewModels;
using System.Runtime.ConstrainedExecution;

namespace AutoLot.Samples;

public class ApplicationDbContext : DbContext {

    public DbSet<Car> Cars { get; set; }

    public DbSet<Make> Makes { get; set; }

    public DbSet<Radio> Radios { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<CarMakeViewModel> CarMakeViewModel { get; set; }

    public DbSet<CarDriver> CarsToDrivers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new CarConfiguration().Configure(modelBuilder.Entity<Car>()); //pass the entity - we are literally just forwarding everything we were doing to the class.

        new RadioConfiguration().Configure(modelBuilder.Entity<Radio>());

        new DriverConfiguration().Configure(modelBuilder.Entity<Driver>());

        new CarMakeViewModelConfiguration().Configure(modelBuilder.Entity<CarMakeViewModel>());

        // to do this in a TPT format, use a model builder
        //modelBuilder.Entity<BaseEntity>().ToTable("BaseEntities");
        //modelBuilder.Entity<Car>().ToTable("Cars"); // two tables with a 1-to-1 mapping
        modelBuilder.Entity<Make>()
            .ToTable((t) => t.HasCheckConstraint(name: "CH_Name", sql: "[Name]<>'Lemon'"));
            
        //modelBuilder.Entity<Car>(entity => {
        //    entity.ToTable("Inventory", "dbo");
        //    //entity.HasKey(e => new { e.Id, e.OrganizationId }); // comp key
        //});

        //modelBuilder.Entity<Radio>(entity => { entity.Property(e => e.CarId).HasColumnName("InventoryId"); });

        //modelBuilder.Entity<Car>(entity => {
        //    entity.ToTable("Inventory", "dbo");
        //    entity.HasKey(e => e.Id); // composite: HashKey((e) => new {e.Id, e.OrganizationId});
        //    entity.HasIndex(e => e.MakeId, "IX_Inventory_MakeId"); //.IsUnique();
        //    // HasDefaultValue("Black");
        //    // def val
        //    // .HasDefaultValueSql("getdate()");
        //    entity.Property((e) => e.Color)
        //    .IsRequired()
        //    .HasMaxLength(50)
        //    .HasDefaultValue("Black");

        //    entity.Property((e) => e.PetName)
        //    .IsRequired()
        //    .HasMaxLength(50);

        //    entity.Property((e) => e.DateBuilt)
        //    .HasDefaultValueSql("getdate()"); //how to use a function as the def value

        //    entity.Property((e) => e.IsDrivable)
        //    .HasField("_isDrivable") // this is inferred based on naming conventions _ + field name, but explicit can help with understanding.
        //    .HasDefaultValue(true);
        // });

        //modelBuilder.Entity<Car>((e) => {
        //    e.Property((p) => p.Display)
        //    .HasComputedColumnSql("[PetName] + '([Color])'", stored:true);
        //});

        // One-to-many with Fluent API (dependent side)
        //modelBuilder.Entity<Car>((e) => {
        //    e.HasOne((d) => d.MakeNavigation)
        //    .WithMany((p) => p.Cars) //WithOne for one-to-one
        //    .HasForeignKey((d) => d.MakeId)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_Inventory_Makes_MakeId");
        //});

        // on opposite side of one-to-many (principal as base, preferred):
        modelBuilder.Entity<Make>((e) =>
        {
            e.HasMany((m) => m.Cars)
            .WithOne((c) => c.MakeNavigation)
            .HasForeignKey(c => c.MakeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Inventory_Makes_MakeId");
        });

        //modelBuilder.Entity<Radio>((e) => {
        //    e.HasIndex((e) => e.CarId, "IX_Radios_CarId").IsUnique();  // for one-to-one, define and index on dependent, or one will be automatically created
        //});

        // one-to-one on dependent
        //modelBuilder.Entity<Radio>((e) => {
        //    e.HasIndex((e) => e.CarId, "IX_Radios_CarId").IsUnique();

        //    // d for dependent, p for principal.
        //    e.HasOne((d) => d.CarNavigation)
        //    .WithOne((p) => p.RadioNavigation)
        //    .HasForeignKey<Radio>((d) => d.CarId);
        //});

        // many-to-many between CarDriver and Cars and Drivers
        //modelBuilder.Entity<Car>()
        //    .HasMany((p) => p.Drivers) // principal is CarDriver
        //    .WithMany((p) => p.Cars)
        //    .UsingEntity<CarDriver>( // for CarDriver, add many-to-many
        //        (j) => j.HasOne((cd) => cd.DriverNavigation)
        //            .WithMany((d) => d.CarDrivers)
        //            .HasForeignKey(nameof(CarDriver.DriverId))
        //            .HasConstraintName("FK_InventoryDriver_Drivers_DriverId")
        //            .OnDelete(DeleteBehavior.Cascade),
        //        (j) => j.HasOne((cd) => cd.CarNavigation)
        //            .WithMany((c) => c.CarDrivers)
        //            .HasForeignKey(nameof(CarDriver.CarId))
        //            .HasConstraintName("FK_InventoryDriver_Inventory_InventoryId")
        //            .OnDelete(DeleteBehavior.ClientCascade),
        //        (j) =>  j.HasKey((cd) => new { cd.CarId, cd.DriverId })
        //    );

        // to exclude an entity from migrations:
        //modelBuilder.Entity<LogEntry>().ToTable("Logs", (t) => t.ExcludeFromMigrations());
    }



    // How to override EF Core defaults & Conventions
    protected override void ConfigureConventions(ModelConfigurationBuilder configBuilder) {
        //configBuilder.Properties<string>().HaveMaxLength(50);  // make strings def of 50 in database
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        ChangeTracker.Tracked += ChangeTracker_Tracked;
    }

    private void ChangeTracker_StateChanged(object sender, EntityStateChangedEventArgs e)
    {
        if (e.OldState == EntityState.Modified && e.NewState == EntityState.Unchanged)
        {
            Console.WriteLine($"Entity of type {e.Entry.Entity.GetType().Name} was updated");
        }
    }

    private void ChangeTracker_Tracked(object sender, EntityTrackedEventArgs e) {
        if (e.FromQuery) {
            Console.WriteLine($"An entity of type {e.Entry.Entity.GetType().Name} was loaded from the DB");
        }
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
        string connectionString = @"server =.,5433;Database=AutoLotSamples;UserId=sa;Password=Th3N3wP@ssword_;Encrypt=False;";

        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
