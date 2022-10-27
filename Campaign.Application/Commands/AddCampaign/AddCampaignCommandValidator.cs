using FluentValidation;
using System;

namespace Campaign.Application.Commands.AddCampaign
{
    public class AddCampaignCommandValidator : AbstractValidator<AddCampaignCommand>
    {
        public AddCampaignCommandValidator()
        {
            RuleFor(k => k.Name)
                .NotEmpty().WithMessage("Name can not be null.");

            RuleFor(k => k.Quantity)
                .NotEmpty().WithMessage("Quantity can not empty.")
                .GreaterThan(0).WithMessage("Quantity must greater than {0}.");

            RuleFor(k => k.Discount)
                .NotEmpty().WithMessage("Discount can not empty.")
                .GreaterThan(0).WithMessage("Discount must greater than {0}.");

            RuleFor(k => k.StartDate)
                .NotEmpty().WithMessage("Start Date can not empty.")
                .GreaterThan(DateTime.Now).WithMessage("Start Date can not be before now");

            RuleFor(k => k.EndDate)
                .NotEmpty().WithMessage("End Date can not empty.")
                .GreaterThan(DateTime.Now).WithMessage("End Date can not be before now");

            RuleFor(k => (int)k.DiscountType)
                .NotEmpty().WithMessage("Discount Type can not empty.")
                .GreaterThan(0).WithMessage("Discount Type must greater than {0}.");

            RuleFor(k => (int)k.CamapignType)
                .NotEmpty().WithMessage("Campaign Type can not empty.")
                .GreaterThan(0).WithMessage("Campaign Type must greater than {0}.");

        }
    }
}