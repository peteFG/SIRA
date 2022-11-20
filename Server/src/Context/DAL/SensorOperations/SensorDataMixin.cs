using CsvHelper.Configuration.Attributes;

namespace Context.DAL.SensorOperations;

[Delimiter(";")]
public class SensorDataMixin
{
    [Name("Latitude")] 
    public string? XCoord { get; set; }

    [Name("Longitude")] 
    public string? YCoord { get; set; }

    [Name("Date")] 
    public string? Date { get; set; }

    [Name("Time")] 
    public string? Timestamp { get; set; }
    
    [Name("Confirmed")]
    public string? ButtonPressed { get; set; }
    
    [Name("Altitude")] 
    public string? Altitude { get; set; }

    [Name("Speed")] 
    public string? Speed { get; set; }

    [Name("Left")] 
    public string? DistanceLeft { get; set; }

    [Name("Right")] 
    public string? DistanceRight { get; set; }

    [Name("Measurements")] 
    public string? Measurements { get; set; }
}