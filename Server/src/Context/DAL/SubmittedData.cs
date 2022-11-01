namespace Context.DAL;

public class SubmittedData : MongoDocument
{
    public DateTime StartTime { get; set; }
    public string Destination { get; set; }
    public float Distance { get; set; }
    public float TravelTime { get; set; }
    public float AvgSpeed { get; set; }
    public float Altitude { get; set; }
}