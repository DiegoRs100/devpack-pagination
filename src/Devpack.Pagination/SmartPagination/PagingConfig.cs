using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Devpack.Pagination.Tests")]
namespace Devpack.Pagination.SmartPagination
{
    internal static class PagingConfig
    {
        internal static int MaxPageSize { get; set; } = int.MaxValue;
        internal static int DefaultPageSize { get; set; } = 25;
    }
}