using Campaign.Domain.Common.Interfaces;
using MongoDB.Bson;
using System;

namespace Campaign.Domain.Common
{
    public class BaseEntity : IEntity
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedTime => DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; }
    }
}