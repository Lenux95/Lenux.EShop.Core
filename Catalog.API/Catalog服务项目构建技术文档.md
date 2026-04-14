# Catalog服务项目构建技术文档

## 1. 项目概述与目标

### 1.1 核心功能
Catalog服务是一个商品目录管理系统，主要负责管理商品信息、库存和品牌类型等数据。其核心功能包括：

- 商品目录管理（增删改查）
- 品牌和类型管理
- 库存管理（增加/减少库存）
- 基于AI的语义搜索
- 商品图片管理
- 价格变更事件发布

### 1.2 应用场景及价值
- **电子商务平台**：作为商品信息的中央存储和管理系统
- **多渠道销售**：为不同销售渠道提供统一的商品信息
- **库存管理**：实时跟踪商品库存状态，避免超卖
- **智能搜索**：提供基于语义的商品搜索功能，提升用户体验
- **数据集成**：与购物车、订单等系统集成，实现完整的电商流程

### 1.3 关键功能点
1. **商品管理**：创建、更新、删除、查询商品
2. **分类管理**：管理商品品牌和类型
3. **库存管理**：跟踪和更新商品库存
4. **搜索功能**：支持按名称、品牌、类型搜索，以及语义搜索
5. **图片管理**：存储和提供商品图片
6. **事件通知**：价格变更时通知其他服务
7. **API版本控制**：支持多版本API

### 1.4 非功能需求
- **性能**：API响应时间<500ms，支持100+ QPS
- **安全性**：身份验证、授权、数据加密
- **可扩展性**：支持水平扩展，应对流量增长
- **可靠性**：99.9%可用性，数据备份与恢复
- **可维护性**：代码可读性、文档完整性、测试覆盖率
- **兼容性**：支持不同客户端平台

### 1.5 成功标准与验收指标
- 所有API端点正常工作，符合设计规范
- 性能满足要求，响应时间在规定范围内
- 安全性测试通过，无漏洞
- 代码测试覆盖率>80%
- 服务稳定运行，无崩溃或异常
- 文档完整，可指导新开发人员快速上手

## 2. 技术栈选型建议与理由

### 2.1 后端技术
| 技术 | 版本 | 选择理由 |
|------|------|--------|
| .NET | 10.0 | 高性能、跨平台、生态成熟，适合构建企业级API服务 |
| ASP.NET Core | 10.0 | 轻量级、高性能的Web框架，支持RESTful API开发 |
| Entity Framework Core | 10.0 | 功能强大的ORM框架，简化数据库操作 |
| PostgreSQL | 15.0+ | 开源、稳定、支持复杂查询和向量搜索，适合存储商品数据 |
| RabbitMQ | 3.12+ | 可靠的消息队列，用于事件发布和服务间通信 |
| Azure OpenAI | - | 提供先进的语义搜索能力，提升用户体验 |

### 2.2 前端技术
| 技术 | 版本 | 选择理由 |
|------|------|--------|
| .NET MAUI | 10.0 | 跨平台移动应用开发框架，可构建iOS、Android和Windows应用 |
| Blazor | 10.0 | 使用C#开发Web界面，与后端技术栈统一，提高开发效率 |

### 2.3 开发工具
| 工具 | 版本 | 选择理由 |
|------|------|--------|
| Visual Studio 2022 | 17.10+ | 完整的.NET开发环境，支持调试、测试和部署 |
| .NET SDK | 10.0 | 提供命令行工具和运行时环境 |
| Docker | 20.10+ | 容器化部署，简化环境管理 |
| Git | 2.30+ | 版本控制，协作开发 |

### 2.4 测试框架
| 框架 | 版本 | 选择理由 |
|------|------|--------|
| xUnit | 2.4+ | 现代化的测试框架，支持并行测试和数据驱动测试 |
| Moq | 4.18+ | 强大的模拟框架，用于单元测试 |
| FluentAssertions | 6.12+ | 流畅的断言库，提高测试可读性 |

### 2.5 第三方库
| 库 | 版本 | 用途 |
|----|------|------|
| Asp.Versioning.Http | 7.0+ | API版本控制 |
| Pgvector | 0.5+ | PostgreSQL向量搜索支持 |
| Aspire.Npgsql.EntityFrameworkCore.PostgreSQL | 9.0+ | .NET Aspire PostgreSQL集成 |
| Aspire.Azure.AI.OpenAI | 9.0+ | .NET Aspire OpenAI集成 |

