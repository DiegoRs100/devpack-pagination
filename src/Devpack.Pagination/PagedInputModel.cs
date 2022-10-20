using Devpack.Extensions.Types;
using Devpack.Pagination.SmartPagination;
using FluentValidation.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Devpack.Pagination
{
    public class PagedInputModel
    {
        [SwaggerParameter("Is the number of page required. If it is not sent, we set 1 by default.")]
        public int PageIndex { get; set; } = 1;

        [SwaggerParameter("This is the quantity of registers to return by page. If it is not sent, the application's default value will be assigned.")]
        public int PageSize { get; set; } = PagingConfig.DefaultPageSize;

        [SwaggerParameter("In case the data you are trying to page needs to be filtered by some kind of value, the search text must be passed here.")]
        public string? SearchQuery { get; set; } = default!;

        [SwaggerParameter("Indicates to the backend whether the search performed should or should not return the total number of records. (Default value: true)")]
        public bool TotalCountRequested { get; set; } = true;

        public int Skip => (PageIndex - 1) * PageSize;
        public virtual bool HasSearchQuery => !SearchQuery!.IsNullOrWhiteSpace();

        public ValidationResult Validate()
        {
            var validations = new PagedInputModelValidation();
            return validations.Validate(this);
        }
    }
}