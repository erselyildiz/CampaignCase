using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Commands.SetCampaignInActive
{
   public class SetCampaignInActiveCommandValidator : AbstractValidator<SetCampaignInActiveCommand>
    {
        public SetCampaignInActiveCommandValidator()
        {

            RuleFor(k => k.Id)
                .NotEmpty().WithMessage("Id can not be null.")
                .Length(24, 24).WithMessage("Id not a object id.");
        }
    }
}