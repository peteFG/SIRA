namespace Context.DAL;

public class Info : MongoDocument
{
    public string Title { get; set; }
    public string Category { get; set; }
    public string? Section { get; set; }
    public string Text { get; set; }
}