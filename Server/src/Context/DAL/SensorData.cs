namespace Context.DAL;

public class SensorData : MongoDocument
{
    public int RideId { get; set; }
    public string XCoord { get; set; }
    public string YCoord { get; set; }
    public string Date { get; set; }
    public string Timestamp { get; set; }
    public string Marked { get; set; }
    public float? Altitude { get; set; }
    public float? Speed { get; set; }
    public int? DistanceLeft { get; set; }
    public int? DistanceRight { get; set; }
    public int Measurements { get; set; }
}