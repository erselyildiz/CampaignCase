using Campaign.Domain.Common;
using Campaign.Domain.Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Campaign.Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public Campaign()
        {
            IsActive = true;
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDate { get; set; }
        public decimal MinimumBasketPrice { get; set; }
        public MinimumBasketType MinimumBasketType { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Discount { get; set; }
        public CamapignType CamapignType { get; set; }
    }
}