using Bogus;
using Devpack.Pagination.SmartPagination;
using FluentAssertions;
using System;
using Xunit;

namespace Devpack.Pagination.Tests
{
    public class PaginationBuilderTests
    {
        [Fact(DisplayName = "Deve atribuir a configuração padrão de MaxPageSize quando uma valor válido for passado.")]
        [Trait("Category", "Builders")]
        public void UseMaxPageSize()
        {
            var faker = new Faker();
            var maxPageSize = faker.Random.Number(10, 20);

            new PaginationBuilder()
                .UseDefaultPageSize(faker.Random.Number(1, 10))
                .UseMaxPageSize(maxPageSize);

            PagingConfig.MaxPageSize.Should().Be(maxPageSize);
        }

        [Fact(DisplayName = "Deve lançar uma exception quando o valor de MaxPageSize for menor que o valor de DefaultOageSize.")]
        [Trait("Category", "Builders")]
        public void UseMaxPageSize_WhenException()
        {
            var faker = new Faker();
            var defaultPageSize = faker.Random.Number(10, 20);

            var builder = new PaginationBuilder()
                .UseDefaultPageSize(defaultPageSize);

            builder.Invoking(b => b.UseMaxPageSize(faker.Random.Number(1, 9))).Should()
                .Throw<InvalidOperationException>().WithMessage("It is not possible to set the maximum page size to a value smaller than the default page size.")
                .WithInnerException<Exception>().WithMessage($"The default value of the page size is currently {defaultPageSize}.");
        }

        [Fact(DisplayName = "Deve atribuir a configuração padrão de DefaultPageSize quando uma valor válido for passado.")]
        [Trait("Category", "Builders")]
        public void UseDefaultPageSize()
        {
            var defaultPageSize = new Faker().Random.Number(1, 20);
            new PaginationBuilder().UseDefaultPageSize(defaultPageSize);

            PagingConfig.DefaultPageSize.Should().Be(defaultPageSize);
        }
    }
}