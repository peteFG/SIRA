namespace Context.DAL;

public class OvertakingDistance
{
    public string Range { get; set; }
    public int Amount { get; set; }
    public int RangeFrom { get; set; }
    public int RangeTo { get; set; }

    // needed for filtering in frontend
    public string Type { get; set; } = "OvertakingDistance";

    public string GetRangeWithAmount()
    {
        return Range + ": " + Amount;
    }
}