namespace Devpack.Pagination
{
    public class PagedList<TData>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int? TotalCount { get; private set; }
        public List<TData> Data { get; private set; }

        public PaginationType PaginationType => TotalCount != null
            ? PaginationType.Countable
            : PaginationType.Infinity;

        public bool HasNextPage => CalculeHasNext();

        public PagedList(IEnumerable<TData> data, int pageIndex, int pageSize, int totalCount)
            : this(data, pageIndex, pageSize)
        {
            TotalCount = totalCount;
        }

        public PagedList(IEnumerable<TData> data, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;

            Data = pageSize < data.Count()
                ? data.Take(pageSize).ToList()
                : data.ToList();
        }

        private bool CalculeHasNext()
        {
            return PaginationType == PaginationType.Countable
                ? PageIndex * PageSize < TotalCount
                : Data.Count == PageSize;
        }
    }
}