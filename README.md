# What does it do?

Simple library to perform paging records.
There are currently two types of pagination:

- Countable
- Infinity (when we don't know the total item count)

# How to use it?

### Countable Pagination

```csharp
    public PagedList<Product> GetPagedProducts(string name, int take)
    {
        var productsCount = _productRepository.CountByName(name);
        var products = _productRepository.GetByName(name, take);

        return new PagedList(products, 1, 10, productsCount)
    }
```

### Infinity Pagination

```csharp
    public PagedList<Product> GetPagedProducts(string name, int take)
    {
        var products = _productRepository.GetByName(name, take);
        return new PagedList(products, 1, 10)
    }
```

# Properties

-  int PageIndex
-  int PageSize
-  int? TotalCount
-  IEnumerable<TData> Data
-  PaginationType PaginationType (Countable = 0 | Infinity = 1)
-  bool HasNextPage;
-  int Skip ((PageIndex - 1) * PageSize)

# PagedInputModel

It is a standard data entry model for queries whose results are returned in paginated form.

```csharp
    public PagedList<Product> GetPagedProducts(PagedInputModel input)
    {
        var products = _productRepository.GetByName(input.SearchQuery, input.PageSize, input.TotalCountRequested);
        return new PagedList(products, input.PageIndex, input.PageSize)
    }
```

Where TotalCountRequested should be used to indicate to the backend whether the total count of the query should be returned.

# PagedListAdapter

It is not possible to perform the deserialization of a PagedList directly, because of that the library provides an adapter that can be used for deserialization.

```csharp
    public PagedList<Product> Deserialize(string json)
    {
        var adapter = JsonSerializer.Deserialize<PagedListAdapter<Product>>(json);
        var pagedList = adapter.BuildPagedList();

        return pagedList;
    }
```

# Smart Pagination

By default, smart pagination includes a .net filter that validates the paging input data and launches a bad request if this data is invalid.

To use it, write at startup:

```csharp
    public void Configure(IServiceCollection services)
    {
        ..

        services.AddSmartPagination();
    }
```

**OBS:** If the DefaultPageSize and MaxPageSize options are not passed, the application will automatically assume the values ​​below:

- **DefaultPageSize:** 25.
- **MaxPageSize:** 2.147.483.647 (int32 max)

### DefaultPageSize & MaxPageSize

Additionally, you can define some metrics for the validation of paging data in the application, where:

- **DefaultPageSize:** Indicates the value that will automatically be page by page if a PageSize value is not specified.
- **MaxPageSize:** indicates the maximum value of records that the application can return per page.

```csharp
    public void Configure(IServiceCollection services)
    {
        ..

        services.AddSmartPagination(options => options
            .UseDefaultPageSize(10)
            .UseMaxPageSize(50));
    }
```