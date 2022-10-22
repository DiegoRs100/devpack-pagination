using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Devpack.Pagination.Tests
{
    public class ExtensionsTests
    {
        [Fact(DisplayName = "Deve montar corretamente a queryString quando um objeto do tipo (PagedInputModel) for passado contendo o (SearchQuery).")]
        [Trait("Category", "Extensions")]
        public void Add_WhithSearchQuery()
        {
            var faker = new Faker();

            var input = new PagedInputModel()
            {
                PageIndex = faker.Random.Number(1, 10),
                PageSize = faker.Random.Number(1, 10),
                SearchQuery = faker.Random.String2(10),
                TotalCountRequested = faker.Random.Bool()
            };

            var expectedResult = $"?" +
                $"{nameof(PagedInputModel.PageIndex)}={input.PageIndex}&" +
                $"{nameof(PagedInputModel.PageSize)}={input.PageSize}&" +
                $"{nameof(PagedInputModel.TotalCountRequested)}={input.TotalCountRequested}&" +
                $"{nameof(PagedInputModel.SearchQuery)}={input.SearchQuery}";

            var queryBuilder = new QueryString();
            queryBuilder.Add(input);

            queryBuilder.ToString().Should().Be(expectedResult);
        }

        [Fact(DisplayName = "Deve montar corretamente a queryString quando um objeto do tipo (PagedInputModel) for passado sem o (SearchQuery).")]
        [Trait("Category", "Extensions")]
        public void Add_WhithoutSearchQuery()
        {
            var faker = new Faker();

            var input = new PagedInputModel()
            {
                PageIndex = faker.Random.Number(1, 10),
                PageSize = faker.Random.Number(1, 10),
                TotalCountRequested = faker.Random.Bool()
            };

            var expectedResult = $"?" +
                $"{nameof(PagedInputModel.PageIndex)}={input.PageIndex}&" +
                $"{nameof(PagedInputModel.PageSize)}={input.PageSize}&" +
                $"{nameof(PagedInputModel.TotalCountRequested)}={input.TotalCountRequested}";

            var queryBuilder = new QueryString();
            queryBuilder.Add(input);

            queryBuilder.ToString().Should().Be(expectedResult);
        }
    }
}