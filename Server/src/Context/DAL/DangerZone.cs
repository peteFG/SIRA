namespace Context.DAL;

public class DangerZone : MongoDocument
{
    public string XCoord { get; set; }
    public string YCoord { get; set; }
    public string Type { get; set; }
    public string ToolTipText { get; set; }
    public string Description { get; set; }
}