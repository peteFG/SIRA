using MongoDB.Bson;

namespace Context.DAL;

public class CommonFile : MongoDocument
{
    public ObjectId FileObjectId { get; set; }
    public string Name { get; set; }
}