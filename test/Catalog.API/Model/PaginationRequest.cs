using System.ComponentModel; // 导入组件模型命名空间，用于属性描述和默认值

namespace eShop.Catalog.API.Model; // 命名空间定义

/// <summary>
/// 分页请求记录，用于API分页参数
/// </summary>
public record PaginationRequest(
    /// <summary>
    /// 每页返回的项目数量
    /// </summary>
    [property: Description("Number of items to return in a single page of results")] // 属性描述
    [property: DefaultValue(10)] // 默认值
    int PageSize = 10, // 默认值为10

    /// <summary>
    /// 要返回的结果页的索引
    /// </summary>
    [property: Description("The index of the page of results to return")] // 属性描述
    [property: DefaultValue(0)] // 默认值
    int PageIndex = 0 // 默认值为0
);
