using Campaign.Application.Request;
using Campaign.Application.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campaign.Application.Helpers.Interfaces
{
    public interface ICampaignHelpers
    {
        Task<List<BasketItemResponse>> SetCampaignToBasketItems(List<BasketItemRequest> basketItemRequests);
    }
}