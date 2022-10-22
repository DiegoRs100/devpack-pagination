using Bogus;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Devpack.Pagination.Tests
{
    public class PagedListAdapterTests
    {
        [Fact(DisplayName = "Deve retornar um PagedList do tipo countable quando o TotalCount estiver presente no adapter.")]
        [Trait("Category", "Models")]
        public void BuildPagedList_Countable()
        {
            var faker = new Faker();

            var adapter = new PagedListAdapter<Guid>()
            {
                PageIndex = faker.Random.Number(1, 100),
                PageSize = faker.Random.Number(1, 100),
                TotalCount = faker.Random.Number(1, 100),
                Data = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() }
            };

            var pagedList = adapter.BuildPagedList();

            pagedList.PageIndex.Should().Be(adapter.PageIndex);
            pagedList.PageSize.Should().Be(adapter.PageSize);
            pagedList.TotalCount.Should().Be(adapter.TotalCount);
            pagedList.Data.Should().BeEquivalentTo(adapter.Data);
        }

        [Fact(DisplayName = "Deve retornar um PagedList do tipo infinity quando o TotalCount não estiver presente no adapter.")]
        [Trait("Category", "Models")]
        public void BuildPagedList_Infinity()
        {
            var faker = new Faker();

            var adapter = new PagedListAdapter<Guid>()
            {
                PageIndex = faker.Random.Number(1, 100),
                PageSize = faker.Random.Number(1, 100),
                Data = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() }
            };

            var pagedList = adapter.BuildPagedList();

            pagedList.PageIndex.Should().Be(adapter.PageIndex);
            pagedList.PageSize.Should().Be(adapter.PageSize);
            pagedList.PaginationType.Should().Be(PaginationType.Infinity);
            pagedList.Data.Should().BeEquivalentTo(adapter.Data);
        }
    }
}