using Campaign.Application.Helpers.Interfaces;
using Campaign.Application.Request;
using Campaign.Application.Response;
using Campaign.Domain.Common.Enums;
using Campaign.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Application.Helpers
{
    public class CampaignHelpers : ICampaignHelpers
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignHelpers(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<BasketItemResponse>> SetCampaignToBasketItems(List<BasketItemRequest> basketItemRequests)
        {
            decimal basketTotal = basketItemRequests.Sum(b => b.Price);

            List<string> campaignNames = GetCampaignNames(basketItemRequests);

            List<Domain.Entities.Campaign> campaigns = await _campaignRepository.GetCampaignsByName(campaignNames);

            List<BasketItemResponse> basketItemsResponse = new();

            foreach (BasketItemRequest basketItemRequest in basketItemRequests)
            {
                if (basketItemsResponse.Any(b => b.ProductCode.Equals(basketItemRequest.ProductCode)))
                    continue;

                BasketItemResponse basketItemResponse = new()
                {
                    ProductCode = basketItemRequest.ProductCode,
                    Price = basketItemRequest.Price * basketItemRequest.Quantity
                };

                List<string> itemCampaignNames = GetCampaignNames(new() { basketItemRequest });

                List<Domain.Entities.Campaign> itemCampaigns = campaigns.Where(c => itemCampaignNames.Contains(c.Name.ToLower())).ToList();

                if (!itemCampaigns.Any())
                {
                    basketItemResponse.Discount = GetNullDiscount(basketItemRequest);
                    basketItemsResponse.Add(basketItemResponse);
                    continue;
                }

                Domain.Entities.Campaign itemMinimumBasketCampaigns = itemCampaigns.FirstOrDefault(i => i.MinimumBasketPrice > 0);

                if (itemMinimumBasketCampaigns is not null)
                {
                    if (itemMinimumBasketCampaigns.MinimumBasketType == MinimumBasketType.Total && basketTotal >= itemMinimumBasketCampaigns.MinimumBasketPrice)
                    {
                        basketItemsResponse.AddRange(GetDiscountByCampaign(itemMinimumBasketCampaigns, basketItemRequests));
                    }
                    else if (itemMinimumBasketCampaigns.MinimumBasketType == MinimumBasketType.Campaign
                        && GetCampaigntBasketTotalPrice(itemMinimumBasketCampaigns.Name, basketItemRequests) >= itemMinimumBasketCampaigns.MinimumBasketPrice)
                    {
                        basketItemsResponse.AddRange(GetDiscountByCampaign(itemMinimumBasketCampaigns, basketItemRequests));
                    }
                    else
                    {
                        basketItemResponse.Discount = GetNullDiscount(basketItemRequest);
                        basketItemsResponse.Add(basketItemResponse);
                    }
                }
                else
                {
                    basketItemResponse.Discount = GetDiscount(itemCampaigns, basketItemRequest);
                    basketItemsResponse.Add(basketItemResponse);
                }

            }

            return basketItemsResponse;
        }

        private static decimal GetCampaigntBasketTotalPrice(string name, List<BasketItemRequest> basketItemRequests)
        {
            List<BasketItemRequest> basketItemRequestByCampaign = GetBasketItemRequestByCampaign(name, basketItemRequests);

            return basketItemRequestByCampaign.Sum(b => b.Price);
        }

        private static List<BasketItemRequest> GetBasketItemRequestByCampaign(string name, List<BasketItemRequest> basketItemRequests)
        {
            List<BasketItemRequest> basketItemRequestsByCampaign = new();
            foreach (BasketItemRequest basketItemRequest in basketItemRequests)
            {
                List<string> itemCampaignNames = GetCampaignNames(new() { basketItemRequest });
                if (itemCampaignNames.Contains(name))
                    basketItemRequestsByCampaign.Add(basketItemRequest);
            }
            return basketItemRequestsByCampaign;
        }

        private static List<string> GetCampaignNames(List<BasketItemRequest> basketItemRequests)
        {
            List<string> campaignNames = basketItemRequests
                .Select(b => new List<string> { b.ProductCode, b.Brand, b.CategoryName })
                .SelectMany(b => b)
                .ToList();

            List<string> allProductGroups = basketItemRequests
                .Select(b => b.ProductGroups)
                .SelectMany(b => b)
                .ToList();

            campaignNames.AddRange(allProductGroups);
            campaignNames = campaignNames
                .Distinct()
                .Select(n => n.ToLower()).ToList();

            return campaignNames;
        }

        private static DiscountResponse GetNullDiscount(BasketItemRequest basketItemRequest) =>
            new()
            {
                DiscountPrice = 0,
                CampaignId = null,
                DiscountedPrice = (basketItemRequest.Price * basketItemRequest.Quantity)
            };

        private static List<BasketItemResponse> GetDiscountByCampaign(Domain.Entities.Campaign campaign, List<BasketItemRequest> basketItemRequests)
        {
            List<BasketItemResponse> basketItemsResponse = new();
            List<BasketItemRequest> basketItemRequestByCampaign = GetBasketItemRequestByCampaign(campaign.Name, basketItemRequests);

            foreach (BasketItemRequest basketItemRequest in basketItemRequestByCampaign)
            {
                decimal discount = campaign.DiscountType == DiscountType.Rate
                     ? GetCalculatedDiscountedPrice(campaign, basketItemRequest)
                     : GetCalculatedDiscountedPrice(campaign, basketItemRequests.FirstOrDefault()) / basketItemRequestByCampaign.Count;
                BasketItemResponse basketItemResponse = new()
                {
                    ProductCode = basketItemRequest.ProductCode,
                    Price = basketItemRequest.Price * basketItemRequest.Quantity,
                    Discount = new()
                    {
                        DiscountPrice = discount,
                        CampaignId = campaign.Id.ToString(),
                        DiscountedPrice = (basketItemRequest.Price * basketItemRequest.Quantity) - discount
                    }
                };
                basketItemsResponse.Add(basketItemResponse);
            }

            return basketItemsResponse;
        }

        private static DiscountResponse GetDiscount(List<Domain.Entities.Campaign> campaigns, BasketItemRequest basketItemRequest)
        {
            List<DiscountResponse> discountResponses = new();

            foreach (Domain.Entities.Campaign campaign in campaigns)
                discountResponses.Add(GetCalculatedDiscount(campaign, basketItemRequest));

            return discountResponses.OrderByDescending(d => d.DiscountedPrice).FirstOrDefault();
        }

        private static DiscountResponse GetCalculatedDiscount(Domain.Entities.Campaign campaign, BasketItemRequest basketItemRequest)
        {
            decimal discount = GetCalculatedDiscountedPrice(campaign, basketItemRequest);

            DiscountResponse discountResponse = new()
            {
                DiscountPrice = discount,
                CampaignId = campaign.Id.ToString(),
                DiscountedPrice = (basketItemRequest.Price * basketItemRequest.Quantity) - discount
            };

            return discountResponse;
        }

        private static decimal GetCalculatedDiscountedPrice(Domain.Entities.Campaign campaign, BasketItemRequest basketItemRequest)
        {
            int quantity = campaign.Quantity >= basketItemRequest.Quantity
                ? basketItemRequest.Quantity
                : campaign.Quantity;

            decimal discount;
            _ = campaign.DiscountType switch
            {
                DiscountType.None => discount = 0,
                DiscountType.Rate => discount = (basketItemRequest.Price * campaign.Discount / 100) * quantity,
                DiscountType.Price => discount = campaign.MinimumBasketType == MinimumBasketType.None
                ? campaign.Discount * quantity
                : campaign.Discount,
                _ => discount = 0
            };

            return discount;
        }
    }
}