## 3. 开发环境搭建步骤

### 3.1 操作系统兼容性
- **Windows**：Windows 10/11 (64位)
- **macOS**：macOS 12+
- **Linux**：Ubuntu 20.04+, Debian 11+

### 3.2 前置依赖要求
- Docker Desktop (用于容器化部署)
- .NET 10.0 SDK
- PostgreSQL 15.0+
- RabbitMQ 3.12+

### 3.3 开发工具安装与配置
1. **安装.NET 10.0 SDK**
   - 访问 https://dot.net/download
   - 下载并安装适合操作系统的SDK版本
   - 验证安装：`dotnet --version`

2. **安装Visual Studio 2022**
   - 访问 https://visualstudio.microsoft.com/vs/
   - 选择"ASP.NET and web development"工作负载
   - 选择".NET Aspire SDK"组件

3. **安装Docker Desktop**
   - 访问 https://docs.docker.com/engine/install/
   - 下载并安装适合操作系统的版本
   - 启动Docker Desktop并验证运行状态

### 3.4 环境变量配置
1. **数据库连接字符串**
   ```
   ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=catalogdb;Username=postgres;Password=your_password
   ```

2. **RabbitMQ连接字符串**
   ```
   ConnectionStrings__EventBus=amqp://guest:guest@localhost:5672/
   ```

3. **OpenAI配置** (可选)
   ```
   ConnectionStrings__OpenAi=Endpoint=your_endpoint;Key=your_key;
   ```

### 3.5 环境验证
1. **验证.NET SDK**
   ```powershell
   dotnet --version
   ```
   预期输出：10.0.x

2. **验证Docker**
   ```powershell
   docker --version
   docker ps
   ```
   预期输出：Docker版本信息和运行中的容器列表

3. **验证PostgreSQL**
   ```powershell
   docker run --name postgres -e POSTGRES_PASSWORD=your_password -p 5432:5432 -d postgres:15
   ```
   预期：PostgreSQL容器启动成功

4. **验证RabbitMQ**
   ```powershell
   docker run --name rabbitmq -p 5672:5672 -p 15672:15672 -d rabbitmq:3-management
   ```
   预期：RabbitMQ容器启动成功

### 3.6 常见问题解决
- **端口冲突**：修改容器端口映射或停止占用端口的服务
- **权限问题**：确保当前用户有足够权限运行Docker命令
- **网络问题**：检查网络连接，确保Docker可以访问外部网络
- **版本不兼容**：确保所有组件版本匹配，特别是.NET SDK和依赖库

## 4. 项目结构设计

### 4.1 整体架构图
```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│   Client Apps   │     │  Catalog API    │     │  External Services │
├─────────────────┤     ├─────────────────┤     ├─────────────────┤
│  • Mobile App   │────▶│  • API Endpoints│────▶│  • PostgreSQL   │
│  • Web App      │     │  • Business Logic│     │  • RabbitMQ     │
└─────────────────┘     │  • Data Access   │     │  • OpenAI       │
                        └─────────────────┘     └─────────────────┘
```

### 4.2 目录结构设计
```
Catalog.API/
├── Apis/                  # API端点定义
│   └── CatalogApi.cs      # 主要API路由和处理逻辑
├── Extensions/            # 扩展方法
│   └── Extensions.cs      # 服务和中间件扩展
├── Infrastructure/        # 基础设施
│   └── CatalogContext.cs  # 数据库上下文
├── Model/                 # 数据模型
│   ├── CatalogBrand.cs    # 品牌模型
│   ├── CatalogItem.cs     # 商品模型
│   ├── CatalogType.cs     # 类型模型
│   ├── PaginatedItems.cs  # 分页结果模型
│   └── PaginationRequest.cs # 分页请求模型
├── Pics/                  # 商品图片
├── Properties/            # 项目属性
├── Services/              # 服务
│   ├── CatalogAI.cs       # AI服务
│   └── ICatalogAI.cs      # AI服务接口
├── Setup/                 # 初始化数据
│   └── catalog.json       # 种子数据
├── Program.cs             # 应用入口
├── Catalog.API.csproj     # 项目文件
└── appsettings.json       # 配置文件
```

### 4.3 配置文件组织方式
- **appsettings.json**：基础配置
- **appsettings.Development.json**：开发环境配置
- **appsettings.Production.json**：生产环境配置
- **User Secrets**：本地开发敏感配置

