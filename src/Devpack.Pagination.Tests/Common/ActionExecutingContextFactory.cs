using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Devpack.Pagination.SmartPagination;
using System;

namespace Devpack.Pagination.Tests.Common
{
    public static class ActionExecutingContextFactory
    {
        public static ActionExecutingContext CreateNotPagingcontext()
        {
            var actionContext = new ActionContext(Mock.Of<HttpContext>(), Mock.Of<RouteData>(), Mock.Of<ActionDescriptor>(), new ModelStateDictionary());

            var actionExecutingContext = new ActionExecutingContext(actionContext, Array.Empty<IFilterMetadata>(), new Dictionary<string, object?>(), Mock.Of<Controller>())
            {
                Result = new OkResult()
            };

            return actionExecutingContext;
        }

        public static ActionExecutingContext CreateValidPagingcontext()
        {
            PagingConfig.MaxPageSize = 100;

            var actionContext = new ActionContext(Mock.Of<HttpContext>(), Mock.Of<RouteData>(), Mock.Of<ActionDescriptor>(), new ModelStateDictionary());

            var arguments = new Dictionary<string, object?>()
            {
                { "pagination", new PagedInputModel() }
            };

            var actionExecutingContext = new ActionExecutingContext(actionContext, Array.Empty<IFilterMetadata>(), arguments, Mock.Of<Controller>())
            {
                Result = new OkResult(),
            };

            return actionExecutingContext;
        }

        public static ActionExecutingContext CreateInvalidPagingcontext()
        {
            var actionContext = new ActionContext(Mock.Of<HttpContext>(), Mock.Of<RouteData>(), Mock.Of<ActionDescriptor>(), new ModelStateDictionary());

            var arguments = new Dictionary<string, object?>()
            {
                { "pagination", new PagedInputModel() { PageIndex = 0, PageSize = 0 } }
            };

            var actionExecutingContext = new ActionExecutingContext(actionContext, Array.Empty<IFilterMetadata>(), arguments, Mock.Of<Controller>())
            {
                Result = new OkResult(),
            };

            return actionExecutingContext;
        }
    }
}