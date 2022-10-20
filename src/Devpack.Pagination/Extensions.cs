using Microsoft.AspNetCore.Http;

namespace Devpack.Pagination
{
    public static class Extensions
    {
        public static QueryString Add(this ref QueryString queryParams, PagedInputModel pagedModel)
        {
            queryParams = queryParams.Add(nameof(pagedModel.PageIndex), pagedModel.PageIndex.ToString())
                .Add(nameof(pagedModel.PageSize), pagedModel.PageSize.ToString())
                .Add(nameof(pagedModel.TotalCountRequested), pagedModel.TotalCountRequested.ToString());

            if (pagedModel.HasSearchQuery)
                queryParams = queryParams.Add(nameof(pagedModel.SearchQuery), pagedModel.SearchQuery!);

            return queryParams;
        }
    }
}