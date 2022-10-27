using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Campaign.Domain.Common.Interfaces
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        DateTime CreatedTime { get; }

        [BsonRepresentation(BsonType.DateTime)]
        DateTime UpdatedTime { get; }
    }
}