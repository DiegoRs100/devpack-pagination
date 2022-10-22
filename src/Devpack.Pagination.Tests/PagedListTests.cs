using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Devpack.Pagination.Tests
{
    public class PagedListTests
    {
        [Fact(DisplayName = "Deve retornar (Countable) quando o total de registros for passado no construtor.")]
        [Trait("Category", "Models")]
        public void PaginationType_WhenCountable()
        {
            var pagedList = new PagedList<Guid>(Array.Empty<Guid>(), 1, 10, 50);
            pagedList.PaginationType.Should().Be(PaginationType.Countable);
        }

        [Fact(DisplayName = "Deve retornar (Infinity) quando o total de registros não for passado no construtor.")]
        [Trait("Category", "Models")]
        public void PaginationType_WhenInfinity()
        {
            var pagedList = new PagedList<Guid>(Array.Empty<Guid>(), 1, 10);
            pagedList.PaginationType.Should().Be(PaginationType.Infinity);
        }

        [Fact(DisplayName = "Deve retornar verdadeiro " +
            "quando uma paginação do tipo countable não estiver acessando a última página de uma listagem.")]
        [Trait("Category", "Models")]
        public void HasNextPage_WhenCountable_ReturnTrue()
        {
            var pagedList = new PagedList<Guid>(Array.Empty<Guid>(), 1, 10, 50);
            pagedList.HasNextPage.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso " +
            "quando uma paginação do tipo countable estiver acessando a última página de uma listagem.")]
        [Trait("Category", "Models")]
        public void HasNextPage_WhenCountable_ReturnFalse()
        {
            var pagedList = new PagedList<Guid>(Array.Empty<Guid>(), 5, 10, 50);
            pagedList.HasNextPage.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro " +
            "quando uma paginação do tipo infinity não estiver acessando a última página de uma listagem.")]
        [Trait("Category", "Models")]
        public void HasNextPage_WhenInfinity_ReturnTrue()
        {
            var data = Enumerable.Repeat(Guid.NewGuid(), 10);
            var pagedList = new PagedList<Guid>(data, 1, 10);

            pagedList.HasNextPage.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso " +
            "quando uma paginação do tipo infinity estiver acessando a última página de uma listagem.")]
        [Trait("Category", "Models")]
        public void HasNextPage_WhenInfinity_ReturnFalse()
        {
            var data = Enumerable.Repeat(Guid.NewGuid(), 5);
            var pagedList = new PagedList<Guid>(data, 3, 10);

            pagedList.HasNextPage.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve popular em (Data) apenas resgistros igual ao número do pageSize " +
            "quando a quantidade de registros informada for maior que o (PageSize).")]
        [Trait("Category", "Models")]
        public void Constructor_WhenPageSizeBigger()
        {
            var pageIndex = 2;
            var pageSize = 3;
            var data = Enumerable.Repeat(Guid.NewGuid(), 5);

            var pagedList = new PagedList<Guid>(data, pageIndex, pageSize);

            pagedList.PageIndex.Should().Be(pageIndex);
            pagedList.PageSize.Should().Be(pageSize);
            pagedList.Data.Should().BeEquivalentTo(data.Take(pageSize));
        }

        [Fact(DisplayName = "Deve popular todas as propriedade corretamente " +
            "quando o objeto for instanciado e a quantidade de registros for menor ou igual ao (PageSize).")]
        [Trait("Category", "Models")]
        public void Constructor_WhenPageSizeLower()
        {
            var pageIndex = 2;
            var pageSize = 10;
            var data = Enumerable.Repeat(Guid.NewGuid(), 5);

            var pagedList = new PagedList<Guid>(data, pageIndex, pageSize);

            pagedList.PageIndex.Should().Be(pageIndex);
            pagedList.PageSize.Should().Be(pageSize);
            pagedList.Data.Should().BeEquivalentTo(data);
        }

        [Fact(DisplayName = "Deve popular corretamente a propriedade (TotalCount) ela for passada no construtor.")]
        [Trait("Category", "Models")]
        public void Constructor_UsingTotalCount()
        {
            var pageIndex = 2;
            var pageSize = 10;
            var totalCount = 525;
            var data = Enumerable.Repeat(Guid.NewGuid(), 5);

            var pagedList = new PagedList<Guid>(data, pageIndex, pageSize, totalCount);

            pagedList.PageIndex.Should().Be(pageIndex);
            pagedList.PageSize.Should().Be(pageSize);
            pagedList.TotalCount.Should().Be(totalCount);
            pagedList.Data.Should().BeEquivalentTo(data);
        }
    }
}