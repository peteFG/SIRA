namespace Context.DAL;

public class Info : MongoDocument
{
    public string Title { get; set; }
    //TODO: First_Aid oder Law
    public string Category { get; set; }
    public string? Section { get; set; }
    public string Text { get; set; }
}