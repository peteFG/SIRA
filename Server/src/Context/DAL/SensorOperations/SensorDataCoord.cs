using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Context.DAL.SensorOperations;

public class SensorDataCoord
{
    public string XCoord { get; set; }
    public string YCoord { get; set; }
    public decimal Difference { get; set; }
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Type Type { get; set; }
}

public enum Type
{
    Speed,
    Altitude
}