namespace CarLibrary;

public class MiniVan : Car
{
    public MiniVan() { }
    public MiniVan(string name, int currentSpeed, int maxSpeed) : base(name, currentSpeed, maxSpeed)
    {
    }

    public override void TurboBoost()
    {
        _state = EngineStateEnum.Dead;
        Console.WriteLine("Nice try. The engine is now in the 'dead' state.");
    }
}
