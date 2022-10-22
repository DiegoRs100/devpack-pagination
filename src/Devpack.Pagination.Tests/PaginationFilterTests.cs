using Devpack.Pagination.SmartPagination;
using Devpack.Pagination.Tests.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;

namespace Devpack.Pagination.Tests
{
    public class PaginationFilterTests
    {
        [Fact(DisplayName = "Não deve alterar a requisição quando a action não possuir parâmetros do tipo (PagedInputModel).")]
        [Trait("Category", "Filters")]
        public void OnActionExecuting_WnenNotParameters()
        {
            var executingContext = ActionExecutingContextFactory.CreateNotPagingcontext();
            var filter = new PaginationFilter();

            filter.OnActionExecuting(executingContext);

            executingContext.ModelState.IsValid.Should().BeTrue();
            executingContext.Result.Should().BeOfType<OkResult>();
        }

        [Fact(DisplayName = "Não deve alterar a requisição quando os dados de paginação forem válidos.")]
        [Trait("Category", "Filters")]
        public void OnActionExecuting_WnenValidPagination()
        {
            var executingContext = ActionExecutingContextFactory.CreateValidPagingcontext();
            var filter = new PaginationFilter();

            filter.OnActionExecuting(executingContext);

            executingContext.ModelState.IsValid.Should().BeTrue();
            executingContext.Result.Should().BeOfType<OkResult>();
        }

        [Fact(DisplayName = "Deve retornar um bad request quando os dados de paginação forem inválidos.")]
        [Trait("Category", "Filters")]
        public void OnActionExecuting_WnenInvalidPagination()
        {
            var executingContext = ActionExecutingContextFactory.CreateInvalidPagingcontext();
            var filter = new PaginationFilter();

            var modelStateMock = new ModelStateDictionary();
            modelStateMock.AddModelError(nameof(PagedInputModel.PageIndex), $"The property ({nameof(PagedInputModel.PageIndex)}) must be greater than 0.");
            modelStateMock.AddModelError(nameof(PagedInputModel.PageSize), $"The property ({nameof(PagedInputModel.PageSize)}) must be greater than 0.");

            var expectedErrors = new SerializableError(modelStateMock);

            filter.OnActionExecuting(executingContext);
            var content = executingContext.Result as BadRequestObjectResult;

            executingContext.Result.Should().BeOfType<BadRequestObjectResult>();
            content!.Value.Should().BeEquivalentTo(expectedErrors);
        }
    }
}