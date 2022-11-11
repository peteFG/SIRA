namespace Context.DAL;

public class SensorData : MongoDocument
{
    public string RideId { get; set; }
    public string? XCoord { get; set; }
    public string? YCoord { get; set; }
    public string? Date { get; set; }
    public string? Timestamp { get; set; }
    public string? ButtonPressed { get; set; }
    public string? Altitude { get; set; }
    public string? Speed { get; set; }
    public string? DistanceLeft { get; set; }
    public string? DistanceRight { get; set; }
    public string? Measurements { get; set; }
    public DateTime UploadTimeStamp { get; set; }
}