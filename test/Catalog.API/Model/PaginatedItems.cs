using System.Text.Json.Serialization; // 导入JSON序列化命名空间，用于控制JSON序列化行为

namespace eShop.Catalog.API.Model; // 命名空间定义

/// <summary>
/// 分页项目类，用于返回分页数据
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class PaginatedItems<TEntity>(
    int pageIndex, // 页码索引
    int pageSize, // 每页大小
    long count, // 总记录数
    IEnumerable<TEntity> data // 数据集合
) where TEntity : class // 泛型约束，TEntity必须是引用类型
{
    /// <summary>
    /// 页码索引
    /// </summary>
    public int PageIndex { get; } = pageIndex;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; } = pageSize;

    /// <summary>
    /// 总记录数
    /// </summary>
    public long Count { get; } = count;

    /// <summary>
    /// 数据集合
    /// </summary>
    public IEnumerable<TEntity> Data { get;} = data;
}
