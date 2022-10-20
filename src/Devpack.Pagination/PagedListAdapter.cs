namespace Devpack.Pagination
{
    public class PagedListAdapter<TData>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int? TotalCount { get; set; }
        public List<TData> Data { get; set; } = default!;

        public PagedList<TData> BuildPagedList()
        {
            return TotalCount == null
                ? new PagedList<TData>(Data, PageIndex, PageSize)
                : new PagedList<TData>(Data, PageIndex, PageSize, TotalCount.Value);
        }
    }
}