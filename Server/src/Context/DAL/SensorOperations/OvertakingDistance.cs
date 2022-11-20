namespace Context.DAL.SensorOperations;

public class OvertakingDistance
{
    public string Range { get; set; }
    public int Amount { get; set; }
    public int RangeFrom { get; set; }
    public int RangeTo { get; set; }
    public string Type { get; set; } = "OvertakingDistance";


    public string GetRangeWithAmount()
    {
        return Range + ": " + Amount;
    }
}