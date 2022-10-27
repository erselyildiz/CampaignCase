using Campaign.Infrastructure.Contexts;
using Campaign.Infrastructure.Contexts.Interfaces;
using Campaign.Infrastructure.Repositories.Interfaces;
using Campaign.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Campaign.Infrastructure.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ICampaignContext _campaignContext;

        public CampaignRepository(IOptions<DatabaseSetting> settings)
        {
            _campaignContext = new CampaignContext(settings.Value);
        }

        public async Task<string> InsertAsync(Domain.Entities.Campaign campaign)
        {
            campaign.Name = campaign.Name.ToLower();
            campaign.UpdatedTime = DateTime.UtcNow;
            await _campaignContext.Campaigns.InsertOneAsync(campaign);
            return campaign.Id.ToString();
        }

        public async Task<bool> UpdateAsync(Domain.Entities.Campaign campaign)
        {
            campaign.Name = campaign.Name.ToLower();
            campaign.UpdatedTime = DateTime.UtcNow;
            var result = await _campaignContext.Campaigns.ReplaceOneAsync(Builders<Domain.Entities.Campaign>.Filter.Eq(nameof(Domain.Entities.Campaign.Id), campaign.Id), campaign);
            return result.IsAcknowledged;
        }

        public async Task<Domain.Entities.Campaign> GetByIdAsync(ObjectId objectId) =>
            await _campaignContext.Campaigns.Find(Builders<Domain.Entities.Campaign>.Filter.Eq(nameof(Domain.Entities.Campaign.Id), objectId)).FirstOrDefaultAsync();

        public async Task<List<Domain.Entities.Campaign>> GetCampaignsByName(List<string> names) =>
            await _campaignContext.Campaigns.Find(Builders<Domain.Entities.Campaign>.Filter.In(f => f.Name,  names)).ToListAsync();
    }
}