using Campaign.Application.Helpers.Interfaces;
using Campaign.Application.Request;
using Campaign.Application.Response;
using Campaign.Application.Services.Interfaces;
using Campaign.Infrastructure.Repositories.Interfaces;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Application.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICampaignHelpers _campaignHelpers;

        public CampaignService(ICampaignRepository campaignRepository, ICampaignHelpers campaignHelpers)
        {
            _campaignRepository = campaignRepository;
            _campaignHelpers = campaignHelpers;
        }

        public async Task<string> AddCampaignAsync(Domain.Entities.Campaign campaign) =>
            await _campaignRepository.InsertAsync(campaign);

        public async Task<bool> SetCampaignInActiveAsync(ObjectId id)
        {
            var campaign = await _campaignRepository.GetByIdAsync(id);

            if (campaign is null)
                return false;

            campaign.IsActive = false;
            return await _campaignRepository.UpdateAsync(campaign);
        }

        public async Task<BasketItemsResponse> SetCampaignToBasketAsync(List<BasketItemRequest> basketItemRequests)
        {
            BasketItemsResponse basketItemRsesponse = new();

            basketItemRsesponse.Items = await _campaignHelpers.SetCampaignToBasketItems(basketItemRequests);

            basketItemRsesponse.TotalDiscountedPrice = basketItemRsesponse.Items
                .Sum(b => b.Discount.DiscountedPrice);
            return basketItemRsesponse;
        }
    }
}