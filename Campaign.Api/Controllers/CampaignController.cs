using Campaign.Api.Models;
using Campaign.Application.Commands.AddCampaign;
using Campaign.Application.Commands.BasketItems;
using Campaign.Application.Commands.SetCampaignInActive;
using Campaign.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Campaign.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator mediator;

        public CampaignController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(BaseResponseViewModel<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponseViewModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponseViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddCampaignAsync([FromBody] AddCampaignCommand command)
        {
            var result = await mediator.Send(command);
            var returnModel = new BaseResponseViewModel<string>
            {
                Data = result
            };

            return Ok(returnModel);
        }

        [HttpPost("set-campaign-in-active")]
        [ProducesResponseType(typeof(BaseResponseViewModel<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponseViewModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponseViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCampaignInActiveAsync([FromBody] SetCampaignInActiveCommand command)
        {
            var result = await mediator.Send(command);
            var returnModel = new BaseResponseViewModel<bool>
            {
                Data = result
            };

            return Ok(returnModel);
        }

        [HttpPost("set-campaign-to-basket")]
        [ProducesResponseType(typeof(BaseResponseViewModel<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponseViewModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponseViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCampaignToBasketAsync([FromBody] BasketItemsCommand command)
        {
            var result = await mediator.Send(command);
            var returnModel = new BaseResponseViewModel<BasketItemsResponse>
            {
                Data = result
            };

            return Ok(returnModel);
        }
    }
}