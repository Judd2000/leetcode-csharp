Console.WriteLine("***** More Fun with EF Core *****\n");

AddRecords();

static void AddRecords() {
    var context = new ApplicationDbContextFactory().CreateDbContext(null);

    var newMake = new Make
    {
        Name = "BMW"
    };

    Console.WriteLine($"State of the {newMake.Name} is {context.Entry(newMake).State}");

    context.Makes.Add(newMake ); // db insert


    Console.WriteLine($"State of the {newMake.Name} is {context.Entry(newMake).State}");

    context.SaveChanges();
}