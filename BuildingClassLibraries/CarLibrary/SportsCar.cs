namespace CarLibrary;

public class SportsCar : Car
{

    public SportsCar() { }

    public SportsCar(string name, int currentSpeed, int maxSpeed) : base(name, currentSpeed, maxSpeed) {}

    public override void TurboBoost()
    {
        Console.WriteLine("Kicking into MAXIMUM OVERDRIVE! Hee-Yaw!");
    }
}
