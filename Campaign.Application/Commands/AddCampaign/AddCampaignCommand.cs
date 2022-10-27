using Campaign.Application.Services.Interfaces;
using Campaign.Domain.Common.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Commands.AddCampaign
{
    public class AddCampaignCommand : IRequest<string>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MinimumBasketPrice { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Discount { get; set; }
        public CamapignType CamapignType { get; set; }
    }

    public class AddCampaignCommandHandler : IRequestHandler<AddCampaignCommand, string>
    {
        private readonly ICampaignService _campaignService;

        public AddCampaignCommandHandler(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public async Task<string> Handle(AddCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaignId = await _campaignService.AddCampaignAsync(new Domain.Entities.Campaign
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MinimumBasketPrice = request.MinimumBasketPrice,
                Quantity = request.Quantity,
                DiscountType = request.DiscountType,
                Discount = request.Discount,
                CamapignType = request.CamapignType
            });

            return campaignId;
        }
    }
}