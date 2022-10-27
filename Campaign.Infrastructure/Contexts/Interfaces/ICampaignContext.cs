using MongoDB.Driver;

namespace Campaign.Infrastructure.Contexts.Interfaces
{
    public interface ICampaignContext
    {
        IMongoCollection<Domain.Entities.Campaign> Campaigns { get; }
    }
}