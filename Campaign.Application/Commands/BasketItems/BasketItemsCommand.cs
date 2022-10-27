using Campaign.Application.Request;
using Campaign.Application.Response;
using Campaign.Application.Services.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Commands.BasketItems
{
    public class BasketItemsCommand : IRequest<BasketItemsResponse>
    {
        public List<BasketItemRequest> BasketItemRequests { get; set; }
    }

    public class BasketItemsCommandHandler : IRequestHandler<BasketItemsCommand, BasketItemsResponse>
    {
        private readonly ICampaignService _campaignService;

        public BasketItemsCommandHandler(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public async Task<BasketItemsResponse> Handle(BasketItemsCommand request, CancellationToken cancellationToken) =>
           await _campaignService.SetCampaignToBasketAsync(request.BasketItemRequests);
    }
}