// See https://aka.ms/new-console-template for more information
Console.WriteLine("Entity Framework Core *******");

static void SampleSaveChanges() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    context.SaveChanges();
}

// Explicit Transaction commit / rollback
static void TransactedSaveChanges() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    using var transaction = context.Database.BeginTransaction();

    try {
        context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception ex) {
        Console.WriteLine("Exception occured: {0}", ex);
        transaction.Rollback();
    }
}

// using save points and explicit transactions
static void UsingSavePoints() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    using var transaction = context.Database.BeginTransaction();

    try
    {
        transaction.CreateSavepoint("checkpoint 1");
        context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception ex) {
        Console.WriteLine("Exception occured: {0}", ex);
        transaction.RollbackToSavepoint("checkpoint 1");
    }
}

// transaction w/ execution strategy, get ref to current execution strategy and call execute to create explicit transaction
static void TransactionWithExecutionStrategies() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var strategy = context.Database.CreateExecutionStrategy();

    strategy.Execute(() => { 
        using var transaction = context.Database.BeginTransaction();
        try {
            // action to execute IE a real database action
            transaction.Commit();
            Console.WriteLine("Insert successful");
        } catch (Exception ex)
        {
            Console.WriteLine($"Insert failed: {ex}");
            transaction.Rollback();
        }
    });
}

// create car
static void AddCar() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);

    context.Cars.Add(new() { MakeId = 1, Color = "RoRoRo", PetName = "Lemon", IsDrivable = false });
    context.SaveChanges(); //commit
}

static void TestCheckConstraint() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    context.Makes.Add(new Make() { Name = "Lemon" });
    // should fail
    context.SaveChanges();
}

static IEnumerable<Car> GetAllYellowCars() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var cars = context.Cars.Where((x) => x.Color == "Yellow"); // deferred execution

    // immediate execution, add a ToList() to the end
    return cars;

    // untracked query to DbSet<T>
    // var untrackedCar = context.Cars.Where((x) => x.Id == 1).AsNoTracking() optional WithIdentityResolution();
}