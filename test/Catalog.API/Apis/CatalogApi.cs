using System.ComponentModel; // 导入组件模型命名空间，用于参数描述
using System.ComponentModel.DataAnnotations; // 导入数据注解命名空间，用于参数验证
using Microsoft.AspNetCore.Http.HttpResults; // 导入HTTP结果命名空间，用于强类型响应
using Microsoft.AspNetCore.Mvc; // 导入MVC命名空间，提供控制器和操作结果
using Microsoft.AspNetCore.Mvc.Infrastructure; // 导入MVC基础设施命名空间
using Pgvector.EntityFrameworkCore; // 导入PostgreSQL向量支持命名空间，用于语义搜索

namespace eShop.Catalog.API; // 命名空间定义

/// <summary>
/// Catalog API端点定义类，使用最小API风格实现
/// </summary>
public static class CatalogApi
{
    /// <summary>
    /// 扩展方法，用于映射Catalog API端点到路由构建器
    /// </summary>
    /// <param name="app">端点路由构建器实例</param>
    /// <returns>配置后的端点路由构建器</returns>
    public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {
        // 创建版本化API，名称为"Catalog"
        var vApi = app.NewVersionedApi("Catalog");
        // 创建API组，支持版本1.0和2.0
        var api = vApi.MapGroup("api/catalog").HasApiVersion(1, 0).HasApiVersion(2, 0);
        // 创建V1版本API组
        var v1 = vApi.MapGroup("api/catalog").HasApiVersion(1, 0);
        // 创建V2版本API组
        var v2 = vApi.MapGroup("api/catalog").HasApiVersion(2, 0);

        // 商品查询相关路由
        // V1版本的获取所有商品端点
        v1.MapGet("/items", GetAllItemsV1)
            .WithName("ListItems") // 设置端点名称
            .WithSummary("List catalog items") // 设置摘要
            .WithDescription("Get a paginated list of items in the catalog.") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // V2版本的获取所有商品端点
        v2.MapGet("/items", GetAllItems)
            .WithName("ListItems-V2") // 设置端点名称
            .WithSummary("List catalog items") // 设置摘要
            .WithDescription("Get a paginated list of items in the catalog.") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // 批量获取商品端点
        api.MapGet("/items/by", GetItemsByIds)
            .WithName("BatchGetItems") // 设置端点名称
            .WithSummary("Batch get catalog items") // 设置摘要
            .WithDescription("Get multiple items from the catalog") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // 根据ID获取商品端点
        api.MapGet("/items/{id:int}", GetItemById)
            .WithName("GetItem") // 设置端点名称
            .WithSummary("Get catalog item") // 设置摘要
            .WithDescription("Get an item from the catalog") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // V1版本的根据名称获取商品端点
        v1.MapGet("/items/by/{name:minlength(1)}", GetItemsByName)
            .WithName("GetItemsByName") // 设置端点名称
            .WithSummary("Get catalog items by name") // 设置摘要
            .WithDescription("Get a paginated list of catalog items with the specified name.") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // 获取商品图片端点
        api.MapGet("/items/{id:int}/pic", GetItemPictureById)
            .WithName("GetItemPicture") // 设置端点名称
            .WithSummary("Get catalog item picture") // 设置摘要
            .WithDescription("Get the picture for a catalog item") // 设置描述
            .WithTags("Items"); // 设置标签

        // AI相关路由
        // V1版本的语义搜索端点
        v1.MapGet("/items/withsemanticrelevance/{text:minlength(1)}", GetItemsBySemanticRelevanceV1)
            .WithName("GetRelevantItems") // 设置端点名称
            .WithSummary("Search catalog for relevant items") // 设置摘要
            .WithDescription("Search the catalog for items related to the specified text") // 设置描述
            .WithTags("Search"); // 设置标签

        // V2版本的语义搜索端点
        v2.MapGet("/items/withsemanticrelevance", GetItemsBySemanticRelevance)
            .WithName("GetRelevantItems-V2") // 设置端点名称
            .WithSummary("Search catalog for relevant items") // 设置摘要
            .WithDescription("Search the catalog for items related to the specified text") // 设置描述
            .WithTags("Search"); // 设置标签

        // 按类型和品牌查询路由
        // V1版本的按类型和品牌获取商品端点
        v1.MapGet("/items/type/{typeId}/brand/{brandId?}", GetItemsByBrandAndTypeId)
            .WithName("GetItemsByTypeAndBrand") // 设置端点名称
            .WithSummary("Get catalog items by type and brand") // 设置摘要
            .WithDescription("Get catalog items of the specified type and brand") // 设置描述
            .WithTags("Types"); // 设置标签
        
        // V1版本的按品牌获取商品端点
        v1.MapGet("/items/type/all/brand/{brandId:int?}", GetItemsByBrandId)
            .WithName("GetItemsByBrand") // 设置端点名称
            .WithSummary("List catalog items by brand") // 设置摘要
            .WithDescription("Get a list of catalog items for the specified brand") // 设置描述
            .WithTags("Brands"); // 设置标签
        
        // 获取所有商品类型端点
        api.MapGet("/catalogtypes",
            [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置响应类型
            async (CatalogContext context) => await context.CatalogTypes.OrderBy(x => x.Type).ToListAsync()) // 内联处理函数
            .WithName("ListItemTypes") // 设置端点名称
            .WithSummary("List catalog item types") // 设置摘要
            .WithDescription("Get a list of the types of catalog items") // 设置描述
            .WithTags("Types"); // 设置标签
        
        // 获取所有商品品牌端点
        api.MapGet("/catalogbrands",
            [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置响应类型
            async (CatalogContext context) => await context.CatalogBrands.OrderBy(x => x.Brand).ToListAsync()) // 内联处理函数
            .WithName("ListItemBrands") // 设置端点名称
            .WithSummary("List catalog item brands") // 设置摘要
            .WithDescription("Get a list of the brands of catalog items") // 设置描述
            .WithTags("Brands"); // 设置标签

        // 商品修改相关路由
        // V1版本的更新商品端点
        v1.MapPut("/items", UpdateItemV1)
            .WithName("UpdateItem") // 设置端点名称
            .WithSummary("Create or replace a catalog item") // 设置摘要
            .WithDescription("Create or replace a catalog item") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // V2版本的更新商品端点
        v2.MapPut("/items/{id:int}", UpdateItem)
            .WithName("UpdateItem-V2") // 设置端点名称
            .WithSummary("Create or replace a catalog item") // 设置摘要
            .WithDescription("Create or replace a catalog item") // 设置描述
            .WithTags("Items"); // 设置标签
        
        // 创建商品端点
        api.MapPost("/items", CreateItem)
            .WithName("CreateItem") // 设置端点名称
            .WithSummary("Create a catalog item") // 设置摘要
            .WithDescription("Create a new item in the catalog"); // 设置描述
        
        // 删除商品端点
        api.MapDelete("/items/{id:int}", DeleteItemById)
            .WithName("DeleteItem") // 设置端点名称
            .WithSummary("Delete catalog item") // 设置摘要
            .WithDescription("Delete the specified catalog item"); // 设置描述

        return app; // 返回配置后的端点路由构建器
    }

    /// <summary>
    /// V1版本的获取所有商品方法
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <returns>包含分页商品列表的Ok结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Ok<PaginatedItems<CatalogItem>>> GetAllItemsV1(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数，使用AsParameters特性自动绑定
        [AsParameters] CatalogServices services) // 服务参数，使用AsParameters特性自动绑定
    {
        return await GetAllItems(paginationRequest, services, null, null, null); // 调用通用方法，不传入过滤参数
    }

    /// <summary>
    /// 通用的获取所有商品方法，支持按名称、类型和品牌过滤
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="name">商品名称过滤</param>
    /// <param name="type">商品类型ID过滤</param>
    /// <param name="brand">商品品牌ID过滤</param>
    /// <returns>包含分页商品列表的Ok结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Ok<PaginatedItems<CatalogItem>>> GetAllItems(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The name of the item to return")] string? name, // 商品名称参数
        [Description("The type of items to return")] int? type, // 商品类型参数
        [Description("The brand of items to return")] int? brand) // 商品品牌参数
    {
        var pageSize = paginationRequest.PageSize; // 获取页大小
        var pageIndex = paginationRequest.PageIndex; // 获取页索引

        var root = (IQueryable<CatalogItem>)services.Context.CatalogItems; // 获取商品查询

        if (name is not null) // 如果提供了名称
        {
            root = root.Where(c => c.Name.StartsWith(name)); // 按名称前缀过滤
        }
        if (type is not null) // 如果提供了类型
        {
            root = root.Where(c => c.CatalogTypeId == type); // 按类型ID过滤
        }
        if (brand is not null) // 如果提供了品牌
        {
            root = root.Where(c => c.CatalogBrandId == brand); // 按品牌ID过滤
        }

        var totalItems = await root // 获取总商品数
            .LongCountAsync();

        var itemsOnPage = await root // 获取当前页的商品
            .OrderBy(c => c.Name) // 按名称排序
            .Skip(pageSize * pageIndex) // 跳过前面的商品
            .Take(pageSize) // 取当前页的商品
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage)); // 返回包含分页商品列表的Ok结果
    }

    /// <summary>
    /// 按ID批量获取商品的方法
    /// </summary>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="ids">商品ID数组</param>
    /// <returns>包含商品列表的Ok结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Ok<List<CatalogItem>>> GetItemsByIds(
        [AsParameters] CatalogServices services, // 服务参数
        [Description("List of ids for catalog items to return")] int[] ids) // ID数组参数
    {
        var items = await services.Context.CatalogItems.Where(item => ids.Contains(item.Id)).ToListAsync(); // 按ID查询商品
        return TypedResults.Ok(items); // 返回包含商品列表的Ok结果
    }

    /// <summary>
    /// 按ID获取单个商品的方法
    /// </summary>
    /// <param name="httpContext">HTTP上下文</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="id">商品ID</param>
    /// <returns>包含商品的Ok结果，或未找到结果，或错误请求结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Results<Ok<CatalogItem>, NotFound, BadRequest<ProblemDetails>>> GetItemById(
        HttpContext httpContext, // HTTP上下文
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The catalog item id")] int id) // 商品ID参数
    {
        if (id <= 0) // 验证ID是否有效
        {
            return TypedResults.BadRequest<ProblemDetails>(new (){ // 返回错误请求
                Detail = "Id is not valid"
            });
        }

        var item = await services.Context.CatalogItems.Include(ci => ci.CatalogBrand).SingleOrDefaultAsync(ci => ci.Id == id); // 查询商品，包含品牌信息

        if (item == null) // 如果商品不存在
        {
            return TypedResults.NotFound(); // 返回未找到
        }

        return TypedResults.Ok(item); // 返回包含商品的Ok结果
    }

    /// <summary>
    /// 按名称获取商品的方法
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="name">商品名称</param>
    /// <returns>包含分页商品列表的Ok结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Ok<PaginatedItems<CatalogItem>>> GetItemsByName(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The name of the item to return")] string name) // 商品名称参数
    {
        return await GetAllItems(paginationRequest, services, name, null, null); // 调用通用方法，传入名称参数
    }

    /// <summary>
    /// 获取商品图片的方法
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="environment">Web主机环境</param>
    /// <param name="id">商品ID</param>
    /// <returns>包含图片文件的PhysicalFileHttpResult，或未找到结果</returns>
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK, "application/octet-stream",
        [ "image/png", "image/gif", "image/jpeg", "image/bmp", "image/tiff",
          "image/wmf", "image/jp2", "image/svg+xml", "image/webp" ])] // 设置响应类型
    public static async Task<Results<PhysicalFileHttpResult,NotFound>> GetItemPictureById(
        CatalogContext context, // 数据库上下文
        IWebHostEnvironment environment, // Web主机环境
        [Description("The catalog item id")] int id) // 商品ID参数
    {
        var item = await context.CatalogItems.FindAsync(id); // 查询商品

        if (item is null || item.PictureFileName is null) // 如果商品不存在或没有图片
        {
            return TypedResults.NotFound(); // 返回未找到
        }

        var path = GetFullPath(environment.ContentRootPath, item.PictureFileName); // 获取图片路径

        string imageFileExtension = Path.GetExtension(item.PictureFileName) ?? string.Empty; // 获取图片扩展名
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension); // 获取MIME类型
        DateTime lastModified = File.GetLastWriteTimeUtc(path); // 获取最后修改时间

        return TypedResults.PhysicalFile(path, mimetype, lastModified: lastModified); // 返回物理文件
    }

    /// <summary>
    /// V1版本的语义搜索方法
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="text">搜索文本</param>
    /// <returns>包含分页商品列表的Ok结果，或重定向结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Results<Ok<PaginatedItems<CatalogItem>>, RedirectToRouteHttpResult>> GetItemsBySemanticRelevanceV1(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The text string to use when search for related items in the catalog")] string text) // 搜索文本参数
    {
        return await GetItemsBySemanticRelevance(paginationRequest, services, text); // 调用通用方法
    }

    /// <summary>
    /// 通用的语义搜索方法
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="text">搜索文本</param>
    /// <returns>包含分页商品列表的Ok结果，或重定向结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Results<Ok<PaginatedItems<CatalogItem>>, RedirectToRouteHttpResult>> GetItemsBySemanticRelevance(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The text string to use when search for related items in the catalog"), Required, MinLength(1)] string text) // 搜索文本参数
    {
        var pageSize = paginationRequest.PageSize; // 获取页大小
        var pageIndex = paginationRequest.PageIndex; // 获取页索引

        if (!services.CatalogAI.IsEnabled) // 如果AI未启用
        {
            return await GetItemsByName(paginationRequest, services, text); // 回退到按名称搜索
        }

        // 创建搜索文本的嵌入向量
        var vector = await services.CatalogAI.GetEmbeddingAsync(text); // 获取文本的嵌入向量

        if (vector is null) // 如果向量为空
        {
            return await GetItemsByName(paginationRequest, services, text); // 回退到按名称搜索
        }

        // 获取商品总数
        var totalItems = await services.Context.CatalogItems
            .LongCountAsync();

        // 获取按相似度排序的商品
        List<CatalogItem> itemsOnPage;
        if (services.Logger.IsEnabled(LogLevel.Debug)) // 如果启用了调试日志
        {
            var itemsWithDistance = await services.Context.CatalogItems // 获取带距离的商品
                .Where(c => c.Embedding != null) // 过滤有嵌入向量的商品
                .Select(c => new { Item = c, Distance = c.Embedding!.CosineDistance(vector) }) // 计算余弦距离
                .OrderBy(c => c.Distance) // 按距离排序
                .Skip(pageSize * pageIndex) // 跳过前面的商品
                .Take(pageSize) // 取当前页的商品
                .ToListAsync();

            services.Logger.LogDebug("Results from {text}: {results}", text, string.Join(", ", itemsWithDistance.Select(i => $"{i.Item.Name} => {i.Distance}"))); // 记录调试日志

            itemsOnPage = itemsWithDistance.Select(i => i.Item).ToList(); // 提取商品列表
        }
        else // 如果未启用调试日志
        {
            itemsOnPage = await services.Context.CatalogItems // 直接获取商品
                .Where(c => c.Embedding != null) // 过滤有嵌入向量的商品
                .OrderBy(c => c.Embedding!.CosineDistance(vector)) // 按距离排序
                .Skip(pageSize * pageIndex) // 跳过前面的商品
                .Take(pageSize) // 取当前页的商品
                .ToListAsync();
        }

        return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage)); // 返回包含分页商品列表的Ok结果
    }

    /// <summary>
    /// 按品牌和类型获取商品的方法
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="typeId">类型ID</param>
    /// <param name="brandId">品牌ID</param>
    /// <returns>包含分页商品列表的Ok结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Ok<PaginatedItems<CatalogItem>>> GetItemsByBrandAndTypeId(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The type of items to return")] int typeId, // 类型ID参数
        [Description("The brand of items to return")] int? brandId) // 品牌ID参数
    {
        return await GetAllItems(paginationRequest, services, null, typeId, brandId); // 调用通用方法，传入类型和品牌参数
    }

    /// <summary>
    /// 按品牌获取商品的方法
    /// </summary>
    /// <param name="paginationRequest">分页请求参数</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="brandId">品牌ID</param>
    /// <returns>包含分页商品列表的Ok结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Ok<PaginatedItems<CatalogItem>>> GetItemsByBrandId(
        [AsParameters] PaginationRequest paginationRequest, // 分页请求参数
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The brand of items to return")] int? brandId) // 品牌ID参数
    {
        return await GetAllItems(paginationRequest, services, null, null, brandId); // 调用通用方法，传入品牌参数
    }

    /// <summary>
    /// V1版本的更新商品方法
    /// </summary>
    /// <param name="httpContext">HTTP上下文</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="productToUpdate">要更新的商品</param>
    /// <returns>创建结果，或错误请求结果，或未找到结果</returns>
    public static async Task<Results<Created, BadRequest<ProblemDetails>, NotFound<ProblemDetails>>> UpdateItemV1(
        HttpContext httpContext, // HTTP上下文
        [AsParameters] CatalogServices services, // 服务参数
        CatalogItem productToUpdate) // 要更新的商品
    {
        if (productToUpdate?.Id == null) // 验证ID是否提供
        {
            return TypedResults.BadRequest<ProblemDetails>(new (){ // 返回错误请求
                Detail = "Item id must be provided in the request body."
            });
        }
        return await UpdateItem(httpContext, productToUpdate.Id, services, productToUpdate); // 调用通用方法
    }

    /// <summary>
    /// 通用的更新商品方法
    /// </summary>
    /// <param name="httpContext">HTTP上下文</param>
    /// <param name="id">商品ID</param>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="productToUpdate">要更新的商品</param>
    /// <returns>创建结果，或错误请求结果，或未找到结果</returns>
    public static async Task<Results<Created, BadRequest<ProblemDetails>, NotFound<ProblemDetails>>> UpdateItem(
        HttpContext httpContext, // HTTP上下文
        [Description("The id of the catalog item to delete")] int id, // 商品ID参数
        [AsParameters] CatalogServices services, // 服务参数
        CatalogItem productToUpdate) // 要更新的商品
    {
        var catalogItem = await services.Context.CatalogItems.SingleOrDefaultAsync(i => i.Id == id); // 查询商品

        if (catalogItem == null) // 如果商品不存在
        {
            return TypedResults.NotFound<ProblemDetails>(new (){ // 返回未找到
                Detail = $"Item with id {id} not found."
            });
        }

        // 更新商品
        var catalogEntry = services.Context.Entry(catalogItem); // 获取商品的实体条目
        catalogEntry.CurrentValues.SetValues(productToUpdate); // 更新商品值

        catalogItem.Embedding = await services.CatalogAI.GetEmbeddingAsync(catalogItem); // 更新嵌入向量

        var priceEntry = catalogEntry.Property(i => i.Price); // 获取价格属性

        if (priceEntry.IsModified) // 如果价格已修改
        {
            // 创建价格变更事件
            var priceChangedEvent = new ProductPriceChangedIntegrationEvent(catalogItem.Id, productToUpdate.Price, priceEntry.OriginalValue); // 创建价格变更事件

            // 保存事件和上下文变更
            await services.EventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent); // 保存事件和上下文变更

            // 发布事件
            await services.EventService.PublishThroughEventBusAsync(priceChangedEvent); // 发布事件
        }
        else // 如果价格未修改
        {
            await services.Context.SaveChangesAsync(); // 仅保存商品更新
        }
        return TypedResults.Created($"/api/catalog/items/{id}"); // 返回创建结果
    }

    /// <summary>
    /// 创建商品的方法
    /// </summary>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="product">要创建的商品</param>
    /// <returns>创建结果</returns>
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")] // 设置错误响应类型
    public static async Task<Created> CreateItem(
        [AsParameters] CatalogServices services, // 服务参数
        CatalogItem product) // 要创建的商品
    {
        var item = new CatalogItem(product.Name) // 创建新商品
        {
            Id = product.Id,
            CatalogBrandId = product.CatalogBrandId,
            CatalogTypeId = product.CatalogTypeId,
            Description = product.Description,
            PictureFileName = product.PictureFileName,
            Price = product.Price,
            AvailableStock = product.AvailableStock,
            RestockThreshold = product.RestockThreshold,
            MaxStockThreshold = product.MaxStockThreshold
        };
        item.Embedding = await services.CatalogAI.GetEmbeddingAsync(item); // 获取嵌入向量

        services.Context.CatalogItems.Add(item); // 添加到数据库
        await services.Context.SaveChangesAsync(); // 保存变更

        return TypedResults.Created($"/api/catalog/items/{item.Id}"); // 返回创建结果
    }

    /// <summary>
    /// 删除商品的方法
    /// </summary>
    /// <param name="services">Catalog服务集合</param>
    /// <param name="id">商品ID</param>
    /// <returns>无内容结果，或未找到结果</returns>
    public static async Task<Results<NoContent, NotFound>> DeleteItemById(
        [AsParameters] CatalogServices services, // 服务参数
        [Description("The id of the catalog item to delete")] int id) // 商品ID参数
    {
        var item = services.Context.CatalogItems.SingleOrDefault(x => x.Id == id); // 查询商品

        if (item is null) // 如果商品不存在
        {
            return TypedResults.NotFound(); // 返回未找到
        }

        services.Context.CatalogItems.Remove(item); // 从数据库中删除
        await services.Context.SaveChangesAsync(); // 保存变更
        return TypedResults.NoContent(); // 返回无内容结果
    }

    /// <summary>
    /// 根据文件扩展名获取MIME类型的方法
    /// </summary>
    /// <param name="extension">文件扩展名</param>
    /// <returns>MIME类型字符串</returns>
    private static string GetImageMimeTypeFromImageFileExtension(string extension) => extension switch
    {
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".bmp" => "image/bmp",
        ".tiff" => "image/tiff",
        ".wmf" => "image/wmf",
        ".jp2" => "image/jp2",
        ".svg" => "image/svg+xml",
        ".webp" => "image/webp",
        _ => "application/octet-stream",
    };

    /// <summary>
    /// 获取图片完整路径的方法
    /// </summary>
    /// <param name="contentRootPath">内容根路径</param>
    /// <param name="pictureFileName">图片文件名</param>
    /// <returns>图片完整路径</returns>
    public static string GetFullPath(string contentRootPath, string pictureFileName) =>
        Path.Combine(contentRootPath, "Pics", pictureFileName);
}
