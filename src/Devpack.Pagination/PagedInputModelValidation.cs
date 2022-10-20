using Devpack.Pagination.SmartPagination;
using FluentValidation;

namespace Devpack.Pagination
{
    public class PagedInputModelValidation : AbstractValidator<PagedInputModel>
    {
        public PagedInputModelValidation()
        {
            RuleFor(p => p.PageIndex)
                .GreaterThan(0)
                .WithMessage($"The property ({nameof(PagedInputModel.PageIndex)}) must be greater than {{ComparisonValue}}.");

            RuleFor(p => p.PageSize)
                .GreaterThan(0)
                    .WithMessage($"The property ({nameof(PagedInputModel.PageSize)}) must be greater than {{ComparisonValue}}.")
                .LessThanOrEqualTo(PagingConfig.MaxPageSize)
                    .WithMessage($"The property ({nameof(PagedInputModel.PageSize)}) must be less than {{ComparisonValue}}.");
        }
    }
}