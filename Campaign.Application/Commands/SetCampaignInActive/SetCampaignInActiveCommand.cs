using Campaign.Application.Exceptions;
using Campaign.Application.Services.Interfaces;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Commands.SetCampaignInActive
{
    public class SetCampaignInActiveCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class SetCampaignInActiveCommandHandler : IRequestHandler<SetCampaignInActiveCommand, bool>
    {
        private readonly ICampaignService _campaignService;

        public SetCampaignInActiveCommandHandler(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public async Task<bool> Handle(SetCampaignInActiveCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.Id, out ObjectId campaignId))
                throw new NotTypeOfObjectIdException("Not a BSON type ObjectId for Campaign.");

            var result = await _campaignService.SetCampaignInActiveAsync(campaignId);

            return result;
        }
    }
}