using CarLibrary;

Console.WriteLine("*** C# CarLibary Client App ***\n");

SportsCar viper = new() { Name = "Viper", MaxSpeed = 240, CurrentSpeed = 40 };
viper.TurboBoost();

// Minivan
MiniVan mv = new();
mv.TurboBoost();
Console.ReadLine();
