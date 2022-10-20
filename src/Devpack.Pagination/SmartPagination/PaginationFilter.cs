using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Devpack.Pagination.SmartPagination
{
    public class PaginationFilter : IActionFilter
    {
        [ExcludeFromCodeCoverage]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not Implemented
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var pagedArgument = context.ActionArguments.Select(a => a.Value).FirstOrDefault(a => a is PagedInputModel);

            if (pagedArgument == null)
                return;

            var validateResult = (pagedArgument as PagedInputModel)!.Validate();

            if (validateResult.IsValid)
                return;

            foreach (var error in validateResult.Errors)
                context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}