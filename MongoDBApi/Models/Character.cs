using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBApi.Models;

public class Character
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    public string CharacterName { get; set; } = null!;

    public string AnimeTitel { get; set; }

    public string ImageUrl { get; set; } = null!;
}
