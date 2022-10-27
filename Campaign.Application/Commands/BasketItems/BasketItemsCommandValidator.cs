using Campaign.Application.Request;
using FluentValidation;

namespace Campaign.Application.Commands.BasketItems
{
    public class BasketItemsCommandValidator : AbstractValidator<BasketItemsCommand>
    {
        public BasketItemsCommandValidator()
        {
            RuleFor(k => k.BasketItemRequests)
                .NotNull().WithMessage("Items can not be null.");

            RuleForEach(x => x.BasketItemRequests).SetValidator(new BasketItemValidator());
        }
    }

    public class BasketItemValidator : AbstractValidator<BasketItemRequest>
    {
        public BasketItemValidator()
        {
            RuleFor(k => k.Brand)
                .NotEmpty().WithMessage("Brand can not be null.");

            RuleFor(k => k.Quantity)
                .NotEmpty().WithMessage("Quantity can not empty.")
                .GreaterThan(0).WithMessage("Quantity must greater than {0}.");

            RuleFor(k => k.CategoryId)
                .NotEmpty().WithMessage("Category Id can not empty.")
                .GreaterThan(0).WithMessage("Category Id must greater than {0}.");

            RuleFor(k => k.CategoryName)
                .NotEmpty().WithMessage("Category Name can not be null.");

            RuleFor(k => k.Price)
                .NotEmpty().WithMessage("Price can not empty.")
                .GreaterThan(0).WithMessage("Price must greater than {0}.");

            RuleFor(k => k.ProductCode)
                .NotEmpty().WithMessage("Product Code can not be null.");

            RuleFor(k => k.ProductGroups)
                .NotNull().WithMessage("Product Groups can not be null.");

            RuleFor(k => k.ProductName)
                .NotEmpty().WithMessage("ProductName can not be null.");
        }
    }
}