### 4.4 代码规范与命名约定
- **命名规范**：
  - 类名：PascalCase（如`CatalogItem`）
  - 方法名：PascalCase（如`GetItemById`）
  - 变量名：camelCase（如`catalogItem`）
  - 常量：UPPER_SNAKE_CASE（如`MAX_STOCK_THRESHOLD`）

- **代码风格**：
  - 使用4空格缩进
  - 大括号使用新行
  - 方法参数超过3个时使用换行
  - 每行不超过120个字符

- **注释规范**：
  - 类和方法使用XML文档注释
  - 复杂逻辑添加内联注释
  - 关键算法添加详细说明

## 5. 核心功能模块实现

### 5.1 数据模型定义

#### CatalogItem模型
```csharp
public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureFileName { get; set; }
    public int CatalogTypeId { get; set; }
    public CatalogType? CatalogType { get; set; }
    public int CatalogBrandId { get; set; }
    public CatalogBrand? CatalogBrand { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public Vector? Embedding { get; set; }
    public bool OnReorder { get; set; }

    public CatalogItem(string name) { Name = name; }

    // 减少库存
    public int RemoveStock(int quantityDesired)
    {
        if (AvailableStock == 0)
            throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
        
        if (quantityDesired <= 0)
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        
        int removed = Math.Min(quantityDesired, this.AvailableStock);
        this.AvailableStock -= removed;
        return removed;
    }

    // 增加库存
    public int AddStock(int quantity)
    {
        int original = this.AvailableStock;
        
        if ((this.AvailableStock + quantity) > this.MaxStockThreshold)
            this.AvailableStock += (this.MaxStockThreshold - this.AvailableStock);
        else
            this.AvailableStock += quantity;
        
        this.OnReorder = false;
        return this.AvailableStock - original;
    }
}
```

#### CatalogBrand模型
```csharp
public class CatalogBrand
{
    public CatalogBrand(string brand) { Brand = brand; }
    public int Id { get; set; }
    public string Brand { get; set; }
}
```

#### CatalogType模型
```csharp
public class CatalogType
{
    public CatalogType(string type) { Type = type; }
    public int Id { get; set; }
    public string Type { get; set; }
}
```

### 5.2 API接口开发

#### API路由配置
```csharp
public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Catalog");
        var api = vApi.MapGroup("api/catalog").HasApiVersion(1, 0).HasApiVersion(2, 0);
        var v1 = vApi.MapGroup("api/catalog").HasApiVersion(1, 0);
        var v2 = vApi.MapGroup("api/catalog").HasApiVersion(2, 0);

        // 商品查询路由
        v1.MapGet("/items", GetAllItemsV1)
            .WithName("ListItems")
            .WithSummary("List catalog items")
            .WithDescription("Get a paginated list of items in the catalog.")
            .WithTags("Items");
        
        // 其他路由...

        return app;
    }
}
```

#### 商品查询实现
```csharp
public static async Task<Ok<PaginatedItems<CatalogItem>>> GetAllItems(
    [AsParameters] PaginationRequest paginationRequest,
    [AsParameters] CatalogServices services,
    string? name,
    int? type,
    int? brand)
{
    var pageSize = paginationRequest.PageSize;
    var pageIndex = paginationRequest.PageIndex;

    var root = (IQueryable<CatalogItem>)services.Context.CatalogItems;

    // 应用筛选条件
    if (name is not null)
        root = root.Where(c => c.Name.StartsWith(name));
    if (type is not null)
        root = root.Where(c => c.CatalogTypeId == type);
    if (brand is not null)
        root = root.Where(c => c.CatalogBrandId == brand);

    // 计算总数和获取分页数据
    var totalItems = await root.LongCountAsync();
    var itemsOnPage = await root
        .OrderBy(c => c.Name)
        .Skip(pageSize * pageIndex)
        .Take(pageSize)
        .ToListAsync();

    return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
}
```

