using Campaign.Domain.Common.Enums;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Infrastructure.Contexts
{
    public class CampaignContextSeed
    {
        public static async Task SeedAsync(IMongoCollection<Domain.Entities.Campaign> mongoCollection)
        {
            bool exist = mongoCollection.Find(p => true).Any();
            if (!exist)
                await mongoCollection.InsertManyAsync(CampaignData());
        }

        private static IEnumerable<Domain.Entities.Campaign> CampaignData() =>
            new List<Domain.Entities.Campaign>()
            {
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("613ce8077d9b46580f8a5a3f"),
                    Name = "apple",
                    CamapignType = CamapignType.Brand,
                    DiscountType = DiscountType.Rate,
                    Discount = 13,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 1
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("63557af3c1980fb48a7e26fd"),
                    Name = "keyboard-discount-group",
                    CamapignType = CamapignType.ProductGroup,
                    DiscountType = DiscountType.Price,
                    Discount = 15,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 1
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("63557b004988ddad5ad3790b"),
                    Name = "aff-5963po",
                    CamapignType = CamapignType.ProductCode,
                    DiscountType = DiscountType.Price,
                    Discount = 100,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 10
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("63557b074272d33244a383dc"),
                    Name = "nevresim-takimlari",
                    CamapignType = CamapignType.Category,
                    DiscountType = DiscountType.Rate,
                    Discount = 5,
                    MinimumBasketPrice = 150,
                    MinimumBasketType = MinimumBasketType.Total,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 9999
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("6356dc6926900c0555643677"),
                    Name = "test-for-isactive",
                    CamapignType = CamapignType.Category,
                    DiscountType = DiscountType.Rate,
                    Discount = 5,
                    MinimumBasketPrice = 150,
                    MinimumBasketType = MinimumBasketType.Total,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 1
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("6359cf7683b87345e2c82072"),
                    Name = "ev-dekorasyon",
                    CamapignType = CamapignType.Category,
                    DiscountType = DiscountType.Rate,
                    Discount = 25,
                    MinimumBasketPrice = 250,
                    MinimumBasketType = MinimumBasketType.Total,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 9999
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("6359cfdc441782672a5c53e9"),
                    Name = "kedimamasi-sepette-50-tl-indirim",
                    CamapignType = CamapignType.ProductGroup,
                    DiscountType = DiscountType.Price,
                    Discount = 50,
                    MinimumBasketPrice = 300,
                    MinimumBasketType = MinimumBasketType.Campaign,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 9999
                },
                new Domain.Entities.Campaign()
                {
                    Id = new ObjectId("6359d87675612319ab9e3716"),
                    Name = "samsung-kulaklik",
                    CamapignType = CamapignType.Category,
                    DiscountType = DiscountType.Rate,
                    Discount = 100,
                    MinimumBasketPrice = 2000,
                    MinimumBasketType = MinimumBasketType.Total,
                    StartDate = DateTime.UtcNow.AddDays(-1).Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    UpdatedTime = DateTime.UtcNow,
                    Quantity = 9999
                }
            };
    }
}