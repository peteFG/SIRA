namespace Context.DAL;

public class OvertakingDistance
{
    public string Range { get; set; }
    public int Amount { get; set; }
    public int RangeFrom { get; set; }
    public int RangeTo { get; set; }

    public string GetRangeWithAmount()
    {
        return Range + ": " + Amount;
    }
}