#### 商品更新实现
```csharp
public static async Task<Results<Created, BadRequest<ProblemDetails>, NotFound<ProblemDetails>>> UpdateItem(
    HttpContext httpContext,
    int id,
    [AsParameters] CatalogServices services,
    CatalogItem productToUpdate)
{
    var catalogItem = await services.Context.CatalogItems.SingleOrDefaultAsync(i => i.Id == id);

    if (catalogItem == null)
        return TypedResults.NotFound<ProblemDetails>(new (){
            Detail = $"Item with id {id} not found."
        });

    // 更新商品信息
    var catalogEntry = services.Context.Entry(catalogItem);
    catalogEntry.CurrentValues.SetValues(productToUpdate);

    // 更新嵌入向量
    catalogItem.Embedding = await services.CatalogAI.GetEmbeddingAsync(catalogItem);

    // 检查价格是否变更
    var priceEntry = catalogEntry.Property(i => i.Price);
    if (priceEntry.IsModified)
    {
        // 创建并发布价格变更事件
        var priceChangedEvent = new ProductPriceChangedIntegrationEvent(
            catalogItem.Id, productToUpdate.Price, priceEntry.OriginalValue);
        
        // 保存事件和上下文变更
        await services.EventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);
        
        // 发布事件
        await services.EventService.PublishThroughEventBusAsync(priceChangedEvent);
    }
    else
    {
        // 仅保存商品更新
        await services.Context.SaveChangesAsync();
    }
    
    return TypedResults.Created($"/api/catalog/items/{id}");
}
```

### 5.3 业务逻辑处理

#### 库存管理
```csharp
// 在CatalogItem类中实现
public int RemoveStock(int quantityDesired)
{
    if (AvailableStock == 0)
        throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
    
    if (quantityDesired <= 0)
        throw new CatalogDomainException($"Item units desired should be greater than zero");
    
    int removed = Math.Min(quantityDesired, this.AvailableStock);
    this.AvailableStock -= removed;
    return removed;
}

public int AddStock(int quantity)
{
    int original = this.AvailableStock;
    
    if ((this.AvailableStock + quantity) > this.MaxStockThreshold)
        this.AvailableStock += (this.MaxStockThreshold - this.AvailableStock);
    else
        this.AvailableStock += quantity;
    
    this.OnReorder = false;
    return this.AvailableStock - original;
}
```

#### 语义搜索
```csharp
public static async Task<Results<Ok<PaginatedItems<CatalogItem>>, RedirectToRouteHttpResult>> GetItemsBySemanticRelevance(
    [AsParameters] PaginationRequest paginationRequest,
    [AsParameters] CatalogServices services,
    string text)
{
    var pageSize = paginationRequest.PageSize;
    var pageIndex = paginationRequest.PageIndex;

    // 检查AI服务是否启用
    if (!services.CatalogAI.IsEnabled)
        return await GetItemsByName(paginationRequest, services, text);

    // 创建搜索文本的嵌入向量
    var vector = await services.CatalogAI.GetEmbeddingAsync(text);
    if (vector is null)
        return await GetItemsByName(paginationRequest, services, text);

    // 获取商品总数
    var totalItems = await services.Context.CatalogItems.LongCountAsync();

    // 按相似度排序获取商品
    var itemsOnPage = await services.Context.CatalogItems
        .Where(c => c.Embedding != null)
        .OrderBy(c => c.Embedding!.CosineDistance(vector))
        .Skip(pageSize * pageIndex)
        .Take(pageSize)
        .ToListAsync();

    return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
}
```

### 5.4 中间件设计与实现

#### 异常处理中间件
```csharp
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred while processing your request."
            };
            
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
```

#### 日志中间件
```csharp
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 开始时间
        var startTime = DateTime.UtcNow;
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;

        try
        {
            await _next(context);
        }
        finally
        {
            // 结束时间
            var endTime = DateTime.UtcNow;
            var elapsedTime = endTime - startTime;
            
            _logger.LogInformation(
                "Request {Method} {Path} completed in {ElapsedMilliseconds}ms with status code {StatusCode}",
                requestMethod, requestPath, elapsedTime.TotalMilliseconds, context.Response.StatusCode);
        }
    }
}
```

## 6. 数据库设计与集成

### 6.1 数据库架构图
```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│  CatalogItems   │◄────┤  CatalogBrands  │     │  CatalogTypes   │
├─────────────────┤     ├─────────────────┤     ├─────────────────┤
│ Id (PK)         │     │ Id (PK)         │     │ Id (PK)         │
│ Name            │     │ Brand           │     │ Type            │
│ Description     │     └─────────────────┘     └─────────────────┘
│ Price           │
│ PictureFileName │
│ CatalogTypeId (FK) │
│ CatalogBrandId (FK) │
│ AvailableStock  │
│ RestockThreshold│
│ MaxStockThreshold│
│ Embedding       │
│ OnReorder       │
└─────────────────┘
```

