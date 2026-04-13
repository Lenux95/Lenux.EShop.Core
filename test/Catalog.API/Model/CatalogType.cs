using System.ComponentModel.DataAnnotations; // 导入数据注解命名空间，用于属性验证

namespace eShop.Catalog.API.Model; // 命名空间定义

/// <summary>
/// 类型模型类，表示商品的类型
/// </summary>
public class CatalogType
{
    /// <summary>
    /// 构造函数，初始化类型名称
    /// </summary>
    /// <param name="type">类型名称</param>
    public CatalogType(string type) {
        Type = type;
    }

    /// <summary>
    /// 类型ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 类型名称，必填字段
    /// </summary>
    [Required] // 标记为必填字段
    public string Type { get; set; }
}
