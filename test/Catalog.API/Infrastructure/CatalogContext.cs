namespace eShop.Catalog.API.Infrastructure; // 命名空间定义

/// <summary>
/// 数据库上下文类，用于处理数据库操作
/// </summary>
/// <remarks>
/// 添加迁移使用以下命令在'Catalog.API'项目目录内：
///
/// dotnet ef migrations add --context CatalogContext [migration-name]
/// </remarks>
public class CatalogContext : DbContext // 继承自DbContext
{
    /// <summary>
    /// 构造函数，接收数据库上下文选项和配置
    /// </summary>
    /// <param name="options">数据库上下文选项</param>
    /// <param name="configuration">应用配置</param>
    public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
    {
    }

    /// <summary>
    /// 商品数据集
    /// </summary>
    public required DbSet<CatalogItem> CatalogItems { get; set; }

    /// <summary>
    /// 品牌数据集
    /// </summary>
    public required DbSet<CatalogBrand> CatalogBrands { get; set; }

    /// <summary>
    /// 类型数据集
    /// </summary>
    public required DbSet<CatalogType> CatalogTypes { get; set; }

    /// <summary>
    /// 模型创建时的配置
    /// </summary>
    /// <param name="builder">模型构建器</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // 添加PostgreSQL向量扩展，用于语义搜索
        builder.HasPostgresExtension("vector");
        
        // 应用实体类型配置
        builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());

        // 添加事件日志表到上下文
        builder.UseIntegrationEventLogs();
    }
}
