using Campaign.Application.Request;
using Campaign.Application.Response;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campaign.Application.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<string> AddCampaignAsync(Domain.Entities.Campaign campaign);
        Task<bool> SetCampaignInActiveAsync(ObjectId id);
        Task<BasketItemsResponse> SetCampaignToBasketAsync(List<BasketItemRequest> basketItemRequests);
    }
}