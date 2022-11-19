namespace Context.DAL;

public class SensorDataCoord
{
    public string XCoord { get; set; }
    public string YCoord { get; set; }
    public float Difference { get; set; }
    public Type Type { get; set; }
}

public enum Type
{
    Speed,
    Altitude
}