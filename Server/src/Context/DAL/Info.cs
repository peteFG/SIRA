using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Context.DAL;

public class Info : MongoDocument
{
    public string Title { get; set; }

    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Category Category { get; set; }

    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Action? Action { get; set; } = null;

    public string? Section { get; set; }
    public string Text { get; set; }

    public string Icon { get; set; } = "document-text";

    public string? Source { get; set; }
}

public enum Category
{
    Law,
    FirstAid
}

public enum Action
{
    EmergencyCall,
    AccidentReport
}