### 6.2 表结构设计

#### CatalogItems表
| 字段名 | 数据类型 | 约束 | 描述 |
|--------|---------|------|------|
| Id | INTEGER | PRIMARY KEY | 商品ID |
| Name | VARCHAR(255) | NOT NULL | 商品名称 |
| Description | TEXT | | 商品描述 |
| Price | DECIMAL(18,2) | NOT NULL | 商品价格 |
| PictureFileName | VARCHAR(255) | | 图片文件名 |
| CatalogTypeId | INTEGER | REFERENCES CatalogTypes(Id) | 类型ID |
| CatalogBrandId | INTEGER | REFERENCES CatalogBrands(Id) | 品牌ID |
| AvailableStock | INTEGER | NOT NULL | 可用库存 |
| RestockThreshold | INTEGER | NOT NULL | 补货阈值 |
| MaxStockThreshold | INTEGER | NOT NULL | 最大库存阈值 |
| Embedding | VECTOR(1536) | | 嵌入向量 |
| OnReorder | BOOLEAN | NOT NULL | 是否需要补货 |
| CreatedAt | TIMESTAMP | NOT NULL | 创建时间 |
| UpdatedAt | TIMESTAMP | NOT NULL | 更新时间 |

#### CatalogBrands表
| 字段名 | 数据类型 | 约束 | 描述 |
|--------|---------|------|------|
| Id | INTEGER | PRIMARY KEY | 品牌ID |
| Brand | VARCHAR(255) | NOT NULL | 品牌名称 |
| CreatedAt | TIMESTAMP | NOT NULL | 创建时间 |
| UpdatedAt | TIMESTAMP | NOT NULL | 更新时间 |

#### CatalogTypes表
| 字段名 | 数据类型 | 约束 | 描述 |
|--------|---------|------|------|
| Id | INTEGER | PRIMARY KEY | 类型ID |
| Type | VARCHAR(255) | NOT NULL | 类型名称 |
| CreatedAt | TIMESTAMP | NOT NULL | 创建时间 |
| UpdatedAt | TIMESTAMP | NOT NULL | 更新时间 |

### 6.3 索引设计与优化
- **CatalogItems**：
  - 主键索引：Id
  - 外键索引：CatalogTypeId, CatalogBrandId
  - 搜索索引：Name (用于前缀搜索)
  - 向量索引：Embedding (用于相似度搜索)

- **CatalogBrands**：
  - 主键索引：Id
  - 唯一索引：Brand (确保品牌名称唯一)

- **CatalogTypes**：
  - 主键索引：Id
  - 唯一索引：Type (确保类型名称唯一)

### 6.4 数据迁移策略与脚本

#### 初始迁移
```powershell
# 创建初始迁移
dotnet ef migrations add InitialCreate --context CatalogContext

# 应用迁移
dotnet ef database update --context CatalogContext
```

#### 种子数据脚本
```csharp
public static class CatalogSeed
{
    public static async Task SeedAsync(CatalogContext context)
    {
        // 检查是否已有数据
        if (context.CatalogBrands.Any() && context.CatalogTypes.Any() && context.CatalogItems.Any())
            return;

        // 插入品牌数据
        var brands = new List<CatalogBrand>
        {
            new CatalogBrand("Microsoft"),
            new CatalogBrand("Apple"),
            new CatalogBrand("Samsung"),
            new CatalogBrand("Sony"),
            new CatalogBrand("Google")
        };
        await context.CatalogBrands.AddRangeAsync(brands);

        // 插入类型数据
        var types = new List<CatalogType>
        {
            new CatalogType("Laptop"),
            new CatalogType("Smartphone"),
            new CatalogType("Tablet"),
            new CatalogType("Smartwatch"),
            new CatalogType("Headphones")
        };
        await context.CatalogTypes.AddRangeAsync(types);

        // 插入商品数据
        var items = new List<CatalogItem>
        {
            new CatalogItem("Surface Laptop 5")
            {
                Description = "Microsoft Surface Laptop 5 with 13.5-inch touchscreen",
                Price = 999.99m,
                PictureFileName = "1.webp",
                CatalogTypeId = types.First(t => t.Type == "Laptop").Id,
                CatalogBrandId = brands.First(b => b.Brand == "Microsoft").Id,
                AvailableStock = 50,
                RestockThreshold = 10,
                MaxStockThreshold = 100
            },
            // 更多商品...
        };
        await context.CatalogItems.AddRangeAsync(items);

        await context.SaveChangesAsync();
    }
}
```

