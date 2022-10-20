namespace Devpack.Pagination.SmartPagination
{
    public class PaginationBuilder
    {
        public PaginationBuilder UseMaxPageSize(int max)
        {
            if (max < PagingConfig.DefaultPageSize)
            {
                throw new InvalidOperationException("It is not possible to set the maximum page size to a value smaller than the default page size.",
                    new Exception($"The default value of the page size is currently {PagingConfig.DefaultPageSize}."));
            }

            PagingConfig.MaxPageSize = max;
            return this;
        }

        public PaginationBuilder UseDefaultPageSize(int value)
        {
            PagingConfig.DefaultPageSize = value;
            return this;
        }
    }
}