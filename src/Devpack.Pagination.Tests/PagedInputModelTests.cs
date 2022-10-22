using Bogus;
using Devpack.Pagination.SmartPagination;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Devpack.Pagination.Tests
{
    public class PagedInputModelTests
    {
        [Fact(DisplayName = "Deve retornar verdadeiro quando for passado algum valor dentro da propriedade SearchQuery.")]
        [Trait("Category", "Models")]
        public void HasSearchQuery_WhenTrue()
        {
            var model = new PagedInputModel()
            {
                SearchQuery = Guid.NewGuid().ToString()
            };

            model.HasSearchQuery.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando não for passado valor dentro da propriedade SearchQuery.")]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        [Trait("Category", "Models")]
        public void HasSearchQuery_WhenFalse(string searchQuery)
        {
            var model = new PagedInputModel()
            {
                SearchQuery = searchQuery
            };

            model.HasSearchQuery.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar corretamente a quantidade de registros a serem skipados " +
            "quando forem populadas as propriedades (PageIndex) e (PageSize).")]
        [Trait("Category", "Models")]
        [InlineData(1, 0)]
        [InlineData(2, 10)]
        public void Skip(int pageIndex, int skiper)
        {
            var pagedList = new PagedInputModel()
            {
                PageIndex = pageIndex,
                PageSize = 10
            };

            pagedList.Skip.Should().Be(skiper);
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando o objeto for construída corretamente.")]
        [Trait("Category", "Models")]
        public void Validate_BeTrue()
        {
            var faker = new Faker();
            PagingConfig.MaxPageSize = int.MaxValue;

            var model = new PagedInputModel()
            {
                PageIndex = faker.Random.Number(1, 100),
                PageSize = faker.Random.Number(1, 100)
            };

            var validateResult = model.Validate();
            validateResult.IsValid.Should().BeTrue($"{PagingConfig.MaxPageSize}, {model.PageIndex}, {model.PageSize}");
        }

        [Fact(DisplayName = "Deve retornar uma lista de erros quando o objeto for construída incorretamente.")]
        [Trait("Category", "Models")]
        public void Validate_BeFalse()
        {
            PagingConfig.MaxPageSize = int.MaxValue;

            var model = new PagedInputModel()
            {
                PageIndex = 0,
                PageSize = 0
            };

            var validateResult = model.Validate();

            validateResult.Errors.Select(e => e.ErrorMessage).Should().BeEquivalentTo(new List<string>()
            {
                $"The property ({nameof(PagedInputModel.PageIndex)}) must be greater than 0.",
                $"The property ({nameof(PagedInputModel.PageSize)}) must be greater than 0."
            });
        }

        [Fact(DisplayName = "Deve retornar uma lista de erros quando a propriedade for construída incorretamente.")]
        [Trait("Category", "Models")]
        public void Validate_BeFalse_WnenPageSizeOverflow()
        {
            PagingConfig.MaxPageSize = 10;
            var model = new PagedInputModel() { PageSize = 50 };

            var validateResult = model.Validate();

            validateResult.Errors.Select(e => e.ErrorMessage).Should().BeEquivalentTo(new List<string>()
            {
                $"The property ({nameof(PagedInputModel.PageSize)}) must be less than {PagingConfig.MaxPageSize}.",
            });
        }
    }
}