### 6.5 ORM/Repository层实现

#### 数据库上下文
```csharp
public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
    {
    }

    public required DbSet<CatalogItem> CatalogItems { get; set; }
    public required DbSet<CatalogBrand> CatalogBrands { get; set; }
    public required DbSet<CatalogType> CatalogTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("vector");
        builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());

        // 添加事件日志表
        builder.UseIntegrationEventLogs();
    }
}
```

#### 实体配置
```csharp
public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");
        
        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.Id)
            .UseIdentityColumn();
        
        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(ci => ci.Description)
            .HasColumnType("text");
        
        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(ci => ci.PictureFileName)
            .HasMaxLength(255);
        
        builder.HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId);
        
        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogBrandId);
        
        builder.Property(ci => ci.AvailableStock)
            .IsRequired();
        
        builder.Property(ci => ci.RestockThreshold)
            .IsRequired();
        
        builder.Property(ci => ci.MaxStockThreshold)
            .IsRequired();
        
        builder.Property(ci => ci.Embedding)
            .HasColumnType("vector(1536)");
        
        builder.Property(ci => ci.OnReorder)
            .IsRequired();
        
        // 添加索引
        builder.HasIndex(ci => ci.Name);
        builder.HasIndex(ci => ci.CatalogTypeId);
        builder.HasIndex(ci => ci.CatalogBrandId);
    }
}
```

### 6.6 数据库连接池配置与优化

#### 连接池配置
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=catalogdb;Username=postgres;Password=your_password;Maximum Pool Size=100;Command Timeout=30;"
  }
}
```

#### 优化建议
- **连接池大小**：根据并发请求数设置，一般为CPU核心数的2-4倍
- **命令超时**：设置合理的超时时间，避免长时间阻塞
- **连接生命周期**：使用短连接，避免长时间占用连接
- **批量操作**：使用批量插入和更新，减少数据库往返
- **事务管理**：合理使用事务，避免长时间事务

## 7. 单元测试编写

### 7.1 测试框架使用指南

#### 项目设置
```powershell
# 创建测试项目
dotnet new xunit -n Catalog.Tests

# 添加依赖
dotnet add Catalog.Tests reference Catalog.API
dotnet add Catalog.Tests package Moq
dotnet add Catalog.Tests package FluentAssertions
```

### 7.2 核心功能测试用例设计

#### 商品管理测试
```csharp
public class CatalogItemTests
{
    [Fact]
    public void RemoveStock_ShouldDecreaseAvailableStock_WhenStockIsSufficient()
    {
        // Arrange
        var item = new CatalogItem("Test Item")
        {
            AvailableStock = 10,
            RestockThreshold = 5,
            MaxStockThreshold = 20
        };
        
        // Act
        var removed = item.RemoveStock(3);
        
        // Assert
        removed.Should().Be(3);
        item.AvailableStock.Should().Be(7);
    }
    
    [Fact]
    public void RemoveStock_ShouldThrowException_WhenStockIsEmpty()
    {
        // Arrange
        var item = new CatalogItem("Test Item")
        {
            AvailableStock = 0,
            RestockThreshold = 5,
            MaxStockThreshold = 20
        };
        
        // Act & Assert
        item.Invoking(i => i.RemoveStock(1))
            .Should().Throw<CatalogDomainException>()
            .WithMessage("Empty stock, product item Test Item is sold out");
    }
    
