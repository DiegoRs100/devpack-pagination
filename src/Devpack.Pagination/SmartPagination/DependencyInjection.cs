using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Devpack.Pagination.SmartPagination
{
    public static class DependencyInjection
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddSmartPagination(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(c => c.Filters.Add<PaginationFilter>());
            return services;
        }

        public static IServiceCollection AddSmartPagination(this IServiceCollection services, Action<PaginationBuilder> options)
        {
            options.Invoke(new PaginationBuilder());
            return services.AddSmartPagination();
        }
    }
}