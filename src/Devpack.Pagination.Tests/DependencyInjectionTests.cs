using Bogus;
using Devpack.Pagination.SmartPagination;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Devpack.Pagination.Tests
{
    public class DependencyInjectionTests
    {
        [Fact(DisplayName = "Deve definir as opções default de paginação de forma correta quando o método for chamado.")]
        [Trait("Category", "Extensions")]
        public void AddSmartPagination()
        {
            var faker = new Faker();
            var defaultPageSize = faker.Random.Number(1, 10);
            var maxPageSize = faker.Random.Number(11, 20);

            var service = new ServiceCollection();

            service.AddSmartPagination(options => options
                .UseDefaultPageSize(defaultPageSize)
                .UseMaxPageSize(maxPageSize));

            new PagedInputModel().PageSize.Should().Be(defaultPageSize);
            PagingConfig.MaxPageSize.Should().Be(maxPageSize);
        }
    }
}