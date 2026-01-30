namespace CarLibrary;
public abstract class Car
{
    public string Name { get; set; } = "";

    public int CurrentSpeed { get; set; }

    public int MaxSpeed { get; set; }

    protected EngineStateEnum _state = EngineStateEnum.Alive;

    public EngineStateEnum EngineState => _state;

    public abstract void TurboBoost();

    protected Car() { }

    protected Car(string name, int currentSpeed, int maxSpeed) {
        Name = name;
        CurrentSpeed = currentSpeed;
        MaxSpeed = maxSpeed;
    }

    public void TurnOnRadio(bool musicOn, MusicMediaEnum mm) {
        string message = musicOn ? "Ra ra rararara" : "";
        Console.WriteLine(message);
    }
}
