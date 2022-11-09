using CsvHelper.Configuration.Attributes;

namespace Context.DAL;

[Delimiter(";")]
public class SensorDataMixin
{
    [Name("Latitude")] 
    public string XCoord { get; set; }

    [Name("Longitude")] 
    public string YCoord { get; set; }

    [Name("Date")] 
    public string Date { get; set; }

    [Name("Time")] 
    public string Timestamp { get; set; }
    
    [Name("Marked")]
    public string Marked { get; set; }
    
    [Name("Altitude")] 
    public float? Altitude { get; set; }

    [Name("Speed")] 
    public float? Speed { get; set; }

    [Name("Left")] 
    public int? DistanceLeft { get; set; }

    [Name("Right")] 
    public int? DistanceRight { get; set; }

    [Name("Measurements")] 
    public int Measurements { get; set; }
}