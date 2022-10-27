using Campaign.Infrastructure.Contexts.Interfaces;
using Campaign.Infrastructure.Settings.Interfaces;
using MongoDB.Driver;

namespace Campaign.Infrastructure.Contexts
{
    public class CampaignContext : ICampaignContext
    {
        public CampaignContext(IDatabaseSetting settings)
        {
            var client = new MongoClient(settings.ConnectionStrings);
            var database = client.GetDatabase(settings.DatabaseName);

            Campaigns = database.GetCollection<Domain.Entities.Campaign>(nameof(Domain.Entities.Campaign));
        }
        public IMongoCollection<Domain.Entities.Campaign> Campaigns { get; }
    }
}