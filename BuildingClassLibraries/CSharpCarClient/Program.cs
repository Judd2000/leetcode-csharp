using CarLibrary; // when project reference is made, solution build order adjusted so that dependents are built first.

Console.WriteLine("*** C# CarLibary Client App ***\n");

SportsCar viper = new() { Name = "Viper", MaxSpeed = 240, CurrentSpeed = 40 };
viper.TurboBoost();

// Minivan
MiniVan mv = new();
mv.TurboBoost();