    [Fact]
    public void AddStock_ShouldIncreaseAvailableStock_WhenBelowMaxThreshold()
    {
        // Arrange
        var item = new CatalogItem("Test Item")
        {
            AvailableStock = 5,
            RestockThreshold = 5,
            MaxStockThreshold = 20
        };
        
        // Act
        var added = item.AddStock(10);
        
        // Assert
        added.Should().Be(10);
        item.AvailableStock.Should().Be(15);
    }
}
```

#### API测试
```csharp
public class CatalogApiTests
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public CatalogApiTests()
    {
        _server = new TestServer(new WebHostBuilder()
            .UseStartup<TestStartup>());
        _client = _server.CreateClient();
    }

    [Fact]
    public async Task GetItems_ShouldReturnPaginatedItems()
    {
        // Arrange
        var request = "/api/catalog/items?pageSize=10&pageIndex=0";
        
        // Act
        var response = await _client.GetAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("items");
    }
}
```

### 7.3 Mock对象使用方法

#### 服务Mock
```csharp
public class CatalogAiTests
{
    [Fact]
    public async Task GetEmbeddingAsync_ShouldReturnVector_WhenAiIsEnabled()
    {
        // Arrange
        var mockAiService = new Mock<ICatalogAI>();
        mockAiService.Setup(s => s.IsEnabled).Returns(true);
        mockAiService.Setup(s => s.GetEmbeddingAsync(It.IsAny<string>()))
            .ReturnsAsync(new Vector(new float[] { 0.1f, 0.2f, 0.3f }));
        
        // Act
        var vector = await mockAiService.Object.GetEmbeddingAsync("test");
        
        // Assert
        vector.Should().NotBeNull();
        vector.Dimensions.Should().Be(3);
    }
}
```

### 7.4 测试覆盖率目标与实现

#### 覆盖率目标
- **核心业务逻辑**：90%+
- **API接口**：80%+
- **数据访问**：70%+
- **工具类**：95%+

#### 覆盖率测量
```powershell
# 运行测试并生成覆盖率报告
dotnet test --collect:"XPlat Code Coverage"

# 查看覆盖率报告
# 报告位于: TestResults/{guid}/coverage.cobertura.xml
```

### 7.5 测试自动化配置

#### CI/CD配置
```yaml
# .github/workflows/test.yml
name: Test

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 10.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

## 8. 服务部署流程

### 8.1 开发/测试/生产环境配置差异

#### 开发环境
- 数据库：本地PostgreSQL或Docker容器
-  RabbitMQ：本地Docker容器
-  OpenAI：可选，使用开发密钥
- 日志：控制台输出，详细级别
- 监控：基本监控

#### 测试环境
- 数据库：测试专用PostgreSQL实例
-  RabbitMQ：测试专用实例
-  OpenAI：测试专用密钥
- 日志：文件输出，包含详细信息
- 监控：完整监控，包括性能指标

#### 生产环境
- 数据库：高可用PostgreSQL集群
-  RabbitMQ：高可用集群
-  OpenAI：生产密钥
- 日志：集中式日志系统（如ELK）
- 监控：全面监控，包括告警系统
- 安全：HTTPS，防火墙，访问控制

### 8.2 构建打包流程

#### 构建命令
```powershell
# 恢复依赖
dotnet restore

# 构建项目
dotnet build --configuration Release

# 运行测试
dotnet test --configuration Release

# 发布项目
dotnet publish --configuration Release --output ./publish
```

#### 优化选项
- **AOT编译**：提高运行性能
- **Trim unused code**：减少部署包大小
- **ReadyToRun**：提高启动速度
- **Single-file deployment**：简化部署

### 8.3 容器化部署方案

#### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Catalog.API/Catalog.API.csproj", "Catalog.API/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["IntegrationEventLogEF/IntegrationEventLogEF.csproj", "IntegrationEventLogEF/"]
COPY ["eShop.ServiceDefaults/eShop.ServiceDefaults.csproj", "eShop.ServiceDefaults/"]
RUN dotnet restore "Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
```

#### Docker Compose
```yaml
version: '3.8'

services:
  catalog-api:
    build:
      context: .
      dockerfile: Catalog.API/Dockerfile
    ports:
      - "8080:8080"
      - "8081:8081"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Host=postgres;Port=5432;Database=catalogdb;Username=postgres;Password=your_password
      - ConnectionStrings__EventBus=amqp://guest:guest@rabbitmq:5672/
    depends_on:
      - postgres
      - rabbitmq

  postgres:
    image: postgres:15
    environment:
      - POSTGRES_PASSWORD=your_password
      - POSTGRES_DB=catalogdb
    volumes:
      - postgres-data:/var/lib/postgresql/data

  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - "5672:5672"
      - "15672:15672"

volumes:
  postgres-data:
```

### 8.4 服务启动/停止/重启操作指南

#### 本地开发环境
```powershell
# 启动服务
dotnet run --project Catalog.API/Catalog.API.csproj

# 停止服务
Ctrl+C

# 重启服务
dotnet run --project Catalog.API/Catalog.API.csproj
```

#### Docker环境
```powershell
# 启动服务
docker-compose up -d

# 停止服务
docker-compose down

# 重启服务
docker-compose restart
```

#### Kubernetes环境
```powershell
# 部署服务
kubectl apply -f catalog-api-deployment.yaml

# 查看服务状态
kubectl get pods

# 重启服务
kubectl rollout restart deployment catalog-api
```

### 8.5 部署验证步骤与健康检查方法

#### 健康检查端点
```csharp
// 在Program.cs中添加
app.MapHealthChecks("/health");
```

#### 验证步骤
1. **服务启动验证**：
   ```powershell
   curl http://localhost:8080/health
   # 预期输出：{"status":"Healthy"}
   ```

2. **API功能验证**：
   ```powershell
   curl http://localhost:8080/api/catalog/catalogbrands
   # 预期输出：品牌列表
   
   curl http://localhost:8080/api/catalog/catalogtypes
   # 预期输出：类型列表
   ```

3. **数据库连接验证**：
   - 检查数据库日志
   - 运行数据库查询验证数据存在

4. **事件总线连接验证**：
   - 检查RabbitMQ管理界面
   - 验证事件发布功能

## 9. 常见问题排查指南

### 9.1 日志系统使用与分析方法

#### 日志配置
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "FormatterName": "json"
    }
  }
}
```

#### 日志分析工具
- **ELK Stack**：Elasticsearch, Logstash, Kibana
- **Graylog**：集中式日志管理
- **Datadog**：日志和监控一体化

### 9.2 常见错误码及解决方案

| 错误码 | 描述 | 可能原因 | 解决方案 |
|--------|------|---------|--------|
| 400 | 坏请求 | 无效的请求参数 | 检查请求参数格式和值 |
| 404 | 未找到 | 资源不存在 | 检查资源ID是否正确 |
| 500 | 内部服务器错误 | 服务器代码异常 | 查看日志，修复代码错误 |
| 503 | 服务不可用 | 依赖服务故障 | 检查数据库、RabbitMQ等依赖服务 |
| 429 | 请求过多 | API限流 | 减少请求频率，实现重试机制 |

### 9.3 性能瓶颈排查思路

1. **API响应缓慢**：
   - 检查数据库查询性能
   - 分析API处理时间
   - 检查网络延迟

2. **数据库性能问题**：
   - 分析SQL执行计划
   - 检查索引使用情况
   - 优化查询语句

3. **内存使用过高**：
   - 检查对象生命周期
   - 分析内存泄漏
   - 优化数据结构

4. **CPU使用率高**：
   - 分析代码热点
   - 优化算法
   - 考虑并行处理

### 9.4 数据库连接问题处理

1. **连接超时**：
   - 检查数据库服务器状态
   - 调整连接超时设置
   - 检查网络连接

2. **连接池耗尽**：
   - 增加连接池大小
   - 检查连接是否正确释放
   - 实现连接池监控

3. **数据库死锁**：
   - 分析死锁日志
   - 优化事务处理
   - 减少事务持有时间

### 9.5 网络异常处理策略

1. **服务间通信失败**：
   - 实现重试机制
   - 使用断路器模式
   - 监控网络状态

2. **外部服务不可用**：
   - 实现降级策略
   - 使用缓存减少依赖
   - 异步处理非关键操作

3. **网络延迟**：
   - 优化数据传输
   - 使用CDN加速
   - 实现请求超时机制

## 10. 总结与最佳实践

### 10.1 总结
本技术文档提供了一个完整的指南，指导您从零开始构建一个功能完整、可稳定运行的Catalog服务项目。通过遵循文档中的步骤，您可以：

- 搭建开发环境并配置必要的依赖
- 设计和实现数据模型与API接口
- 集成数据库和外部服务
- 编写单元测试确保代码质量
- 部署和监控服务运行状态

### 10.2 最佳实践
1. **代码质量**：
   - 遵循SOLID原则
   - 保持代码简洁明了
   - 编写单元测试
   - 使用代码分析工具

2. **性能优化**：
   - 数据库索引优化
   - 缓存策略
   - 异步处理
   - 资源池管理

3. **安全性**：
   - 输入验证
   - 认证授权
   - 数据加密
   - 安全审计

4. **可维护性**：
   - 模块化设计
   - 清晰的文档
   - 一致的代码风格
   - 版本控制最佳实践

5. **可扩展性**：
   - 微服务架构
   - 容器化部署
   - 水平扩展
   - 配置外部化

通过遵循这些最佳实践，您可以构建一个高质量、可靠的Catalog服务，为您的电子商务平台提供强大的商品管理能力。